# PinPointRelay

This program reads messages (NMEA strings or alerts) from a selected COM port and sends them to a web server. The user of this program is able to choose the COM port and web URL, and these settings are saved in the user’s application data folder in Windows. This program also generates a 22-character ID code. The HTTP POST request sent to the website with this program includes the message received on the COM port in the “nmea” variable and the ID code in the “id_code” variable. This ID code ensures that the website can identify multiple Pinpoint devices, but people cannot maliciously use someone else’s ID code. 

## Related Repositories

- [pinpoint-pic](https://github.com/omccully/pinpoint-pic) - C microcontroller program for this project
- [pinpoint](https://github.com/omccully/pinpoint) - Ruby on Rails app for this project

## Demo

[![Pinpoint Demonstration]
(https://i.ytimg.com/vi/boWn7i7RBzA/maxresdefault.jpg)] 
(https://www.youtube.com/watch?v=boWn7i7RBzA "Pinpoint Demonstration")

![Relay](/full-screenshot.png)


