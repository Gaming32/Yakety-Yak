using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LiteNetLib.Utils;

public class LiteNetClient : MonoBehaviour
{
    EventBasedNetListener netListener;
    NetManager netManager;

    AudioSource audioSource;

    bool clientStarted;

    private void Awake()
    {
        netListener = new EventBasedNetListener();
        netManager = new NetManager(netListener);

        audioSource = GetComponent<AudioSource>();
    }

    public void StartClient(string address, int port, string key)
    {
        netListener.NetworkReceiveEvent += RecieveAudio;

        clientStarted = netManager.Start();
        netManager.Connect(address, port, key);

        StartCoroutine("SendAudio");
    }

    public void StopClient()
    {
        StopCoroutine("SendAudio");
        clientStarted = false;
        netManager.Stop();

        netListener.ClearNetworkReceiveEvent();
    }

    IEnumerator SendAudio()
    {
        NetDataWriter writer = new NetDataWriter();
        while (true) {
            AudioClip audioClip = Microphone.Start("", false, 1, 126);
            yield return new WaitForSecondsRealtime(126f/44100f);
            float[] sendData = new float[126];
            audioClip.GetData(sendData, 0);
            writer.Reset();
            writer.PutArray(sendData);
            netManager.SendToAll(writer, DeliveryMethod.ReliableSequenced);
        }
    }

    void RecieveAudio(NetPeer fromPeer, NetPacketReader dataReader, DeliveryMethod deliveryMethod)
    {
        float[] readData = dataReader.GetFloatArray();
        dataReader.Recycle();

        AudioClip audioClip = AudioClip.Create("PersonMicrophone", 126, 1, 44100, false);
        audioClip.SetData(readData, 0);
        audioSource.PlayOneShot(audioClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (clientStarted)
        {
            netManager.PollEvents();
        }
    }
}
