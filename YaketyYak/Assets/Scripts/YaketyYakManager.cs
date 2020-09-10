using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YaketyYakManager : MonoBehaviour
{
    public YaketyYak yaketyYak;
    public string key;

    //NetworkManager networkManager;
    LiteNetServer server;
    LiteNetClient client;

    public void Awake()
    {
        //networkManager = GetComponent<NetworkManager>();
        //networkManager.networkPort = yaketyYak.port;
        server = GetComponent<LiteNetServer>();
        client = GetComponent<LiteNetClient>();
    }

    public void OnConnect(Text ipComponent)
    {
        string ip = ipComponent.text;
        Debug.Log("Connecting to " + ip + " port " + yaketyYak.port.ToString() + "...");
        client.StartClient(ip, yaketyYak.port, key);
        //networkManager.networkAddress = ip;
        //networkManager.StartClient();
    }

    public void OnHost()
    {
        Debug.Log("Hosting on 0.0.0.0 port " + yaketyYak.port.ToString() + "...");
        server.StartServer(yaketyYak.port, key);
        client.StartClient("127.0.0.1", yaketyYak.port, key);
        //networkManager.StartHost();
    }
}
