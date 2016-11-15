using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;

public class TCPConnector
{
    #region Public Data
    private bool _isConnected = false;
    #endregion
    // O Nome da conexão, não é necessário mas é melhor para observação caso você tenha mais de uma conexão rodando

    #region Private Data
    private TcpClient _tcpClient;
    private NetworkStream _networkStream;
    private StreamWriter _streamWriter;
    private StreamReader _streamReader;

    private string _hostIpAddress;
    private int _port;

    private struct SocketInitializeParameters
    {
        public string ipAddress;
        public int port;
        public float turnTime;
    }
    #endregion

    #region Const Data
    private const string _socketResourcesPath = @"Resources/ConsoleApplication1/ConsoleApplication1/bin/Debug/ConsoleApplication1.exe";
    private const string _socketParametersFilePath = @"Resources/ConsoleApplication1/ConsoleApplication1/bin/Debug/SocketParameters.txt";
    #endregion

    //Tenta iniciar a conexão e cria o socket se necessário
    public void SetupSocket(string p_ipAddress, int p_port, float p_turnTime, bool p_openSocket, Action p_callbackSuccess, Action p_callbackFailed)
    {
        _hostIpAddress = p_ipAddress;
        _port = p_port;
        Action __callbackSetupSocket = delegate
        {
            try
            {
                _tcpClient = new TcpClient(_hostIpAddress, _port);
                _networkStream = _tcpClient.GetStream();
                _streamWriter = new StreamWriter(_networkStream);
                _streamReader = new StreamReader(_networkStream);
                _isConnected = true;
                if (p_callbackSuccess != null) p_callbackSuccess();
            }
            catch (SocketException p_socketException)
            {
                UnityEngine.Debug.Log("Socket error:" + p_socketException);
                if (p_callbackFailed != null)
                    p_callbackFailed();
            }
        };

        if (p_openSocket == true)
        {
            OpenSocket(p_ipAddress, p_port, p_turnTime, __callbackSetupSocket, p_callbackFailed);
        }
        else
        {
            if (__callbackSetupSocket != null) __callbackSetupSocket();
        }
    }

    private void OpenSocket(string p_ipAddress, int p_port, float p_turnTime, Action p_callbackFinish, Action p_callbackFailed = null)
    {
        CreateSocketInitializeParameters(p_ipAddress, p_port, p_turnTime, delegate
        {
            Process __process = new Process();
            __process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            __process.StartInfo.CreateNoWindow = false;
            __process.StartInfo.UseShellExecute = false;

            string __fullPath = Application.dataPath + "/" + _socketResourcesPath;
            __process.StartInfo.FileName = __fullPath;
            try
            {
                __process.Start();
                if (__process.Responding == true)
                {
                    __process.CancelOutputRead();
                }
            }
            catch (Exception p_exception)
            {
                UnityEngine.Debug.LogError(p_exception);
                if (p_callbackFailed != null)
                    p_callbackFailed();
            }
        });
    }

    private void CreateSocketInitializeParameters(string p_ipAddress, int p_port, float p_turnTime, Action p_callbackFinish)
    {
        SocketInitializeParameters __socketInitializeParameters = new SocketInitializeParameters();
        __socketInitializeParameters.ipAddress = p_ipAddress;
        __socketInitializeParameters.port = p_port;
        __socketInitializeParameters.turnTime = p_turnTime;
        
        string __fullPath = Application.dataPath + "/" +  _socketParametersFilePath;
        File.WriteAllText(__fullPath, JsonUtility.ToJson(__socketInitializeParameters));
        if (p_callbackFinish != null)
            p_callbackFinish();
    }
    

    //Envia a mensagem para o socket.
    public void SendData(string p_string)
    {
        if (!_isConnected)
            return;

        string __tempString = p_string + "\r\n";
        _streamWriter.Write(__tempString);
        _streamWriter.Flush();
    }

    //Lê a mensagem recebida pelo servidor.
    public string ReceiveData()
    {
        string __response = string.Empty;
        try
        {
            if (_networkStream.DataAvailable == true)
            {
                byte[] __dataToRead = new Byte[_tcpClient.SendBufferSize];
                _networkStream.Read(__dataToRead, 0, __dataToRead.Length);
                __response = System.Text.Encoding.UTF8.GetString(__dataToRead);
            }
        }
        catch (SocketException p_socketException)
        {
            UnityEngine.Debug.LogError("Socket error:" + p_socketException);
        }
        return __response;
    }

    //Fecha a conexão com o socket
    public void CloseSocket()
    {
        if (_isConnected == false)
            return;
        _streamWriter.Close();
        _streamReader.Close();
        _tcpClient.Close();
        _isConnected = false;
    }

    //Reconecta ao socket
    /* public void ReconectToSocket()
     {
         if (_networkStream.CanRead == false)
         {
             SetupSocket(_hostIpAddress, _port);
         }
     }*/

    public bool GetIsConnected()
    {
        return _isConnected;
    }
}