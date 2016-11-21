using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GlobalVariables
{
    public static string personagemSelecionado;
    public static GameObject[] ListaPersonagens;

    public static char P1_Escolha = 'A';
    public static char P2_Escolha;
    public static Camera cameraAtivaP1;

    public static Camera[] Cameras;
    public static int currentCameraIndex;

    public static Camera cameraAtivaP2;


    public static List<Transform> TilesEmJogo = new List<Transform>();
    public static List<Transform> tilesCaminhados = new List<Transform>();
    public static List<Transform> tilesAcoes = new List<Transform>();

    //public static Transform UltimoTileSelecionado;
    public static Dictionary<string, Transform> UltimoTileSelecionado = new Dictionary<string, Transform>();

    // public static List<string> GlobalTileColisor = null;
    public static Dictionary<string, string> GlobalTileColisor = new Dictionary<string, string>();
    public static Dictionary<string, bool> ClassAction = new Dictionary<string, bool>();

    public static bool PlayerAtk = false;
    public static bool PlayerDef = false;

    public static bool IS_SERVER_READY = false;
    public static string _SERVER_RESPONSE;
    public static bool START = false;
    public static bool WAITING = false;
    public static string PlayerName;


    public static SocketConnector __socket;



       //public static List<string> TileProibidos = new List<string>();//.Add("S2007");

}