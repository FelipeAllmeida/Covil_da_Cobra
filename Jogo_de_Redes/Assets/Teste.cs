using UnityEngine;
using System;
using System.Collections;

public class Teste : MonoBehaviour
{
    void Start()
    {
        SocketConnector __socket = new SocketConnector();
        Action __callbackFinished = delegate
        {
            Debug.Log("Socket Iniciado");
        };
        __socket.AInitialize();
        __socket.OpenAndTryToConnectToSocket("127.0.0.1", 1300, 2, 120f, true, __callbackFinished, null);
    }
	
}
