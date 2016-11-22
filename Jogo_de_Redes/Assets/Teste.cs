using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Teste : MonoBehaviour
{
    public struct TesteData
    {
        public List<string> listString;
    }
    void Start()
    {
        SocketConnector __socket = new SocketConnector();
        Action __callbackFinished = delegate
        {
            Debug.Log("Socket Iniciado");
            __socket.SendData("Dados :D");
        };
        __socket.AInitialize();
        __socket.TryToConnectToSocket("127.0.0.1", 1300, __callbackFinished, null);
        //__socket.OpenAndTryToConnectToSocket("127.0.0.1", 1300, 1, 120f, true, __callbackFinished, null);
        TesteData __testeData = new TesteData();
        __testeData.listString = new List<string>();
        __testeData.listString.Add("penis");
        __testeData.listString.Add("vagina");
        __socket.SendData(JsonUtility.ToJson(__testeData));
        __socket.onSocketResponse += delegate (string p_response)
        {
            Debug.Log("Socket response: " + p_response);
            __socket.StopReadingSocketDataThread();
        };
        __socket.StartReadSocketDataThread();
        //__socket.ReadSocketData();
    }
	
}
