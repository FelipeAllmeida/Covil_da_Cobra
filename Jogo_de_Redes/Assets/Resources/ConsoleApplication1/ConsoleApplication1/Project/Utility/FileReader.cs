using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

class FileReader
{
    private const string __socketParametersPath = "SocketParameters.txt";

    public SocketInitializationData GetSocketInitializationData()
    {
        SocketInitializationData __socketData = new SocketInitializationData();
        string __jsonString = File.ReadAllText(__socketParametersPath);
        var __output = SimpleJSON.JSON.Parse(__jsonString);
        __socketData.ipAddress = __output["ipAddress"];
        __socketData.port = __output["port"].AsInt;
        __socketData.turnTime = __output["turnTime"].AsFloat;
        return __socketData;
    }
}