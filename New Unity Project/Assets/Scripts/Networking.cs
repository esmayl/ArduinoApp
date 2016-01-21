using System;
using UnityEngine;
using System.Net;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Networking : MonoBehaviour
{
    public static WebClient client = new WebClient();

    static int lightSensorValue = 0;
    static int temperatureSensor = 0;

    static float loopTimer = 0;
    static float resendTime = 0.5f;
    static float timeOutTimer = 0f;    
    static int timeOut = 5;



    public void Awake()
    {
        client.DownloadStringCompleted += ShowComplete;

        SendRequest("http://192.168.1.200");
    }

    public void Update()
    {
        if (client.IsBusy)
        {
            TickTimeOut(Time.deltaTime);

            if (TimeOutCompare())
            {
                ResetTimeOutTimer();

                client.CancelAsync();

                Debug.Log("Client got no response, connection timed out.");
            }
        }
        else if (LoopTimerCompare())
        {
            SendRequest("http://192.168.1.200");
            ResetLoopTimer();
        }
        
        if (!LoopTimerCompare())
        {
            TickLoopTimer(Time.deltaTime);
        }
    }

    public static void TickTimeOut(float tick)
    {
        timeOutTimer += tick;
    }

    public static void TickLoopTimer(float tick)
    {
        loopTimer += tick;
    }

    public static bool TimeOutCompare()
    {
        if (timeOutTimer > timeOut)
        {
            return true;
        }

        return false;
    }

    public static bool LoopTimerCompare()
    {
        if (loopTimer >= resendTime && !client.IsBusy)
        {
            return true;
        }
        
        return false;
    }

    public static void ResetLoopTimer()
    {
        loopTimer = 0;
    }

    public static void ResetTimeOutTimer()
    {
        timeOutTimer = 0;
    }

    public static void SendRequest(string url)
    {      
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
