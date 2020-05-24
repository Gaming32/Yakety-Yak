using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class YaketyYakManager : MonoBehaviour
{
    public YaketyYak yaketyYak;
    public Button hostButton;
    public Button controlButton;
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
        controlButton.onClick.RemoveListener(OnConnect);
        controlButton.onClick.AddListener(OnDisconnect);
        controlButton.GetComponentInChildren<Text>().text = "Disconnect";

        string ip = ipComponent.text;
        PlayerPrefs.SetString("lastAddress", ip);
        Debug.Log("Connecting to " + ip + " port " + yaketyYak.port.ToString() + "...");
        networkManager.networkAddress = ip;
        networkManager.StartClient();
    }

    public void OnDisconnect()
    {
        controlButton.onClick.RemoveListener(OnDisconnect);
        controlButton.onClick.AddListener(OnConnect);
        controlButton.GetComponentInChildren<Text>().text = "Connect";

        Debug.Log("Disconnecting...");
        networkManager.StopClient();
    }

    public void OnHost()
    {
        Debug.Log("Hosting on 0.0.0.0 port " + yaketyYak.port.ToString() + "...");
        networkManager.StartHost();
    }
}
