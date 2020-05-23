using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YaketyYakManager : MonoBehaviour
{
    public void OnConnect(Text ipComponent)
    {
        string ip = ipComponent.text;
        Debug.Log("Connecting to " + ip + " port 9250...");
    }

    public void OnHost()
    {
        Debug.Log("Hosting on 0.0.0.0 port 9250...");
    }
}
