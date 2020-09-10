using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLib;

public class LiteNetServer : MonoBehaviour
{
    EventBasedNetListener netListener;
    NetManager netManager;

    bool serverStarted;

    private void Awake()
    {
        netListener = new EventBasedNetListener();
        netManager = new NetManager(netListener);

    }

    // Start is called before the first frame update
    public void StartServer(int port, string key)
    {
        Debug.Log("Starting server...");

        netListener.ConnectionRequestEvent += (request) =>
        {
            request.Accept();
        };

        netListener.PeerConnectedEvent += (client) =>
        {
            Debug.Log($"Client connected: {client}");
        };

        netListener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            byte[] readData = new byte[dataReader.AvailableBytes];
            dataReader.GetBytes(readData, dataReader.AvailableBytes);
            dataReader.Recycle();
            netManager.SendToAll(readData, DeliveryMethod.ReliableSequenced, fromPeer);
        };

        serverStarted = netManager.Start(port);
    }

    public void StopServer()
    {
        serverStarted = false;
        netManager.Stop();

        netListener.ClearConnectionRequestEvent();
        netListener.ClearPeerConnectedEvent();
        netListener.ClearNetworkReceiveEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (serverStarted)
            netManager.PollEvents();
    }
}
