#include <Arduino.h>
#include <EEPROM.h>
#include <FreqPeriodCounter.h>

#define led_pin 13

#define TH_ADC A0                                             // analog pin 0 = throttle position input signal
#define ACC_ADC A1                                            // analog pin 1 = gas pedal position input signal
#define FAULT 2                                               // digital pin 2 = fail safe status
#define ACC_PWM 9                                             // digital pin 9 = gas pedal output PWM
#define RPM_IN 3                                              // digital pin 3 = RPM input

#define POS_TAB_SIZE 10                                       // number of position ranges
#define RPM_TAB_SIZE 10                                       // number of RPM ranges (has to be at least 1)
#define POS_TAB_ACT_SIZE (POS_TAB_SIZE+2)                     //
#define RPM_TAB_ACT_SIZE (RPM_TAB_SIZE+2)                     //

#define EE_POS_ADR 0                                          // EEPROM address for position ranges
#define EE_RPM_ADR (EE_POS_ADR + POS_TAB_ACT_SIZE)            // EEPROM address for RMP ranges
#define EE_OUT_ADR (EE_RPM_ADR + RPM_TAB_ACT_SIZE*2)          // EEPROM address for translation table
#define EE_CLOSED_CAL_ADR (EE_OUT_ADR + POS_TAB_ACT_SIZE * RPM_TAB_ACT_SIZE)  // EEPROM addres for gas pedal de-pressed calibration value
#define EE_OPENED_CAL_ADR (EE_CLOSED_CAL_ADR + 2)             // EEPROM addres for gas pedal pressed calibration value

// serial communication aliases
#define COM_FREE 0
#define COM_GET_FULL_T 'F'
#define COM_SEND_FULL_T 'f'
#define COM_GET_RPM_T 'R'
#define COM_SEND_RPM_T 'r'
#define COM_GET_POS_T 'P'
#define COM_SEND_POS_T 'p'
#define COM_EE_BURN 'B'
#define COM_CLOSE_CAL 'C'
#define COM_OPEN_CAL 'O'
#define COM_CAL_BURN 'E'

//uint8_t range_pos[POS_TAB_ACT_SIZE] = {0, 7, 15, 40, 55, 70, 103, 150, 175, 200, 225, 255};
/*uint8_t output_pos[RPM_TAB_ACT_SIZE][POS_TAB_ACT_SIZE] = {
  {0, 10, 25, 50, 75, 120, 110, 75, 50, 25, 10, 255},
  {0, 10, 25, 50, 75, 120, 110, 75, 50, 25, 10, 255},
  {0, 10, 25, 50, 75, 120, 110, 75, 50, 25, 10, 255},
  {0, 10, 25, 50, 75, 120, 110, 75, 50, 25, 10, 255},
  {0, 10, 25, 50, 75, 120, 110, 75, 50, 25, 10, 255},
  {0, 10, 25, 50, 75, 120, 110, 75, 50, 25, 10, 255},
  {0, 10, 25, 50, 75, 120, 110, 75, 50, 25, 10, 255},
  {0, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 255},
  {0, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 255},
  {0, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 255},
  {0, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 255},
  {0, 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 255}};
uint16_t range_rpm[RPM_TAB_ACT_SIZE] = {100, 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 12000};*/
uint8_t range_pos[POS_TAB_ACT_SIZE];
uint8_t output_pos[RPM_TAB_ACT_SIZE][POS_TAB_ACT_SIZE];
uint16_t range_rpm[RPM_TAB_ACT_SIZE];
uint16_t max_rpm, rpm_temp;
uint8_t command_count=0;
uint8_t command_status=COM_FREE;
uint8_t uni1=0;
uint8_t send_pos_inedx=0, send_rpm_index=0;
float gas_factor=0.4153;
uint16_t closed_calib=205, opened_calib=819;

FreqPeriodCounter counter_ms(RPM_IN, micros);
uint8_t translate_with_weights(uint16_t, uint16_t);
void counterISR();


void setup() 
{
  pinMode(led_pin,OUTPUT);

  // setup output accelerator PWM pin
  pinMode(ACC_PWM, OUTPUT);

  // interrupt for RPM counter
  attachInterrupt(1, counterISR, CHANGE);

  Serial.begin(57600);           //  setup serial

  //EEPROM.put(EE_POS_ADR, range_pos);
  //EEPROM.put(EE_RPM_ADR, range_rpm);
  //EEPROM.put(EE_OUT_ADR, output_pos);

  // read translation table and ranges from EEPROM
  EEPROM.get(EE_POS_ADR, range_pos);
  EEPROM.get(EE_RPM_ADR, range_rpm);
  EEPROM.get(EE_OUT_ADR, output_pos);

  max_rpm = range_rpm[RPM_TAB_ACT_SIZE-1];
 
  // calculate gas calibration factor
  gas_factor = (float)255 / (opened_calib - closed_calib);
}

