﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SimpleJSON;


class Server : GameController
{
    private enum SocketState
    {
        RECEIVING_CLIENT_DATA,
        SENDING_CLIENT_DATA
    }

    private SocketState _currentState = SocketState.RECEIVING_CLIENT_DATA;

    private TcpListener _tcpListener;

    private Thread _threadClientAcception;

    private List<ClientData> _listClients;

    private List<Thread> _listClientWaitForResponseThreads;

    private IPAddress _ipAdress;

    private Stopwatch _stopwatchClientStream;
    private TimeSpan _timeSpawn;

    private int _maxClients = 0;
    private float _waitClientResponseTime = 120;

    private bool _isServerRunning = false;
    private bool _isAcceptingNewClients = true;

    private byte[] _bytes;


    struct ClientData
    {
        public TcpClient tcpClient;
        public string id;
        public string clientResponse;
    }

    #region Initialization
    public void Initialize(SocketInitializationData p_socketData)
    {
        _ipAdress = IPAddress.Parse(p_socketData.ipAddress);
        _tcpListener = new TcpListener(_ipAdress, p_socketData.port);
        _listClients = new List<ClientData>();
        _listClientWaitForResponseThreads = new List<Thread>();
        _bytes = new Byte[1024];
        _stopwatchClientStream = new Stopwatch();
        _currentState = SocketState.RECEIVING_CLIENT_DATA;
        _maxClients = p_socketData.maxClients;
        _waitClientResponseTime = p_socketData.turnTime;
        RunServer();
    }
    #endregion

    private void RunServer()
    {
        try
        {
            _isServerRunning = true;
            _tcpListener.Start();
            StartAcceptTcpClientThread(ServerLoop);
        }
        catch (SocketException p_socketException)
        {
            Console.WriteLine("SocketException: {0}", p_socketException);
        }
    }

    private void ServerLoop()
    {
        while (_isServerRunning == true)
        {
            switch (_currentState)
            {
                case SocketState.RECEIVING_CLIENT_DATA:
                    RecivingClientDataState();
                    break;
                case SocketState.SENDING_CLIENT_DATA:
                    break;
            }
        }
    }

    private void RecivingClientDataState()
    {
        if (_stopwatchClientStream.IsRunning == false)
        {
            Console.WriteLine("Waiting client data for {0} seconds", _waitClientResponseTime);
            StartWaitClientsStreamThread(_waitClientResponseTime);
            _stopwatchClientStream.Start();
        }
        else
        {
            _timeSpawn = _stopwatchClientStream.Elapsed;
            if (_timeSpawn.Seconds > _waitClientResponseTime)
            {
                _stopwatchClientStream.Stop();
                _stopwatchClientStream.Reset();
                for (int i = 0; i < _listClientWaitForResponseThreads.Count; i++)
                {
                    if (_listClientWaitForResponseThreads[i].IsAlive == true)
                    {
                        _listClientWaitForResponseThreads[i].Abort();
                    }
                    _listClientWaitForResponseThreads.Clear();
                }
                _currentState = SocketState.SENDING_CLIENT_DATA;
            }
        }
    }

    private void SendingClientDataState()
    {

    }

    private void StopServer()
    {
        _tcpListener.Stop();
    }

    private void StartAcceptTcpClientThread(Action p_callbackFinish)
    {
        Console.WriteLine("Waiting for clients to connect...\n");
        _threadClientAcception = new Thread(new ParameterizedThreadStart(AcceptTcpClientThread));
        Action __callbackThreadFinish = delegate
        {
            _currentState = SocketState.RECEIVING_CLIENT_DATA;
            if (p_callbackFinish != null)
                p_callbackFinish();
        };
        _threadClientAcception.Start(__callbackThreadFinish);
    }

    private string GameBegin()
    {
        JSONClass Node = new JSONClass();
        Node.Add("Request", new JSONData("GAME_BEGIN"));
        return Node["Request"];
    }

