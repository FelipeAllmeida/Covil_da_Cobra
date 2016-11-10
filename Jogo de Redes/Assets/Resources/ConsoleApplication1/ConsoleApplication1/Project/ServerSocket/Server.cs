using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SimpleJSON;


class Server
{
    private TcpListener _tcpListener;

    private List<ClientData> _listTcpClients;

    private IPAddress _ipAdress;

    private int _serverPort = 1300;
    private int _connectionNumber = 0;

    private bool _isServerRunning = false;
    private bool _isAcceptingNewClients = true;

    private Byte[] _bytes;

    Thread _threadClientAcception;
    Dictionary<string, Thread> _dictStreamToClientThreads;

    struct ClientData
    {
        public TcpClient tcpClient;
        public string id;
        public string clientResponse;
    }

    #region Initialization
    public void Initialize()
    {
        _ipAdress = IPAddress.Parse("127.0.0.1");
        _tcpListener = new TcpListener(_ipAdress, _serverPort);
        _listTcpClients = new List<ClientData>();
        _dictStreamToClientThreads = new Dictionary<string, Thread>();
        _bytes = new Byte[1024];
        RunServer();
    }
    #endregion

    private void RunServer()
    {
        try
        {
            _isServerRunning = true;
            _tcpListener.Start();
            StartAcceptTcpClientThread();
            Console.WriteLine("Socket iniciado");
        }
        catch (SocketException p_socketException)
        {
            Console.Write("SocketException: {0}", p_socketException);
        }
    }

    private void StopServer()
    {
        _tcpListener.Stop();
    }

    private void StartAcceptTcpClientThread()
    {
        _threadClientAcception = new Thread(AcceptTcpClientThread);
        _threadClientAcception.Start();
    }

    private void AcceptTcpClientThread()
    {
        while (_isAcceptingNewClients == true)
        {
            ClientData __clientData = new ClientData();
            __clientData.tcpClient = _tcpListener.AcceptTcpClient();
            __clientData.id = Guid.NewGuid().ToString();
            _connectionNumber++;
            _listTcpClients.Add(__clientData);
            Console.WriteLine("New Client Connected, total of: " + _listTcpClients.Count + "\n");
            StartWaitForClientStreamThread(__clientData);
        }
    }

    private void StartWaitForClientStreamThread(ClientData p_clientData)
    {
        Thread __newThread = new Thread(new ParameterizedThreadStart(WaitForClientStreamThread));
        __newThread.Start(p_clientData);
        _dictStreamToClientThreads.Add(p_clientData.id, __newThread);
    }

    private void WaitForClientStreamThread(object p_clientData)
    {
        while (_isServerRunning)
        {
            ClientData __clientData = (ClientData)p_clientData;
            NetworkStream __networkStream = ((ClientData)p_clientData).tcpClient.GetStream();
            int __readCount = __networkStream.Read(_bytes, 0, _bytes.Length);
            if (__readCount != 0)
            {
                string __response = Encoding.ASCII.GetString(_bytes, 0, __readCount);
                Console.Write("Dados recebidos do cliente[" + __clientData.id + "]: \n" + __response);
                __clientData.clientResponse = __response;
                _listTcpClients[_listTcpClients.FindIndex(x => x.id == __clientData.id)] = __clientData;
                __networkStream.Flush();
                MessageHandler(__response);
            }            
        }
    }

    private void StreamToClients(string p_responseToStream)
    {                 
        for (int i = 0;i < _listTcpClients.Count;i++)
        {
            string __response = p_responseToStream;
            byte[] __dataToSend = Encoding.ASCII.GetBytes(__response);

            NetworkStream __networkStream = _listTcpClients[i].tcpClient.GetStream();
            __networkStream.Write(__dataToSend, 0, __dataToSend.Length);
            __networkStream.Flush();
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