void loop()
{
  int acc_val;
  int new_out;
  uint8_t ser_data;

  // read input gas pedal position
  acc_val = analogRead(ACC_ADC);

  // calculate new translated position
  // TODO: put measured RPM as second argument
  new_out = translate_with_weights(acc_val, 2000);

  // update stimulus for TL494
  analogWrite(ACC_PWM, new_out);

  if(counter_ms.ready()) 
  {
    // TODO: move "new_out =" here
    // TODO: put in RPM as calculation: 60000/counter_ms.period

  }

  // take care of serial communication inputs
  if(Serial.available())
  {
    ser_data = Serial.read();

    // toggle LED for debug purposes
    if(uni1 == 0)
    {
      digitalWrite(led_pin,HIGH);
      uni1 = 1;
    }
    else
    {
      digitalWrite(led_pin,LOW);
      uni1 = 0;
    }

    // when new command comes
    if(command_status == COM_FREE)
    {
      // receive full translation table
      if(ser_data == COM_GET_FULL_T)
      {
        command_status = COM_GET_FULL_T;
        command_count = POS_TAB_ACT_SIZE * RPM_TAB_ACT_SIZE;
      }

      // receive RPM ranges table
      if(ser_data == COM_GET_RPM_T)
      {
        command_status = COM_GET_RPM_T;
        command_count = RPM_TAB_ACT_SIZE * 2;
      }

      // receive position ranges table
      if(ser_data == COM_GET_POS_T)
      {
        command_status = COM_GET_POS_T;
        command_count = POS_TAB_ACT_SIZE;
      }

      // send full translation table
      if(ser_data == COM_SEND_FULL_T)
      {
        command_status = COM_SEND_FULL_T;
        command_count = POS_TAB_ACT_SIZE * RPM_TAB_ACT_SIZE;
        send_pos_inedx = POS_TAB_ACT_SIZE - 1;
        send_rpm_index = RPM_TAB_ACT_SIZE - 1;
      }

      // send RPM ranges
      if(ser_data == COM_SEND_RPM_T)
      {
        command_status = COM_SEND_RPM_T;
        command_count = RPM_TAB_ACT_SIZE * 2;
      }

      // send position ranges
      if(ser_data == COM_SEND_POS_T)
      {
        command_status = COM_SEND_POS_T;
        command_count = POS_TAB_ACT_SIZE;
      }

      // burn data to EEPROM
      if(ser_data == COM_EE_BURN)
      {
        EEPROM.put(EE_POS_ADR, range_pos);
        EEPROM.put(EE_RPM_ADR, range_rpm);
        EEPROM.put(EE_OUT_ADR, output_pos);

        command_status = COM_FREE;
      }

      // refresh de-pressed gas pedal calibration
      if(ser_data == COM_CLOSE_CAL)
      {
        closed_calib = acc_val;

        command_status = COM_FREE;
      }

      // refresh pressed gas pedal calibration
      if(ser_data == COM_OPEN_CAL)
      {
        opened_calib = acc_val;

        command_status = COM_FREE;
      }

      // store gas pedal calibration in EEPROM
      if(ser_data == COM_CAL_BURN)
      {
        EEPROM.put(EE_CLOSED_CAL_ADR, closed_calib);
        EEPROM.put(EE_OPENED_CAL_ADR, opened_calib);

        command_status = COM_FREE;
      }
    }
    else 
    {
      // receiving full translation table in progress
      if(command_status == COM_GET_FULL_T)                      
      {
        command_count--;                                        // decrease nuber of bytes left to receive

        //uint8_t col, row;
        //col = command_count / RPM_TAB_ACT_SIZE;
        //row = command_count % RPM_TAB_ACT_SIZE;
        //output_pos[col][row] = ser_data;                       // save new data

        uint8_t *tab_point = &output_pos[0][0];
        *(tab_point + command_count) = ser_data;                // save new data from the end

        // when last byte of the command received
        if(command_count == 0)
          command_status = COM_FREE;
      }

      // receiving RPM ranges table in progress
      if(command_status == COM_GET_RPM_T)                      
      {
        uint8_t temp_index;

        command_count--;
  
        if ((command_count & 0x01) == 0)
        {
            // odd bytes = MSB
            rpm_temp = rpm_temp + (((uint16_t)ser_data) << 8);

            temp_index = command_count >> 1;
            range_rpm[temp_index] = rpm_temp;
        }
        else
        {
            // even bytes = LSB
            rpm_temp = ser_data;
        }

        // when last byte of the command received
        if(command_count == 0)
          command_status = COM_FREE;
      }

      // receiving position ranges table in progress
      if(command_status == COM_GET_POS_T)                      
      {
        command_count--;                                        // decrease nuber of bytes left to receive

        range_pos[command_count] = ser_data;

        // when last byte of the command received
        if(command_count == 0)
          command_status = COM_FREE;
      }
    }
  } 

  // handling commands sending data back to PC
  // send back full translation table
  if((command_status == COM_SEND_FULL_T))// && (Serial.availableForWrite()))
  {
    delay(12);

    Serial.write(output_pos[send_rpm_index][send_pos_inedx]);

    if(send_pos_inedx == 0)
    {
      send_pos_inedx = POS_TAB_ACT_SIZE - 1;
      send_rpm_index--;
    }
    else
      send_pos_inedx--;

    command_count--;                                          // decrease nuber of bytes left to send

    //uint8_t *tab_point = &output_pos[0][0];
    //Serial.write(*(tab_point + command_count));             // send new data from the end

    //when last byte of the command send
    if(command_count == 0)
    {
      command_status = COM_FREE;
      delay(12);
    }
  }

  // send RPM ranges table
  if(command_status == COM_SEND_RPM_T)
  {
    uint8_t temp_index;

    command_count -= 2;
    temp_index = command_count >> 1;

    // send LSB and MSB
    Serial.write((uint8_t)(range_rpm[temp_index] & 0xFF));
    delay(12);
    Serial.write((uint8_t)(range_rpm[temp_index] >> 8));
    delay(12);

    //when last byte of the command send
    if(command_count == 0)
    {
      command_status = COM_FREE;
      delay(12);
    }
  }

  // send position ranges table
  if(command_status == COM_SEND_POS_T)
  {
    command_count--;                                          // decrease nuber of bytes left to send

    delay(12);
    Serial.write(range_pos[command_count]);

    //when last byte of the command send
    if(command_count == 0)
    {
      command_status = COM_FREE;
      delay(12);
    }
  }

}


