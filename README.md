# ElectronicThrottleController

IMPORTANT - For production in JLCPCB please use v1.3 or v2.2 files! 

This is a project of Electronic Throttle Controller also known as Drive By Wire, created by Filip from DStage garage and free to use.

## HW generation 1
> [!IMPORTANT]
> ETC v1.3 is only suited for potentiometer based throttle position sensors and gas pedals as it uses a simple calibration method.
> 
The board is purely analogue, no programming required and no chance of failure due to MCU hang-up, flash disappearing due to harsh engine environment etc. Safety was a priority with this one ;-) It also has build in simple fail safes - in case any of the control wires is broken or shorted with power for example the throttle will close automatically.

The board shape is designed to fit popular aluminium casing to be found in tme.eu, Farnell etc. You can also use the provided STL file for 3D printing the case.There are no connectors, wires are soldered directly to the board to keep it compact.

You can also check a video to learn more about this project: https://www.youtube.com/watch?v=MTnsbOgZkUA 

## HW generation 2
> [!TIP]
> ETC v2.2 is suited for all types of throttle position sensors and gas pedals as it uses a different calibration method in comparison to v1.3.

The main functionality is purely analogue. If no special functions are desired the provided place for an optional Arduino Nano may stay unpopulated, no actions are required and the board will work in the same way as generation 1. The optional module can be used to monitor sensors but also to create a gas pedal map if required (basic SW in beta version is provided). All signals connected to the Arduino are buffered to minimise the risk in case of program failure. The usage of the Arduino module for throttle control has to be delibatery selected by desoldering jumper Z1 and soldering Z2. 

Wires can be soldered directly to the board, but this version allows you to use ARK terminals (screw terminals) instead. If PCB and assembly is ordered in JLCPCB user can chose BOM file with or without ARK terminals.
Besides the different input circuitry/calibration method compared to generation 1 this one is improved and better optimised in terms of heat dissipation.

The board form factor is larger than for version 1, but it will fit a popular aluminium casing, and a STL file for 3D printing is also provided. 

Here's a quick video comparing v2.2 with v1.3 https://studio.youtube.com/video/byb1L_GXVq8/edit

# Disclaimer: 
This device is dedicated for stationary engines and vehicles driving on private closed
properties. It should not be used on public roads!
