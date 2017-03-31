using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Web;
using System.Net;

namespace PinPointRelay
{
    public partial class Form1 : Form
    {
        static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string PinpointFolder = Path.Combine(AppData, "Pinpoint");
        static string PinpointLogFile = Path.Combine(PinpointFolder, "log.txt");
        static string PinpointOptionsFile = Path.Combine(PinpointFolder, "options.txt");

        string COMPortName { get; set; }
        string WebServerAddress { get; set; }
        string IDCode { get; set; }

        Thread ReadThread { get; set; }
        SerialPort sp { get; set; }

        public Form1()
        {
            InitializeComponent();

            RefreshButton_Click(null, null);

            IDCode = RandomIDString();

            try
            {
                LoadOptions();
            }
            catch { }

            LogLine("Program started");

            ReadThread = new Thread(Read);
            ReadThread.Start();
        }

        /// <summary>
        /// Continuously reads lines from the SerialPort object
        /// and logs them in the LogBox and PinpointLogFile.
        /// </summary>
        void Read()
        {
            while (true)
            {
                try
                {
                    string line = sp.ReadLine().Replace("\n", "").Replace("\r", "");
                    LogLine(line);
                    SendLineToServer(line);
                }
                catch(IOException e)
                {
                    MessageBox.Show("Serial port disconnected. \n\n" + e.ToString());
                }
            }
        }

        /// <summary>
        /// Refresh the COM ports listed in the COMPortsList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            COMPortsList.Items.Clear();
            foreach (string PortName in SerialPort.GetPortNames())
            {
                COMPortsList.Items.Add(PortName);
            }

            // If COMPortName is still contained in COMPortsList, select it
            SelectCOMPort(COMPortName);
        }

        /// <summary>
        /// Select a COM port by name. If it's in the COMPortsList,
        /// it will be selected in the list and a SerialPort object
        /// will be created for it.
        /// </summary>
        /// <param name="COMPort">Name of COM port. ex: COM4</param>
        void SelectCOMPort(string COMPort)
        {
            int index = COMPortsList.FindString(COMPort);
            if (index != -1)
            {
                COMPortsList.SetSelected(index, true);
                // COM port is autmatically opened here due to the
                // SelectedValueChanged event
            }

        }

        /// <summary>
        /// Opens the COM port named by COMPortName
        /// </summary>
        void OpenCOMPort()
        {
            if (sp != null && sp.IsOpen)
            {
                sp.Close();
            }
            sp = new SerialPort(COMPortName, 9600, Parity.None, 8, StopBits.One);
            sp.Open();
        }

        private void COMPortsList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (COMPortsList.SelectedIndex != -1)
            {
                string NewCOMPortName = (string)((ListBox)sender).SelectedItem;
                if (NewCOMPortName != COMPortName)
                {
                    COMPortName = NewCOMPortName;
                    OpenCOMPort();
                    LogLine("COM port changed to " + COMPortName);
                }

            }
        }

        /// <summary>
        /// Log a line in the LogBox textbox and in the 
        /// PinpointLogFile text file.
        /// </summary>
        /// <param name="line"></param>
        private void LogLine(string line)
        {
            if (!Directory.Exists(PinpointFolder))
            {
                Directory.CreateDirectory(PinpointFolder);
            }

            string FullLine = DateTime.UtcNow.ToString("s") + " " + line + Environment.NewLine;
            File.AppendAllText(PinpointLogFile, FullLine);

            if (Thread.CurrentThread == ReadThread)
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    LogBox.AppendText(FullLine);
                });
            }
            else
            {
                LogBox.AppendText(FullLine);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReadThread.Abort();
            SaveOptions();
            LogLine("Program terminated");
        }

        /// <summary>
        /// Load options from PinpointOptionsFile
        /// </summary>
        void LoadOptions()
        {
            foreach (string line in File.ReadAllLines(PinpointOptionsFile))
            {
                try
                {
                    string optionname = line.Substring(0, line.IndexOf('='));
                    string optionvalue = line.Substring(line.IndexOf('=') + 1);

                    switch (optionname)
                    {
                        case "comport":
                            SelectCOMPort(optionvalue);
                            break;
                        case "weburl":
                            WebServerAddressBox.Text = optionvalue;
                            break;
                        case "idcode":
                            IDCode = optionvalue;
                            break;
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Save options to PinpointOptionsFile
        /// </summary>
        void SaveOptions()
        {
            if (!Directory.Exists(PinpointFolder))
            {
                Directory.CreateDirectory(PinpointFolder);
            }

            File.WriteAllLines(PinpointOptionsFile, new string[] {
                "comport=" + COMPortName,
                "weburl=" + WebServerAddress,
                "idcode=" + IDCode
            });
        }

        private void WebServerAddressBox_TextChanged(object sender, EventArgs e)
        {
            WebServerAddress = WebServerAddressBox.Text;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Hide();
                SysTrayIcon.Visible = true;
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                SysTrayIcon.Visible = false;
                this.Show();
            }
        }

        private void SysTrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SysTrayIcon.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }


        private void SendLineToServer(string line)
        {
            // this is what we are sending
            string post_data = "nmea=" + Uri.EscapeDataString(line) + "&id_code=" + IDCode;
            // MessageBox.Show(post_data);

            // this is where we will send it
            string uri = WebServerAddressBox.Text;

            // create a request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";

            // turn our request string into a byte stream
            byte[] postBytes = Encoding.ASCII.GetBytes(post_data);

            // this is important - make sure you specify type this way
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // grab te response and print it out to the console along with the status code
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string resp_str = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch { }
            

            //Console.WriteLine(response.StatusCode);
            //MessageBox.Show(resp_str);
        }

        string RandomIDString()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendLineToServer("@ALERT-0-B");
        }
    }
}
