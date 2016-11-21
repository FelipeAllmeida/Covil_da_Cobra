using System;
using System.Collections.Generic;
using System.Linq;
namespace ConsoleApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        { 
            FileReader __fileReader = new FileReader();
            Server __server = new Server();

            SocketInitializationData __socketData = __fileReader.GetSocketInitializationData();



            __server.Initialize(__socketData);
        }
    }
}