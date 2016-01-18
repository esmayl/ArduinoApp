using System;
using UnityEngine;
using System.Net;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Networking : MonoBehaviour
{
    static WebClient client = new WebClient();

    static int lightSensorValue = 0;
    static int temperatureSensor = 0;

    int timeOut = 5;
    float timeOutTimer = 0f;

    float loopTimer = 0;
    float resendTime = 0.5f;


    public void Awake()
    {
        client.DownloadStringCompleted += ShowComplete;

        SendRequest("http://192.168.1.200");
    }

    public void Update()
    {
        if (client.IsBusy)
        {
            timeOutTimer += Time.deltaTime;

            if (timeOutTimer > timeOut)
            {
                timeOutTimer = 0f;

                client.CancelAsync();

                Debug.Log("Client got no response, connection timed out.");
            }
        }
        else if (loopTimer >= resendTime)
        {
            SendRequest("http://192.168.1.200");
            loopTimer = 0;
        }
        
        if (loopTimer < resendTime && !client.IsBusy)
        {
            loopTimer += Time.deltaTime;
        }
    }

    public static void SendRequest(string url)
    {
        if (client.IsBusy) { Debug.Log("Client is busy!"); return; }
        
        client.DownloadStringAsync(new Uri(url),null);
    }

    public static void ShowComplete(object sender, DownloadStringCompletedEventArgs e)
    {
        if (e.Result != "")
        {
            Debug.Log("Page found!");
        }
        else { return; }

        if (e.Result.Contains("LichtSensor"))
        {
            int indexPin = e.Result.IndexOf("LichtSensor");

            string tempString = e.Result.Substring(indexPin + "LichtSensor".Length+2,1);
            SetLightValue(int.Parse(tempString));
        }
        if (e.Result.Contains("TemperatuurSensor"))
        {
            int indexSensor = e.Result.IndexOf("TemperatuurSensor");

            string tempString = e.Result.Substring(indexSensor + "TemperatuurSensor".Length + 2, 1);
            SetTemperatureValue(int.Parse(tempString));
        }
    }

    public static void SetLightValue(int p)
    {
        lightSensorValue = p;
    }
    
    public static void SetTemperatureValue(int p)
    {
        temperatureSensor = p;
    }

    public static int GetLightValue()
    {
        return lightSensorValue;
    }

    public static int GetTemperatureValue()
    {
        return temperatureSensor;
    }

}
