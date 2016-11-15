using UnityEngine;
using System;
using System.Threading;
using System.Collections;

public class SocketConnector
{
    #region Event Data
    public event Action<string> onSocketResponse;
    #endregion

    #region Private Data
    private TCPConnector _tcpConnection;

    private string _ipAddress = "127.0.0.1";

    private int _port = 1300;

    private bool _isConnected = false;

    private Thread _readStreamThread;
    #endregion

    public void AInitialize()
    {
        _tcpConnection = new TCPConnector();
    }

    public void ApplicationQuitHandler()
    {
        _isConnected = false;
        //_readStreamThread.Abort();
    }

    private bool VerifyIfIsConnectedToSocket()
    {
        return _tcpConnection.GetIsConnected();
    }

    public void TryConnectToServer(string p_ipAddress, int p_port, float p_turnTime, bool p_openSocket, Action p_callbackSuccess, Action p_callbackFailed)
    {
        if (VerifyIfIsConnectedToSocket() == true)
            return;
        Action __callbackFinishSetupSocket = delegate
        {
            _isConnected = true;
            if (p_callbackSuccess != null)
                p_callbackSuccess();
        };
        _tcpConnection.SetupSocket(_ipAddress, _port, p_turnTime, p_openSocket, __callbackFinishSetupSocket, p_callbackFailed);
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
        _readStreamThread = new Thread(ReadSocketDataThread);
        _readStreamThread.Start();
    }

    private void ReadSocketDataThread()
    {
        while (_isConnected == true)
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

