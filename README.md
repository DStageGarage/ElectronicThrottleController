# ElectronicThrottleController

IMPORTANT - For production in JLCPCB please use v1.3 or v2.2 files! 

This is an open HW project of Electronic Throttle Controller also known as Drive By Wire, created by Filip from DStage garage.

# HW generation 1
The board is purely analogue, no programming required and no chance of failure due to MCU hang-up, flash disappearing due to harsh engine environment etc. Safety was a priority with this one ;-) It also has build in simple fail safes - in case any of the control wires is broken or shorted with power for example the throttle will close automatically.

The board shape is designed to fit popular aluminium casing to be found in tme.eu, Farnell etc. There are no connectors, wires are soldered directly to the board to keep it compact.

You can also check a video to learn more about this project: https://www.youtube.com/watch?v=MTnsbOgZkUA 

# HW generation 2
The main functionality is purely analogue. However, there is now a place provided for an optional Arduino Nano, which can be used to monitor sensors but also to create a gas pedal map if required (SW not provided at the moment). All signals connected to the Arduino are buffered to minimise the risk in case of program failure. The usage of the Arduino module for throttle control has to be delibatery selected by desoldering jumper Z1 and soldering Z2. If special functions provided by Arduino are not needed, no actions are required, and the board will work in the same way as generation 1.
Wires can be soldered directly to the board, but this version allows you to use ARK terminals instead. 
Compared to generation 1, this one is improved and better optimised in terms of heat dissipation.

The board form factor is larger than for version 1, but it will fit a popular aluminium casing, and a 3D model for printing the casing will be available soon. 

# Disclaimer: 
This device is dedicated for stationary engines and vehicles driving on private closed
properties. It should not be used on public roads!
