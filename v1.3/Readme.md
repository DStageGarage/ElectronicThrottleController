# ETC v1.3 is the main/recommended version for HW generation 1. 

Last update 2023.03.26:
- changed the frequency of TPS input filter from 150Hz to 720Hz to avoid problem of tgrottle "flapping" due to too slow response presumably. 
  The new value has been tested on the bench and seems to solve the issue. No needto remove C2 and C3 now. 
  Changed component values: R9 from 5k1 to 10k, C2 from 100n to 22n, C3 from 220n to 22n.
- changed throttle motor PWM frequency from quite high ~300kHz to resonable ~100kHz. This change vastly reduces heat loss in H-bridge MOSFET transistors.
  As the dead time is achieved by difference in time of openning and closing the transistors (open by resistor, close by diode) majority of heat loses
  comes from channel opening time. With reduced frequency transistors are now much cooler.
- updated schematic in user manual and added note that there may be small differences in the looks of JLCPCB component list due to above changes in BOM.
