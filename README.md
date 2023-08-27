# PinPointRelay

This program reads messages (NMEA strings or alerts) from a selected COM port and sends them to a web server. The user of this program is able to choose the COM port and web URL, and these settings are saved in the user’s application data folder in Windows. This program also generates a 22-character ID code. The HTTP POST request sent to the website with this program includes the message received on the COM port in the “nmea” variable and the ID code in the “id_code” variable. This ID code ensures that the website can identify multiple Pinpoint devices, but people cannot maliciously use someone else’s ID code. 

![Relay](/relay-screenshot.png)
