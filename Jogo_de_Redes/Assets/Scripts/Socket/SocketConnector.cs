using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public class SocketConnector
{

    public enum GameState
    {
        LOBBY_ROOM = 1,
        CAN_START = 2,

        WAITING_PLAYERS_TURN = 3,
        DO_TURN = 4
    }

    //  GameController _GameController = new GameController()

    #region Event Data
    public event Action<string> onSocketResponse;
    #endregion

    #region Private Data
    private TCPConnector _tcpConnection;
    private GameController _GameController;
    //private GameState __responseENUM;


    private bool _isConnected = false;
    private bool _isReading = false;

    private Thread _readStreamThread;
    #endregion

    public void AInitialize()
    {
        _tcpConnection = new TCPConnector();
    }

    public void ApplicationQuitHandler()
    {
        _isConnected = false;
    }

    private bool VerifyIfIsConnectedToSocket()
    {
        return _tcpConnection.GetIsConnected();
    }

    public void TryToConnectToSocket(string p_ipAddress, int p_port, Action p_callbackSuccess, Action p_callbackFailed)
    {
        if (VerifyIfIsConnectedToSocket() == true)
            return;

        Action __callbackFinishSetupSocket = delegate
        {
            _isConnected = true;
            if (p_callbackSuccess != null)
                p_callbackSuccess();
        };

        _tcpConnection.SetupSocket(p_ipAddress, p_port, __callbackFinishSetupSocket, p_callbackFailed);
    }

    public void OpenAndTryToConnectToSocket(string p_ipAddress, int p_port, int p_maxClients, float p_turnTime, bool p_openSocket, Action p_callbackSuccess, Action p_callbackFailed)
    {
        if (VerifyIfIsConnectedToSocket() == true)
            return;
        Action __callbackFinishSetupSocket = delegate
        {
            _isConnected = true;
            if (p_callbackSuccess != null)
                p_callbackSuccess();
        };
        _tcpConnection.OpenAndSetupSocket(p_ipAddress, p_port, p_maxClients, p_turnTime, __callbackFinishSetupSocket, p_callbackFailed);
    }

    public void SendData(string p_string)
    {
        if (VerifyIfIsConnectedToSocket() == false)
        {
            Debug.Log("Not connected");
            return;
        }

        _tcpConnection.SendData(p_string);
    }

    public void StartReadSocketDataThread()
    {
        _isReading = true;
        _readStreamThread = new Thread(ReadSocketDataThread);
        _readStreamThread.Start();
    }

    private void ReadSocketDataThread()
    {
        while (_isReading == true)
        {
            string __response = _tcpConnection.ReceiveData();
            if (__response != string.Empty)
            {

                GlobalVariables.IS_SERVER_READY = true;


                int _en = 0;
                if (int.TryParse(__response, out _en))
                {
                    GameState __responseENUM = (GameState)_en;

                    switch (__responseENUM)
                    {
                        case GameState.LOBBY_ROOM:
                            GlobalVariables.WAITING = true;
                            break;
                        case GameState.CAN_START:
                            GlobalVariables.START = true;
                            break;
                        case GameState.WAITING_PLAYERS_TURN:
                            GlobalVariables.WAITING_PLAYERS_TURN = true;
                            break;
                    }
                }
                //se nao retornar um estado para o jogo, retorna a lista de acoes calculadas
                else
                {
                    GlobalVariables.DO_TURN = true;

                    const int MaxLength = 1000;

                    var removeTresh = __response;
                    if (removeTresh.Length > MaxLength)
                        removeTresh = removeTresh.Substring(0, MaxLength);

                    GlobalVariables._SERVER_RESPONSE = removeTresh;
                }


                if (onSocketResponse != null)
                    onSocketResponse(__response);
            }

        }
    }

    public void StopReadingSocketDataThread()
    {
        _isReading = false;
    }

    public void ReadSocketData()
    {
        string __response = _tcpConnection.ReceiveData();
        if (__response != string.Empty)
        {
            Debug.Log("Socket response: " + __response);
            if (onSocketResponse != null)
                onSocketResponse(__response);
        }
    }
}