// interrupt for RPM counter
void counterISR()
{ 
  counter_ms.poll();
}

// based on input position and RPM read values from translation table and aproximate the output from up to 4 points
uint8_t translate_with_weights(uint16_t input_pos, uint16_t input_rpm)
{
  uint8_t i1, pos1, pos2, in_pos;
  unsigned int rpm1, rpm2, dif1, dif2, dif_tot, in_temp;
  float weip1, weip2, weir1, weir2, output;

  // normalize input position to calibrated gas pedal range
  if(input_pos < closed_calib)
    input_pos = closed_calib;  
  in_temp = (float)(input_pos - closed_calib) * gas_factor;
  if(in_temp > 255)
    in_temp = 255;
  in_pos = in_temp;

  // RPM - find points and calculate weights (assuming top limit cannot be exceeded)
  for(i1=1;i1<RPM_TAB_ACT_SIZE;i1++)                          // sweep through RPM ranges
  {
    if(input_rpm <= range_rpm[i1])                            // when range found
    {
        dif_tot = range_rpm[i1] - range_rpm[i1-1];
        dif1 = input_rpm - range_rpm[i1-1];
        dif2 = range_rpm[i1] - input_rpm;

        weir1 = (float)dif2 / dif_tot;
        weir2 = (float)dif1 / dif_tot;

        rpm1 = i1 - 1;
        rpm2 = i1;
        
      i1 = RPM_TAB_ACT_SIZE;                                  // exit for loop
    }
  }

  // POSITION - find points and calculate weights
  for(i1=1;i1<POS_TAB_ACT_SIZE;i1++)            
  {
    if(in_pos < range_pos[i1])                             // when range found
    {
        dif_tot = range_pos[i1] - range_pos[i1-1];
        dif1 = in_pos - range_pos[i1-1];
        dif2 = range_pos[i1] - in_pos;

        weip1 = (float)dif2 / dif_tot;
        weip2 = (float)dif1 / dif_tot;

        pos1 = i1 - 1;
        pos2 = i1;
 
        i1 = POS_TAB_ACT_SIZE;                                // exit for loop
    }
  }

  // calculate translated position acording to 4 points with weights
  output = (float)(((float)output_pos[rpm1][pos1] * weip1) + ((float)output_pos[rpm1][pos2] * weip2)) * weir1;
  output += (float)(((float)output_pos[rpm2][pos1] * weip1) + ((float)output_pos[rpm2][pos2] * weip2)) * weir2;

  return (uint8_t)output;
}
