using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SensorValue : MonoBehaviour
{
    [Range(0,1)]
    public int sensorPin = 0;

    Text sensorObject;

    void Awake()
    {
        sensorObject = GetComponent<Text>();
    }

	void Update ()
	{
	    switch (sensorPin)
	    {
            case 0:
	                sensorObject.text = "Light value: "+Networking.GetLightValue();
	                break;
            case 1:
	                sensorObject.text = "Temperature value: "+Networking.GetTemperatureValue();
                    break;
	    }

	}
}
