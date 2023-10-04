/* 
Release Notes
28-5-2013 delay(2900) wait for stable pwm signal
7-2-2017 All tests with poll(digitalRead(counterPin)) instead of poll()

*/

#include <assert.h>

const int debounceTime_ms = 10;
volatile bool testing=1;

FreqPeriodCounter counter_ms(millis);
FreqPeriodCounter counter_us(micros);
FreqPeriodCounter counter_deb(millis, debounceTime_ms);
//FreqPeriodCounter counter_ms(counterPin, millis);
//FreqPeriodCounter counter_us(counterPin, micros);
//FreqPeriodCounter counter_deb(counterPin, millis, debounceTime_ms);

void testAll()
{ Serial << "\nStart, wait a moment";
  Timer1.pwm(PWMpin, 600, 1023000);
  delay(2900); // wait for stable pwm signal, start with high level
  test_interrupt();
  test_ms(); 
  test_us(); 
  test_us();
  test_ms(); 
  test_Hertz();
  testDebounce();
  //test_msContinuously();
  //test_usContinuously();
  Serial << "\nAll tests OK";
  while(1);
}

void test_interrupt()
{ attachInterrupt(counterInterrupt, testISR, CHANGE);
  while(testing); 
  detachInterrupt(counterInterrupt);
}

void testISR()
{ //if(counter_ms.poll())
  if(counter_ms.poll(digitalRead(counterPin))) 
  { testing = 0;
    assert(counter_ms.period >= 1022);
    assert(counter_ms.period <= 1024);
    assert(counter_ms.pulseWidth >= 599);
    assert(counter_ms.pulseWidth <= 601);
    assert(counter_ms.pulseWidthLow >= 422);
    assert(counter_ms.pulseWidthLow <= 424);
    assert(counter_ms.ready() == 1);
    Serial << F("\ntest interrupt OK\n");
    Serial << counter_ms.level, counter_ms.period, counter_ms.pulseWidth, counter_ms.pulseWidthLow;
  }
}

void test_ms()
{ counter_ms.synchronize(); // previous test also uses counter_ms
  while(1)
  { //if(counter_ms.poll()) 
    if(counter_ms.poll(digitalRead(counterPin))) 
    { assert(counter_ms.period >= 1022);
      assert(counter_ms.period <= 1024);
      assert(counter_ms.pulseWidth >= 599);
      assert(counter_ms.pulseWidth <= 601);
      assert(counter_ms.pulseWidthLow >= 422);
      assert(counter_ms.pulseWidthLow <= 424);
      assert(counter_ms.ready() == 1);
      assert(counter_ms.ready() == 0); // ! 
      Serial << F("\ntest millisec OK\n");
      Serial << counter_ms.level, counter_ms.period, counter_ms.pulseWidth, counter_ms.pulseWidthLow;
      break;
    }
  }
}
 
void test_us()
{ while(1)
  { //if(counter_us.poll()) 
    if(counter_us.poll(digitalRead(counterPin))) 
    { assert(counter_us.period >= 1022000);
      assert(counter_us.period <= 1024000);
      assert(counter_us.pulseWidth >= 599000);
      assert(counter_us.pulseWidth <= 601000);
      assert(counter_us.pulseWidthLow >= 422000);
      assert(counter_us.pulseWidthLow <= 424000);
      assert(counter_us.ready() == 1);
      Serial << F("\ntest microsec OK\n");
      Serial << counter_us.level, counter_us.period, counter_us.pulseWidth, counter_us.pulseWidthLow;
      break;
    }
  } 
}

void test_Hertz()
{ counter_us.period = 100;
  assert(counter_us.hertz() == 10000);
  Serial << F("\ntest hertz micros OK\n");
  counter_ms.period = 10;
  assert(counter_ms.hertz() == 100);  
  assert(counter_ms.hertz(1000) >= 10000);
  Serial << F("\ntest hertz millis OK\n");
}

//   _   _                    _
// _| |_| |__________________| |_
//  0 2 4 6                100
    
void testDebounce()
{ unsigned long elapsedTime, time, periodBegin;
  while(1)
  { time = millis();
    elapsedTime = time - periodBegin;  
    if(elapsedTime > 100) periodBegin = time;
    if(elapsedTime < 2) digitalWrite(PWMpin, HIGH);
    if(elapsedTime >= 2) digitalWrite(PWMpin, LOW);
    if(elapsedTime < 4) digitalWrite(PWMpin, LOW);
    if(elapsedTime >= 4) digitalWrite(PWMpin, HIGH);
    if(elapsedTime > 6) digitalWrite(PWMpin, LOW);

    //if(counter_deb.poll())
    if(counter_deb.poll(digitalRead(counterPin)))
    { assert(counter_deb.period >= 90);
      assert(counter_deb.period <= 110);
      assert(counter_deb.pulseWidth >= 8);
      assert(counter_deb.pulseWidth <= 12);
      Serial << F("\ntest Debounce OK\n");
      Serial << counter_deb.level, counter_deb.period, counter_deb.pulseWidth, counter_deb.pulseWidthLow;
      break;
    }
  }
}

void test_msContinuously()
{ Timer1.pwm(PWMpin, 600, 1023000); 
  while(1)
  { //counter_ms.poll();
    counter_ms.poll(digitalRead(counterPin));
    if(loopDuration_ms(0, 1000)) Serial << endl << counter_ms.period, counter_ms.pulseWidth, counter_ms.pulseWidthLow;
  }
}

void test_usContinuously()
{ Timer1.pwm(PWMpin, 600, 50);
  while(1)
  { //counter_us.poll();
    counter_us.poll(digitalRead(counterPin));
    if(loopDuration_ms(0, 20000)) Serial << endl << counter_us.period, counter_us.pulseWidth, counter_us.pulseWidthLow;
  }
}

bool loopDuration_ms(bool _print, int interval)
{ static unsigned long lastLoopTime=0;
  static int i=0;
  unsigned long time = micros();
  int loopDuration = time - lastLoopTime;
  lastLoopTime = time;
  if(i++ > interval)
  { if(_print) Serial << " " << loopDuration;
    i=0;
    return true;
  }
  return false;
}



