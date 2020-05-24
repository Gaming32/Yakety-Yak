using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class YaketyYakManager : MonoBehaviour
{
    public YaketyYak yaketyYak;
    public Text ipComponent;

    NetworkManager networkManager;

    public void Start()
    {
        ipComponent.text = PlayerPrefs.GetString("lastAddress", "");
        networkManager = GetComponent<NetworkManager>();
        networkManager.networkPort = yaketyYak.port;
    }

    public void OnConnect()
    {
        string ip = ipComponent.text;
        PlayerPrefs.SetString("lastAddress", ip);
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
