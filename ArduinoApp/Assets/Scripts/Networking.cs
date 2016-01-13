using System;
using UnityEngine;
using System.Net;
using UnityEngine.Networking;

public class Networking : MonoBehaviour
{
    public int timeOut = 10;

    static WebClient client = new WebClient();

    float timer = 0f;

    public void Awake()
    {
        client.DownloadStringCompleted += ShowComplete;        
    }

    public void Update()
    {
        if (client.IsBusy)
        {
            timer += Time.deltaTime;

            if (timer > timeOut)
            {
                timer = 0f;

                client.CancelAsync();

                Debug.Log("Client got no response, connection timed out.");
            }
        }
    }

    public void SendRequest(string url)
    {
        if (client.IsBusy) { Debug.Log("Client is busy!"); return; }
        
        client.DownloadStringAsync(new Uri(url),null);
        //client.DownloadString(String.Format("http://192.168.1.200/{0}={1}", pin, value));
    }

    public static void ShowComplete(object sender, DownloadStringCompletedEventArgs e)
    {
        Debug.Log(""+e.Result);
    }
}