    private void AcceptTcpClientThread(object p_callbackFinish)
    {
        while (_isAcceptingNewClients == true)
        {
            if (_listClients.Count >= _maxClients)
            {
                Console.WriteLine("All clients connected, starting game...");

                StreamToClients("1");

                if ((Action)p_callbackFinish != null) ((Action)p_callbackFinish)();

            }
            else
            {
                ClientData __clientData = new ClientData();
                __clientData.tcpClient = _tcpListener.AcceptTcpClient();
                __clientData.id = Guid.NewGuid().ToString();
                _listClients.Add(__clientData);
                Console.WriteLine("New Client Connected, total of: {0}\n", _listClients.Count);
                //((Action)p_callbackFinish)();
            }
        }
    }

    private void StartWaitClientsStreamThread(float p_waitTime)
    {
        for (int i = 0; i < _listClients.Count; i++)
        {
            Console.WriteLine(i);
            Thread __newThread = new Thread(new ParameterizedThreadStart(WaitClientStreamThread));
            __newThread.Start(_listClients[i]);
            _listClientWaitForResponseThreads.Add(__newThread);
        }
    }

    private void WaitClientStreamThread(object p_clientData)
    {
        ClientData __clientData = (ClientData)p_clientData;
        NetworkStream __networkStream = ((ClientData)p_clientData).tcpClient.GetStream();
        int __readCount = __networkStream.Read(_bytes, 0, _bytes.Length);
        if (__readCount != 0)
        {
            string __response = Encoding.ASCII.GetString(_bytes, 0, __readCount);


            Console.WriteLine("Dados recebidos do cliente[{0}]: \n{1}", __clientData.id, __response);
            __clientData.clientResponse = __response;
            _listClients[_listClients.FindIndex(x => x.id == __clientData.id)] = __clientData;
            __networkStream.Flush();


            bool isEnum = __response.Substring(0, 1) == "E";

            ClientData cliente = _listClients[_listClients.FindIndex(x => x.id == __clientData.id)];

            //envia o nome do jogador
            //Se for ENUM
            if (isEnum)
            {
                //GameState __responseENUM = (GameState)Enum.Parse(typeof(GameState), __response);
                //switch (__responseENUM)
                //{

                //    case GameState.REQUEST_PLAYER_NUMBER:
                //        StreamToClients(get_nPlayer().ToString());
                //        break;
                //    case GameState.CAN_START:
                //        if (CanStart())
                //        {
                //            StreamToClients(GameBegin());
                //        }
                //        break;
                //}
            }
            //Se for Dados
            else
            {
                //__response = __response.Substring(1, __response.Length - 1);
                //if ((_listClients.Count - 1) < _maxClients)
                //{
                //    Players(__response, cliente.id);
                //}
                //else
                //{
                //    PlayerOK(char.Parse(__response), cliente.id);
                //    if (CanStart())
                //    {
                //        StreamToClients("START");
                //    }
                //}


            }
            //__networkStream.Close();
        }
    }

    private void StreamToClients(string p_responseToStream)
    {
        for (int i = 0; i < _listClients.Count; i++)
        {
            string __response = p_responseToStream;
            byte[] __dataToSend = Encoding.ASCII.GetBytes(__response);

            NetworkStream __networkStream = _listClients[i].tcpClient.GetStream();
            __networkStream.Write(__dataToSend, 0, __dataToSend.Length);
            __networkStream.Flush();
            Console.WriteLine("Dados enviados cliente[{0}]: \n{1}", _listClients[i].id, __response);
            _currentState = SocketState.RECEIVING_CLIENT_DATA;
        }
    }

    private void MessageHandler(string p_response)
    {
        string __type = JSON.Parse(p_response);
        StreamToClients(p_response);
    }

}
public static class AUtility
{
    public static T ToEnum<T>(this string p_string)
    {
        return (T)Enum.Parse(typeof(T), p_string, true);
    }
}