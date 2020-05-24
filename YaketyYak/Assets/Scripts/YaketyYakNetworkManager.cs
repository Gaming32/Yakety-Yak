using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class YaketyYakNetworkManager : NetworkManager
{
    public Text infoLabel;
    public Text addressLabel;

    string hostname;

    public void Awake()
    {
        hostname = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
    }

    public void Start()
    {
        addressLabel.text = "My Address:\n" + hostname;
    }

    public override void OnStartHost()
    {
        Debug.Log("Host Started! (" + hostname + ")");
        infoLabel.text = "Host Started!";
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        Debug.Log("Connected!");
        infoLabel.text = "Connected successfully!";
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        Debug.Log("Disconnected");
        infoLabel.text = "Disconnected";
    }
}
