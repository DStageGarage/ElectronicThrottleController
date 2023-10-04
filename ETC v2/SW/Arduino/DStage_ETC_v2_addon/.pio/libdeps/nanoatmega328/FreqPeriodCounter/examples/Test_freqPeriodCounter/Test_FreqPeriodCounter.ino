#include <FreqPeriodCounter.h>
#include <Streaming.h>
#include <TimerOne.h> 

// Works on Arduino Uno, not on Zero
// Puts a PWM signal on d9 and measure it at d3 
// Note: connect d3 to d9 

const byte counterPin = 3; // connect d3 to d9
const byte counterInterrupt = 1; // = d3
const byte PWMpin = 9; // PWM only on d9 or d10

FreqPeriodCounter counter(counterPin, micros);

void setup(void) 
{ Serial.begin(9600); 
  pinMode(PWMpin, OUTPUT);  
  Timer1.initialize(); 
  testAll();
  Timer1.pwm(PWMpin, 300, 20000); // duty cycle [10 bit], period [us] <8388480 
  attachInterrupt(counterInterrupt, counterISR, CHANGE);
}

void loop(void) 
{ if(!counter.ready()) Serial << "\nwaiting";
  else Serial << endl << counter.level, counter.period, counter.pulseWidth, counter.pulseWidthLow; 
}

void counterISR()
{ counter.poll();
}

