using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GlobalVariables
{



    public static string _selectedCharacter;
    public static GameObject[] _CharactersList;

    public static string Player_Team_Chosen = "V";
    public static string Player_Name;

    public static char P1_Escolha = 'A';
    public static char P2_Escolha;
    public static Camera cameraAtivaP1;

    public static Camera[] Cameras;
    public static int currentCameraIndex;

    public static Camera cameraAtivaP2;


    public static List<Transform> AllTilesInGame = new List<Transform>();
    public static List<Transform> AllWalkedTiles = new List<Transform>();
    public static List<Transform> AllActionTiles = new List<Transform>();

    public static List<Transform> WalkedMeele = new List<Transform>();
    public static List<Transform> WalkedRange = new List<Transform>();
    public static List<Transform> WalkedMage = new List<Transform>();

    public static List<Transform> ActionMeeleAtk = new List<Transform>();
    public static List<Transform> ActionRangeAtk = new List<Transform>();
    public static List<Transform> ActionMageAtk = new List<Transform>();

    public static List<Transform> ActionMeeleDef = new List<Transform>();
    public static List<Transform> ActionRangeDef = new List<Transform>();
    public static List<Transform> ActionMageDef = new List<Transform>();


    //public static Transform UltimoTileSelecionado;
    public static Dictionary<string, Transform> LastTileSelected = new Dictionary<string, Transform>();

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
    public static bool WAITING_PLAYERS_TURN = false;
    public static bool DO_TURN = false;


    public static SocketConnector __socket;

    // public static SocketConnector __socket;

    //public static List<string> TileProibidos = new List<string>();//.Add("S2007");

}