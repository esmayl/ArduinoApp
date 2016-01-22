using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SensorValue : MonoBehaviour
{
    [Range(0,1)]
    public int sensorPin = 0;

    Image sensorObject;

    void Awake()
    {
        sensorObject = GetComponentInChildren<Image>();
    }

	void Update ()
	{
	    switch (sensorPin)
	    {
            case 0:
	                if (Networking.GetLightValue() > 0)
	                {
	                    sensorObject.CrossFadeColor(Color.red, 0.1f, true, true);
	                }
	                else
	                {
                        sensorObject.CrossFadeColor(Color.green, 0.1f, true, true);
	                }
	                break;
            case 1:
	                if (Networking.GetTemperatureValue() > 0)
	                {
	                    sensorObject.CrossFadeColor(Color.red, 0.1f, true, true);
	                }
	                else
	                {
                        sensorObject.CrossFadeColor(Color.green, 0.1f, true, true);
	                }
                    break;
	    }

	}
}
