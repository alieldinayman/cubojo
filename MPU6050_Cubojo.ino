#include <Wire.h>
#include <I2Cdev.h>
#include <MPU6050.h>

MPU6050 mpu;
int16_t ax, ay, az, gx, gy, gz;
int vx, vy;

void setup() {
  Serial.begin(4800);
  Wire.begin();
  mpu.initialize();
  //if (!mpu.testConnection()) { while (1); }
}

void loop() {
  mpu.getMotion6(&ax, &ay, &az, &gx, &gy, &gz);

  vx = ((gx+300)/150)-1;  // "+300" because the x axis of gyroscope give values about -350 while it's not moving. Change this value if you get something different using the TEST code, chacking if there are values far from zero.
  vy = -(gz-100)/150; // same here about "-100"

  String output = String(gx) + ";" + String(gz) + ";" + String(vx) + ";" + String(vy);

  Serial.println(output);
  Serial.flush();
  
  delay(10);
}
