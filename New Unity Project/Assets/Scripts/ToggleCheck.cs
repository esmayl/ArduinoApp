﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ToggleCheck : MonoBehaviour
{
    public string url = "";

    Toggle toggle;

    string temp;

    void Awake()
    {
        temp = url;
        toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(Toggled);
    }

    void Update()
    {
        if (Networking.client.IsBusy)
        {
            toggle.interactable = false;
        }
        else
        {
            toggle.interactable = true;
            Networking.ResetLoopTimer();
        }
    }


    public void Toggled(bool value)
    {
        Debug.Log(value);
        if (value)
            {
            temp = url;
            temp += "1";
            }
        if (!value)
        {
            temp = url;
            temp += "0";
        }
        Networking.SendRequest(temp);
        }
    }