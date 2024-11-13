# ETC v1.x is the first HW generation 

2024.11.13:
> [!IMPORTANT]
> A new recomended "low gain" BOM added.
> There is a known problem with some of the throttles where they tend to "flap" at some positions or when position is changed. This is most likely caused by the high gain of the error aplifier in PWM controller. In simple terms the PWM changes very fast so the throttle motor can not keep up thus starts oscilating.
> There are documented cases where lowering the gain  solves that issue. This new BOM changes value of R7 from 10k to 1k and R8 from 1k to 2.2k, efectively changing the ęrror amplifier gain from ~10 to ~0.45. This has not been tested with a "model" Bosch throttle yet but shouldn't cause any issues.

2023.09.06:
- added 3D model for printing a case for v1.x

2023.03.26:
- v1.3 - changed the frequency of TPS input filter from 150Hz to 720Hz to avoid problem of tgrottle "flapping" due to too slow response presumably. 
  The new value has been tested on the bench and seems to solve the issue. No needto remove C2 and C3 now. 
  Changed component values: R9 from 5k1 to 10k, C2 from 100n to 22n, C3 from 220n to 22n.
- v1.3 - changed throttle motor PWM frequency from quite high ~300kHz to resonable ~100kHz. This change vastly reduces heat loss in H-bridge MOSFET transistors.
  As the dead time is achieved by difference in time of openning and closing the transistors (open by resistor, close by diode) majority of heat loses
  comes from channel opening time. With reduced frequency transistors are now much cooler.
- v1.3 - updated schematic in user manual and added note that there may be small differences in the looks of JLCPCB component list due to above changes in BOM.
