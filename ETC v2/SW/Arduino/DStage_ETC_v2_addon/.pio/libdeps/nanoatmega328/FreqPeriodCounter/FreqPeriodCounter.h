/* 
FreqPeriodCounter
Copyright (C) 2012  Albert van Dalen http://www.avdweb.nl
This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License at http://www.gnu.org/licenses .

AUTHOR: Albert van Dalen
WEBSITE: http://www.avdweb.nl/arduino/libraries/frequency-period-counter.html
*/

#ifndef FREQPERIODCOUNTER_H 
#define FREQPERIODCOUNTER_H

#include <Arduino.h>

class FreqPeriodCounter
{    
public:
  FreqPeriodCounter(byte pin, unsigned long (*timeFunctionPtr)(), unsigned debounceTime=0); 
  FreqPeriodCounter(unsigned long (*timeFunctionPtr)(), unsigned debounceTime=0); 
  void synchronize();
  bool poll(); // input pin
  bool poll(bool _level); 
  bool ready();
  unsigned long hertz(unsigned int precision=1);

  unsigned long period, pulseWidth, pulseWidthLow, elapsedTime;
  bool level; 
  
protected:
  unsigned long time, transientTime;
  unsigned debounceTime; 
  char transientCount;
  byte pin;
  
  bool lastLevel, readyVal; 
  unsigned long (*timeFunctionPtr)();
};

#endif


