# ETC v2.2 is the newer HW generation

2024.11.13:
> [!IMPORTANT]
> A new recomended "low gain" BOM added.
> There is a known problem with some of the throttles where they tend to "flap" at some positions or when position is changed. This is most likely caused by the high gain of the error aplifier in PWM controller. In simple terms the PWM changes very fast so the throttle motor can not keep up thus starts oscilating.
> There are documented cases where lowering the gain  solves that issue. This new BOM changes value of R7 from 10k to 1k and R8 from 1k to 2.2k, efectively changing the ęrror amplifier gain from ~10 to ~0.45. This has not been tested with a "model" Bosch throttle yet but shouldn't cause any issues.

2023.08.22:
- Alternative BOM file version includes screw terminal connectors so you can order them soldered in via JLCPCB
- User manual pending, use calibration sheet for now.
