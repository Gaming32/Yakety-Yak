using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class YaketyYakManager : MonoBehaviour
{
    public YaketyYak yaketyYak;

    NetworkManager networkManager;

    public void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        networkManager.networkPort = yaketyYak.port;
    }

    public void OnConnect(Text ipComponent)
    {
        string ip = ipComponent.text;
        Debug.Log("Connecting to " + ip + " port " + yaketyYak.port.ToString() + "...");
        networkManager.networkAddress = ip;
        networkManager.StartClient();
    }

    public void OnHost()
    {
        Debug.Log("Hosting on 0.0.0.0 port " + yaketyYak.port.ToString() + "...");
        networkManager.StartHost();
    }
}
