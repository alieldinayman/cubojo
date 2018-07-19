using UnityEngine;
using System.Collections;
using System;
using System.IO.Ports;
 
 
public class MotionSensor : MonoBehaviour
{
 
    SerialPort stream;
 
    public GameObject target; // is the gameobject to
 
    public float acc_normalizer_factor = 0.00025f;
    public float gyro_normalizer_factor = 1.0f / 32768.0f;   // 32768 is max value captured during test on imu
 
    //float curr_angle_x = 0;
    //float curr_angle_y = 0;
    //float curr_angle_z = 0;
 
    float curr_offset_x = 0;
    float curr_offset_y = -2;
 
    // Increase the speed/influence rotation
    public float factor = 7;
    public bool enableRotation;
    public bool enableTranslation;
 
    // SELECT YOUR COM PORT AND BAUDRATE
    string port = "COM5";
    int baudrate = 4800;
 
    void Start()
    {
		Debug.Log("OK1");
        stream = new SerialPort("\\\\.\\" + port, baudrate);
		
        stream.Open();
		Debug.Log("OK");
    }
 
    void Update()
    {
		/*if (curr_offset_x < -10)
		{
			curr_offset_x = -10;
		}
		
		if (curr_offset_x > 10)
		{
			curr_offset_x = 10;
		}
		
		if (curr_offset_y > 10)
		{
			curr_offset_y = 10;
		}
		
		if (curr_offset_y < 1)
		{
			curr_offset_y = -1;
		}*/
		
		if (Input.GetKeyDown("space"))
		{
			Debug.Log("OK");
			curr_offset_x = 0;
			curr_offset_y = -2;
		}
        string dataString = "null received";
		dataString = stream.ReadLine();
		
        char splitChar = ';';
        string[] dataRaw = dataString.Split(splitChar);
			
		float vx = Int32.Parse(dataRaw[2]);
		float vy = Int32.Parse(dataRaw[3]);
		
        curr_offset_x += (vx*acc_normalizer_factor);
        curr_offset_y += (vy*acc_normalizer_factor);
		
		//Debug.Log(curr_offset_x);
		//Debug.Log(curr_offset_y);

        if(enableTranslation) target.transform.position = new Vector3(-curr_offset_x, -curr_offset_y, -15f);
        //if(enableRotation)target.transform.rotation = Quaternion.Euler(curr_angle_x * factor, -curr_angle_z * factor, curr_angle_y * factor);
			
    }
	
	  void OnTriggerEnter(Collider col){
      if(col.gameObject.tag == "Cube"){
           Destroy(col.gameObject);
		}
	  }
		  
 
}