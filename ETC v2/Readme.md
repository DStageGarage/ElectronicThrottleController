# ETC v2.x - 2nd HW generation

## Hisotry of changes
Last update 2024.11.13:
> [!IMPORTANT]
> A new recomended "low gain" BOM added.
> There is a known problem with some of the throttles where they tend to "flap" at some positions or when position is changed. This is most likely caused by the high gain of the error aplifier in PWM controller. In simple terms the PWM changes very fast so the throttle motor can not keep up thus starts oscilating.
> There are documented cases where lowering the gain  solves that issue. This new BOM changes value of R7 from 10k to 1k and R8 from 1k to 2.2k, efectively changing the ęrror amplifier gain from ~10 to ~0.45. This has not been tested with a "model" Bosch throttle yet but shouldn't cause any issues.

2023.10.04:
- beta SW for Arduino Nano and PC added, not tested

2023.08.30:
- two variants of 3D models fo case added

2023.08.22:
- Alternative BOM file version for v2.2 inclused screw terminal connectors so you can order them soldered in via JLCPCB
  
## Connection
| Connector | Signal | Function |
| :---: | :---: | :--- |
| P1 | GP | Gas pedal position signal. Most cases there are two redundand outputs from the gas pedal, use one on which voltage increases with pushing the pedal further. If both outputs have inclinig signal the one with wider voltage range is usually preferable. |
| P1 | GND | Use this pin for both gas pedal and TPS ground (negative power terminal). In case of potentiometer based sensors connect it to the low side of the potentiometer. |
| P2 | +5V | Use this pin for both gas pedal and TPS power (positive power terminal). In case of potentiometer based sensors connect it to the high side of the potentiometer. |
| P2 | TH | Throttle Position Sensor signal. Most cases there are two redundand outputs from the TPS, use one on which voltage increases with openning the throttle further. If both outputs have inclinig signal the one with wider voltage range is usually preferable. |
| P3 | GND | Use this pin for connecting to ECU signal ground when using IAC input and/or sharing the TPS signal. |
| P3 | IAC | Idle air PWM signal input from the ECU. In cas ECU has an open-collector/open-drain style IAC output (only connects to ground) user has to add pull-up resistor (for example 10k) to +5V on this input. Preferably the PWM signal should be 0-5V but 0-12V will also work. Please note that IAC has relatively high range by default and can open the throttle up to 30%. R26 can be increased in value to tighten the range and improve control resolution. |
| P4 | PWR | Main power terminal. Connect to +12V with a 5A fuse and a minimum of 1mm2 wire. |
| P4 | GND | Power ground for the board. Connect to a power ground on the vehicle with a minimum 1mm2 wire. |
| P5 | M1 | Throttle motor terminal 1. Polarity has to be checked experimentaly. |
| P5 | M2 | Throttle motor terminal 2. Polarity has to be checked experimentaly. |

## Calibration
Please refer to the DStage_ETC_v2.2_calibration.xlsx spreadsheet for calibration instructions.
