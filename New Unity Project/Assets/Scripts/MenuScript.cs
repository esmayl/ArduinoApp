using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public GameObject[] RFZenderMenu;
    public GameObject[] SensorMenu;

    // Update is called once per frame
    void Update () {
	}

    public void ToggleRFMenu(bool toggle)
    {
        foreach (GameObject g in RFZenderMenu)
        {
            g.SetActive(toggle);
        }
    }

    public void ToggleSensorMenu(bool toggle)
    {
        foreach (GameObject g in SensorMenu)
        {
            g.SetActive(toggle);
        }
    }
}
