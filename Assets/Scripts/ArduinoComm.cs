using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

public class ArduinoComm : MonoBehaviour
{
    public static byte[] DataIn, DataOut;
    Thread sendThread;
    Thread recvThread;
    static bool endofapp=false;

    bool startup = true;
    // Start is called before the first frame update
    void Start()
    {
        if (startup)
        {
            ArduinoComm.DataIn = Encoding.ASCII.GetBytes("0");
            ArduinoComm.DataOut = Encoding.ASCII.GetBytes("0");
            sendThread = new Thread(ArduinoComm.StartSend);
            recvThread = new Thread(ArduinoComm.StartReceive);
            sendThread.Start();
            recvThread.Start();
            startup = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int dataouttemp = (int) Bar.rotationBarFromHelmet*1000;
        ArduinoComm.DataOut = Encoding.ASCII.GetBytes(dataouttemp.ToString());
        Helmet.rotationHelmetFromWorld = Int32.Parse(Encoding.ASCII.GetString(ArduinoComm.DataIn)); //TODO: fix constants
        //Debug.Log(Helmet.rotationHelmetFromWorld);
    }
    private static void StartReceive(int portNrRecv)
    {
        using var udpClientB = new UdpClient(portNrRecv);
        udpClientB.EnableBroadcast = true;
        while (!endofapp)
        {
            var ipep = new IPEndPoint(IPAddress.Any, portNrRecv);
            var buf = udpClientB.Receive(ref ipep);
            lock (DataIn)
            {
                DataIn = buf;
            }


            Console.WriteLine("data received, raw print:" + Encoding.ASCII.GetString(DataIn));
            // Thread.Sleep(500); //TODO: comment this out
        }
        udpClientB.Close();
    }

    public static void StartReceive()
    {
        StartReceive(20666);
    }

    private static void StartSend(int portNrSend)
    {
        using var udpClientA = new UdpClient(portNrSend);
        udpClientA.EnableBroadcast = true;
        byte[] buf;
        while (!endofapp)
        {
            lock (DataOut)
            {
                buf = DataOut;
            }

            udpClientA.Send(buf, buf.Length, "192.168.137.137", portNrSend);
            // Console.WriteLine("one message sent");
            Thread.Sleep(200);
        }
        udpClientA.Close();
    }

    public static void StartSend()
    {
        StartSend(20777);
    }

    void OnApplicationQuit()
    {
        endofapp = true;
    }
}
