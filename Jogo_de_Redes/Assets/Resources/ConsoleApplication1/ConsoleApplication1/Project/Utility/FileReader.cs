#define DEBUGGING
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

class FileReader
{
#if DEBUGGING == false
    private const string __socketParametersPath = @"Assets/Resources/ConsoleApplication1/ConsoleApplication1/bin/Debug/SocketParameters.txt";
#else
    private const string __socketParametersPath = @"SocketParameters.txt";
#endif

    public SocketInitializationData GetSocketInitializationData()
    {

        SocketInitializationData __socketData = new SocketInitializationData();

        JSONClass rootNode = new JSONClass();

        rootNode.Add("ipAddress", new JSONData("127.0.0.1"));
        rootNode.Add("port", new JSONData("1300"));
        rootNode.Add("maxClients", new JSONData("1"));
        rootNode.Add("turnTime", new JSONData("120"));


        __socketData.ipAddress = rootNode["ipAddress"];
        __socketData.port = rootNode["port"].AsInt;
        __socketData.turnTime = rootNode["turnTime"].AsFloat;
        __socketData.maxClients = rootNode["maxClients"].AsInt;
        return __socketData;
    }
}