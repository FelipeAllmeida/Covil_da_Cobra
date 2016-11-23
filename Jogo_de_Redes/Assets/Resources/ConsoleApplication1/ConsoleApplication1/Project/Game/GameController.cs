using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SimpleJSON;
using System.ComponentModel;
using ConsoleApplication1.Project.Game;


internal class GameController
{

    public enum GameState
    {
        CONNECTING_PLAYERS,
        CHOOSING_TEAM,
        START_GAME,
        ON,
        TURN_RESPONSE,
        REQUEST_TURN,
        DO_TURN
    }

    public GameState CURRENT_STATE = GameState.CONNECTING_PLAYERS;


    public struct PlayerOne
    {
        public string Name;
        public string IDCliente;
        public string Team;
        public bool TurnSend;

        public int MeeleId;
        public int MeeleAP;
        public int MeeleAtk;
        public int MeeleDef;
        public int MeeleHP;

        public int MeeleCurrentAP;
        public int MeeleCurrentHP;
        public string MeeleCurrentTile;

        public int ArcherId;
        public int ArcherAP;
        public int ArcherAtk;
        public int ArcherDef;
        public int ArcherHP;

        public int ArcherCurrentAP;
        public int ArcherCurrentHP;
        public string ArcherCurrentTile;

        public int MageId;
        public int MageAP;
        public int MageAtk;
        public int MageDef;
        public int MageHP;

        public int MageCurrentAP;
        public int MageCurrentHP;
        public string MageCurrentTile;
    }

    public struct PlayerTwo
    {
        public string Name;
        public string IDCliente;
        public string Team;
        public bool TurnSend;

        public int MeeleId;
        public int MeeleAP;
        public int MeeleAtk;
        public int MeeleDef;
        public int MeeleHP;

        public int MeeleCurrentAP;
        public int MeeleCurrentHP;
        public string MeeleCurrentTile;

        public int ArcherId;
        public int ArcherAP;
        public int ArcherAtk;
        public int ArcherDef;
        public int ArcherHP;

        public int ArcherCurrentAP;
        public int ArcherCurrentHP;
        public string ArcherCurrentTile;

        public int MageId;
        public int MageAP;
        public int MageAtk;
        public int MageDef;
        public int MageHP;

        public int MageCurrentAP;
        public int MageCurrentHP;
        public string MageCurrentTile;
    }


    public PlayerOne P1;
    public PlayerTwo P2;

    public int _PlayersConnected = 0;
    public int _PlayersReady = 0;

    public SimpleJSON.JSONArray ListToSend = new SimpleJSON.JSONArray();
    //public List<string> ListToSend = new List<string>();
    public void GameLoop(object p_callbackResponseTurn)
    {

        if (CURRENT_STATE == GameState.REQUEST_TURN)
        {
            P1.TurnSend = false;
            P2.TurnSend = false;

            var TempWalkP1 = new Dictionary<int, List<string>>();
            var List_TempWalkP1 = new List<string>();
            TempWalkP1.Add(1, List_TempWalkP1);
            TempWalkP1.Add(2, List_TempWalkP1);
            TempWalkP1.Add(3, List_TempWalkP1);

            var TempDefP1 = new Dictionary<int, List<string>>();
            var ListTempDefP1 = new List<string>();
            TempDefP1.Add(1, ListTempDefP1);
            TempDefP1.Add(2, ListTempDefP1);
            TempDefP1.Add(3, ListTempDefP1);

            var TempAtkP1 = new Dictionary<int, List<string>>();
            var ListTempAtkP1 = new List<string>();
            TempAtkP1.Add(1, ListTempAtkP1);
            TempAtkP1.Add(2, ListTempAtkP1);
            TempAtkP1.Add(3, ListTempAtkP1);


            var TempWalkP2 = new Dictionary<int, List<string>>();
            var List_TempWalkP2 = new List<string>();
            TempWalkP2.Add(4, List_TempWalkP2);
            TempWalkP2.Add(5, List_TempWalkP2);
            TempWalkP2.Add(6, List_TempWalkP2);

            var TempDefP2 = new Dictionary<int, List<string>>();
            var ListTempDefP2 = new List<string>();
            TempDefP2.Add(4, ListTempDefP2);
            TempDefP2.Add(5, ListTempDefP2);
            TempDefP2.Add(6, ListTempDefP2);

            var TempAtkP2 = new Dictionary<int, List<string>>();
            var ListTempAtkP2 = new List<string>();
            TempAtkP2.Add(4, ListTempAtkP2);
            TempAtkP2.Add(5, ListTempAtkP2);
            TempAtkP2.Add(6, ListTempAtkP2);



            foreach (var _character in _ResponseP1)
            {
                if (_character.id == 1)
                {
                    P1.MeeleCurrentTile = _character.CurrentTile;
                }
                if (_character.id == 2)
                {
                    P1.ArcherCurrentTile = _character.CurrentTile;
                }
                if (_character.id == 3)
                {
                    P1.MageCurrentTile = _character.CurrentTile;
                }


                foreach (var walked in _character.WalkedTiles)
                {
                    List_TempWalkP1.Add(walked);
                    TempWalkP1[_character.id] = List_TempWalkP1;
                }

                foreach (var def in _character.ActionTilesDef)
                {
                    ListTempDefP1.Add(def);
                    TempDefP1[_character.id] = ListTempDefP1;
                }

                foreach (var atk in _character.ActionTilesAtk)
                {
                    ListTempAtkP1.Add(atk);
                    TempAtkP1[_character.id] = ListTempAtkP1;
                }

            }

            foreach (var _character in _ResponseP2)
            {
                if (_character.id == 4)
                {
                    P2.MeeleCurrentTile = _character.CurrentTile;
                }
                if (_character.id == 5)
                {
                    P2.ArcherCurrentTile = _character.CurrentTile;
                }
                if (_character.id == 6)
                {
                    P2.MageCurrentTile = _character.CurrentTile;
                }

                foreach (var walked in _character.WalkedTiles)
                {
                    List_TempWalkP2.Add(walked);
                    TempWalkP2[_character.id] = List_TempWalkP2;
                }

                foreach (var def in _character.ActionTilesDef)
                {
                    ListTempDefP2.Add(def);
                    TempDefP2[_character.id] = ListTempDefP2;
                }

                foreach (var atk in _character.ActionTilesAtk)
                {
                    ListTempAtkP2.Add(atk);
                    TempAtkP2[_character.id] = ListTempAtkP2;
                }

            }

            //15 action max
            for (int turno = 0; turno < 15; turno++)
            {
                var CHAR1W = TempWalkP1[1].Count >= turno + 1 ? TempWalkP1[1][turno] : string.Empty;
                var CHAR1A = TempAtkP1[1].Count >= turno + 1 ? TempAtkP1[1][turno] : string.Empty;
                var CHAR1D = TempDefP1[1].Count >= turno + 1 ? TempDefP1[1][turno] : string.Empty;

                var CHAR2W = TempWalkP1[2].Count >= turno + 1 ? TempWalkP1[2][turno] : string.Empty;
                var CHAR2A = TempAtkP1[2].Count >= turno + 1 ? TempAtkP1[2][turno] : string.Empty;
                var CHAR2D = TempDefP1[2].Count >= turno + 1 ? TempDefP1[2][turno] : string.Empty;

                var CHAR3W = TempWalkP1[3].Count >= turno + 1 ? TempWalkP1[3][turno] : string.Empty;
                var CHAR3A = TempAtkP1[3].Count >= turno + 1 ? TempAtkP1[3][turno] : string.Empty;
                var CHAR3D = TempDefP1[3].Count >= turno + 1 ? TempDefP1[3][turno] : string.Empty;

                var CHAR4W = TempWalkP2[4].Count >= turno + 1 ? TempWalkP2[4][turno] : string.Empty;
                var CHAR4A = TempAtkP2[4].Count >= turno + 1 ? TempAtkP2[4][turno] : string.Empty;
                var CHAR4D = TempDefP2[4].Count >= turno + 1 ? TempDefP2[4][turno] : string.Empty;

                var CHAR5W = TempWalkP2[5].Count >= turno + 1 ? TempWalkP2[5][turno] : string.Empty;
                var CHAR5A = TempAtkP2[5].Count >= turno + 1 ? TempAtkP2[5][turno] : string.Empty;
                var CHAR5D = TempDefP2[5].Count >= turno + 1 ? TempDefP2[5][turno] : string.Empty;

                var CHAR6W = TempWalkP2[6].Count >= turno + 1 ? TempWalkP2[6][turno] : string.Empty;
                var CHAR6A = TempAtkP2[6].Count >= turno + 1 ? TempAtkP2[6][turno] : string.Empty;
                var CHAR6D = TempDefP2[6].Count >= turno + 1 ? TempDefP2[6][turno] : string.Empty;


                //Team 1
                if (CHAR1W == CHAR4A || CHAR1W == CHAR5A || CHAR1W == CHAR6A)
                {
                    //dano
                    if (CHAR1W == CHAR4A)
                    {
                        P1.MeeleCurrentHP -= P2.MeeleAtk;
                    }
                    if (CHAR1W == CHAR5A)
                    {
                        P1.MeeleCurrentHP -= P2.ArcherAtk;
                    }
                    if (CHAR1W == CHAR6A)
                    {
                        P1.MeeleCurrentHP -= P2.MageAtk;
                    }
                }
                if (CHAR2W == CHAR4A || CHAR2W == CHAR5A || CHAR2W == CHAR6A)
                {
                    //dano
                    if (CHAR2W == CHAR4A)
                    {
                        P1.ArcherCurrentHP -= P2.MeeleAtk;
                    }
                    if (CHAR2W == CHAR5A)
                    {
                        P1.ArcherCurrentHP -= P2.ArcherAtk;
                    }
                    if (CHAR2W == CHAR6A)
                    {
                        P1.ArcherCurrentHP -= P2.MageAtk;
                    }
                }
                if (CHAR3W == CHAR4A || CHAR3W == CHAR5A || CHAR3W == CHAR6A)
                {
                    //dano
                    if (CHAR3W == CHAR4A)
                    {
                        P1.MageCurrentHP -= P2.MeeleAtk;
                    }
                    if (CHAR3W == CHAR5A)
                    {
                        P1.MageCurrentHP -= P2.ArcherAtk;
                    }
                    if (CHAR3W == CHAR6A)
                    {
                        P1.MageCurrentHP -= P2.MageAtk;
                    }
                }



                if (CHAR1A == CHAR4D || CHAR1A == CHAR5D || CHAR1A == CHAR6D)
                {
                    //def
                    if (CHAR1A == CHAR4D)
                    {
                        P2.MeeleCurrentHP -= P1.MeeleAtk - P2.MeeleDef;
                    }
                    if (CHAR1A == CHAR5A)
                    {
                        P2.ArcherCurrentHP -= P1.MeeleAtk - P2.ArcherDef;
                    }
                    if (CHAR1A == CHAR6A)
                    {
                        P2.MageCurrentHP -= P1.MeeleAtk - P2.MageDef;
                    }
                }
                if (CHAR2A == CHAR4D || CHAR2A == CHAR5D || CHAR2A == CHAR6D)
                {
                    //def
                    if (CHAR2A == CHAR4D)
                    {
                        P2.MeeleCurrentHP -= P1.ArcherAtk - P2.MeeleDef;
                    }
                    if (CHAR2A == CHAR5A)
                    {
                        P2.ArcherCurrentHP -= P1.ArcherAtk - P2.ArcherDef;
                    }
                    if (CHAR2A == CHAR6A)
                    {
                        P2.MageCurrentHP -= P1.ArcherAtk - P2.MageDef;
                    }
                }
                if (CHAR3A == CHAR4D || CHAR3A == CHAR5D || CHAR3A == CHAR6D)
                {
                    //def
                    if (CHAR3A == CHAR4D)
                    {
                        P2.MeeleCurrentHP -= P1.MageAtk - P2.MeeleDef;
                    }
                    if (CHAR3A == CHAR5A)
                    {
                        P2.ArcherCurrentHP -= P1.MageAtk - P2.ArcherDef;
                    }
                    if (CHAR3A == CHAR6A)
                    {
                        P2.MageCurrentHP -= P1.MageAtk - P2.MageDef;
                    }
                }

                if (CHAR1A == CHAR4A || CHAR1A == CHAR5A || CHAR1A == CHAR6A)
                {
                    //atk atk
                    if (CHAR1A == CHAR4A)
                    {
                        P1.MeeleCurrentHP -= P1.MeeleAtk - P2.MeeleAtk;
                        P2.MeeleCurrentHP -= P1.MeeleAtk - P2.MeeleAtk;
                    }
                    if (CHAR1A == CHAR5A)
                    {
                        P1.MeeleCurrentHP -= P1.MeeleAtk - P2.ArcherAtk;
                        P2.ArcherCurrentHP -= P1.MeeleAtk - P2.ArcherAtk;
                    }
                    if (CHAR1A == CHAR6A)
                    {
                        P1.MeeleCurrentHP -= P1.MeeleAtk - P2.MageAtk;
                        P2.MageCurrentHP -= P1.MeeleAtk - P2.MageAtk;
                    }
                }
                if (CHAR2A == CHAR4A || CHAR2A == CHAR5A || CHAR2A == CHAR6A)
                {
                    //atk atk
                    if (CHAR2A == CHAR4A)
                    {
                        P1.ArcherCurrentHP -= P1.ArcherAtk - P2.MeeleAtk;
                        P2.MeeleCurrentHP -= P1.ArcherAtk - P2.MeeleAtk;
                    }
                    if (CHAR2A == CHAR5A)
                    {
                        P1.ArcherCurrentHP -= P1.ArcherAtk - P2.ArcherAtk;
                        P2.ArcherCurrentHP -= P1.ArcherAtk - P2.ArcherAtk;
                    }
                    if (CHAR2A == CHAR6A)
                    {
                        P1.ArcherCurrentHP -= P1.ArcherAtk - P2.MageAtk;
                        P2.MageCurrentHP -= P1.ArcherAtk - P2.MageAtk;
                    }
                }
                if (CHAR3A == CHAR4A || CHAR3A == CHAR5A || CHAR3A == CHAR6A)
                {
                    //atk atk
                    if (CHAR3A == CHAR4A)
                    {
                        P1.MageCurrentHP -= P1.MageAtk - P2.MeeleAtk;
                        P2.MeeleCurrentHP -= P1.MageAtk - P2.MeeleAtk;
                    }
                    if (CHAR3A == CHAR5A)
                    {
                        P1.MageCurrentHP -= P1.MageAtk - P2.ArcherAtk;
                        P2.ArcherCurrentHP -= P1.MageAtk - P2.ArcherAtk;
                    }
                    if (CHAR3A == CHAR6A)
                    {
                        P1.MageCurrentHP -= P1.MageAtk - P2.MageAtk;
                        P2.MageCurrentHP -= P1.MageAtk - P2.MageAtk;
                    }
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                //Team 2
                if (CHAR4W == CHAR1A || CHAR4W == CHAR2A || CHAR4W == CHAR3A)
                {
                    //dano
                    if (CHAR4W == CHAR1A)
                    {
                        P2.MeeleCurrentHP -= P1.MeeleAtk;
                    }
                    if (CHAR4W == CHAR2A)
                    {
                        P2.MeeleCurrentHP -= P1.ArcherAtk;
                    }
                    if (CHAR4W == CHAR3A)
                    {
                        P2.MeeleCurrentHP -= P1.MageAtk;
                    }
                }
                if (CHAR5W == CHAR1A || CHAR5W == CHAR2A || CHAR5W == CHAR3A)
                {
                    //dano
                    if (CHAR5W == CHAR1A)
                    {
                        P2.ArcherCurrentHP -= P1.MeeleAtk;
                    }
                    if (CHAR5W == CHAR2A)
                    {
                        P2.ArcherCurrentHP -= P1.ArcherAtk;
                    }
                    if (CHAR5W == CHAR3A)
                    {
                        P2.ArcherCurrentHP -= P1.MageAtk;
                    }
                }
                if (CHAR6W == CHAR1A || CHAR6W == CHAR2A || CHAR6W == CHAR3A)
                {
                    //dano
                    if (CHAR6W == CHAR1A)
                    {
                        P2.MageCurrentHP -= P1.MeeleAtk;
                    }
                    if (CHAR6W == CHAR2A)
                    {
                        P2.MageCurrentHP -= P1.ArcherAtk;
                    }
                    if (CHAR6W == CHAR3A)
                    {
                        P2.MageCurrentHP -= P1.MageAtk;
                    }
                }



                if (CHAR4A == CHAR1D || CHAR4A == CHAR2D || CHAR4A == CHAR3D)
                {
                    //def
                    if (CHAR4A == CHAR1D)
                    {
                        P1.MeeleCurrentHP -= P2.MeeleAtk - P1.MeeleDef;
                    }
                    if (CHAR4A == CHAR2D)
                    {
                        P1.ArcherCurrentHP -= P2.MeeleAtk - P1.ArcherDef;
                    }
                    if (CHAR4A == CHAR3D)
                    {
                        P1.MageCurrentHP -= P2.MeeleAtk - P1.MageDef;
                    }
                }
                if (CHAR5A == CHAR1D || CHAR5A == CHAR2D || CHAR5A == CHAR3D)
                {
                    //def
                    if (CHAR5A == CHAR1D)
                    {
                        P1.MeeleCurrentHP -= P2.ArcherAtk - P1.MeeleDef;
                    }
                    if (CHAR5A == CHAR2D)
                    {
                        P1.ArcherCurrentHP -= P2.ArcherAtk - P1.ArcherDef;
                    }
                    if (CHAR5A == CHAR3D)
                    {
                        P1.MageCurrentHP -= P2.ArcherAtk - P1.MageDef;
                    }
                }
                if (CHAR6A == CHAR1D || CHAR6A == CHAR2D || CHAR6A == CHAR3D)
                {
                    //def
                    if (CHAR6A == CHAR1D)
                    {
                        P1.MeeleCurrentHP -= P2.MageAtk - P1.MeeleDef;
                    }
                    if (CHAR6A == CHAR2D)
                    {
                        P1.ArcherCurrentHP -= P2.MageAtk - P1.ArcherDef;
                    }
                    if (CHAR6A == CHAR3D)
                    {
                        P1.MageCurrentHP -= P2.MageAtk - P1.MageDef;
                    }
                }

            }

            JSONClass rootNode = new JSONClass();
            rootNode.Add("id", new JSONData(1));
            rootNode.Add("CurrentHP", new JSONData(P1.MeeleCurrentHP));
            rootNode.Add("CurrentAP", new JSONData(P1.MeeleCurrentAP));
            rootNode.Add("CurrentTile", new JSONData(P1.MeeleCurrentTile));
            ListToSend.Add(rootNode);

            rootNode = new JSONClass();
            rootNode.Add("id", new JSONData(2));
            rootNode.Add("CurrentHP", new JSONData(P1.ArcherCurrentHP));
            rootNode.Add("CurrentAP", new JSONData(P1.ArcherCurrentAP));
            rootNode.Add("CurrentTile", new JSONData(P1.ArcherCurrentTile));
            ListToSend.Add(rootNode);

            rootNode = new JSONClass();
            rootNode.Add("id", new JSONData(3));
            rootNode.Add("CurrentHP", new JSONData(P1.MageCurrentHP));
            rootNode.Add("CurrentAP", new JSONData(P1.MageCurrentAP));
            rootNode.Add("CurrentTile", new JSONData(P1.MageCurrentTile));
            ListToSend.Add(rootNode);

            rootNode = new JSONClass();
            rootNode.Add("id", new JSONData(4));
            rootNode.Add("CurrentHP", new JSONData(P2.MeeleCurrentHP));
            rootNode.Add("CurrentAP", new JSONData(P2.MeeleCurrentAP));
            rootNode.Add("CurrentTile", new JSONData(P2.MeeleCurrentTile));
            ListToSend.Add(rootNode);

            rootNode = new JSONClass();
            rootNode.Add("id", new JSONData(5));
            rootNode.Add("CurrentHP", new JSONData(P2.ArcherCurrentHP));
            rootNode.Add("CurrentAP", new JSONData(P2.ArcherCurrentAP));
            rootNode.Add("CurrentTile", new JSONData(P2.ArcherCurrentTile));
            ListToSend.Add(rootNode);

            rootNode = new JSONClass();
            rootNode.Add("id", new JSONData(6));
            rootNode.Add("CurrentHP", new JSONData(P2.MageCurrentHP));
            rootNode.Add("CurrentAP", new JSONData(P2.MageCurrentAP));
            rootNode.Add("CurrentTile", new JSONData(P2.MageCurrentTile));
            ListToSend.Add(rootNode);


            CURRENT_STATE = GameState.ON;
            if ((Action)p_callbackResponseTurn != null) ((Action)p_callbackResponseTurn)();
        }
    }

    List<CharactersModel> _ResponseP1 = new List<CharactersModel>();
    List<CharactersModel> _ResponseP2 = new List<CharactersModel>();

    public string GameLogic(string IDCLiente = null, string _Turn = null)
    {
        
        if (CURRENT_STATE == GameState.ON)
        {
            if (P1.IDCliente == IDCLiente && !P1.TurnSend)
            {
                ListToSend = new SimpleJSON.JSONArray();
                //ListToSend = new List<string>();

                var p_JsonResponseArray = JSONArray.Parse(_Turn).AsArray;
                for (int i = 0; i < p_JsonResponseArray.Count; i++)
                {
                    CharactersModel _char_response = new CharactersModel();

                    JSONNode _responseJSON = JSONNode.Parse(p_JsonResponseArray[i]);

                    int id_Character = _responseJSON["id"].AsInt;

                    _char_response.CurrentTile = _responseJSON["CurrentTile"].Value;
                    Console.WriteLine(_char_response.CurrentTile);

                    int CurrentHP_Character = _responseJSON["CurrentHP"].AsInt;
                    int CurrentAP_Character = _responseJSON["CurrentAP"].AsInt;

                    if (id_Character == P1.MeeleId)
                    {
                        if (P1.MeeleCurrentAP != CurrentAP_Character || P1.MeeleCurrentHP != CurrentHP_Character)
                        {
                            //"cheating?";
                        }
                    }
                    else if (id_Character == P1.ArcherId)
                    {
                        if (P1.ArcherCurrentAP != CurrentAP_Character || P1.ArcherCurrentHP != CurrentHP_Character)
                        {
                            //"cheating?";
                        }
                    }
                    else if (id_Character == P1.MageId)
                    {
                        if (P1.MageCurrentAP != CurrentAP_Character || P1.MageCurrentHP != CurrentHP_Character)
                        {
                            //"cheating?";
                        }
                    }

                    //List<string> WalkedTiles_Character = _responseJSON["WalkedTiles"].Value;
                    //List<string> ActionTilesAtk_Character = _responseJSON["ActionTilesAtk"].Value;
                    //List<string> ActionTilesDef_Character = _responseJSON["ActionTilesDef"].Value;
                    var walk = _responseJSON["WalkedTiles"];
                    for (int w_index = 0; w_index < walk.Count; w_index++)
                    {
                        _char_response.WalkedTiles.Add(walk[w_index]);
                        _char_response.CurrentTile = walk[w_index];
                    }

                    var actionAtk = _responseJSON["ActionTilesAtk"];
                    for (int aa_index = 0; aa_index < actionAtk.Count; aa_index++)
                    {
                        _char_response.ActionTilesAtk.Add(actionAtk[aa_index]);
                    }

                    var actionDef = _responseJSON["ActionTilesDef"];
                    for (int ad_index = 0; ad_index < actionDef.Count; ad_index++)
                    {
                        _char_response.ActionTilesDef.Add(actionDef[ad_index]);
                    }

                    _char_response.id = id_Character;
                    _char_response.CurrentHP = CurrentHP_Character;
                    _char_response.CurrentAP = 10;



                    _ResponseP1.Add(_char_response);

                }
                P1.TurnSend = true;
               // CURRENT_STATE = GameState.REQUEST_TURN;
                return "Dados do Turno recebidos";
            }
            if (P2.IDCliente == IDCLiente && !P2.TurnSend)
            {
                ListToSend = new SimpleJSON.JSONArray();
                //ListToSend = new List<string>();

                var p_JsonResponseArray = JSONArray.Parse(_Turn).AsArray;
                for (int i = 0; i < p_JsonResponseArray.Count; i++)
                {
                    CharactersModel _char_response = new CharactersModel();

                    JSONClass rootNode = new JSONClass();

                    JSONNode _responseJSON = JSONNode.Parse(p_JsonResponseArray[i]);

                    int id_Character = _responseJSON["id"].AsInt;

                    _char_response.CurrentTile = _responseJSON["CurrentTile"].Value;
                    Console.WriteLine(_char_response.CurrentTile);

                    int CurrentHP_Character = _responseJSON["CurrentHP"].AsInt;
                    int CurrentAP_Character = _responseJSON["CurrentAP"].AsInt;

                    if (id_Character == P2.MeeleId)
                    {
                        if (P2.MeeleCurrentAP != CurrentAP_Character || P2.MeeleCurrentHP != CurrentHP_Character)
                        {
                            //"cheating?";
                        }
                    }
                    else if (id_Character == P2.ArcherId)
                    {
                        if (P2.ArcherCurrentAP != CurrentAP_Character || P2.ArcherCurrentHP != CurrentHP_Character)
                        {
                            //"cheating?";
                        }
                    }
                    else if (id_Character == P2.MageId)
                    {
                        if (P2.MageCurrentAP != CurrentAP_Character || P2.MageCurrentHP != CurrentHP_Character)
                        {
                            //"cheating?";
                        }
                    }

                    //List<string> WalkedTiles_Character = _responseJSON["WalkedTiles"].Value;
                    //List<string> ActionTilesAtk_Character = _responseJSON["ActionTilesAtk"].Value;
                    //List<string> ActionTilesDef_Character = _responseJSON["ActionTilesDef"].Value;
                    var walk = _responseJSON["WalkedTiles"];
                    for (int w_index = 0; w_index < walk.Count; w_index++)
                    {
                        _char_response.WalkedTiles.Add(walk[w_index]);
                        _char_response.CurrentTile = walk[w_index];
                    }

                    var actionAtk = _responseJSON["ActionTilesAtk"];
                    for (int aa_index = 0; aa_index < actionAtk.Count; aa_index++)
                    {
                        _char_response.ActionTilesAtk.Add(actionAtk[aa_index]);
                    }

                    var actionDef = _responseJSON["ActionTilesDef"];
                    for (int ad_index = 0; ad_index < actionDef.Count; ad_index++)
                    {
                        _char_response.ActionTilesDef.Add(actionDef[ad_index]);
                    }

                    _char_response.id = id_Character;
                    _char_response.CurrentHP = CurrentHP_Character;
                    _char_response.CurrentAP = 10;


                 
                    _ResponseP2.Add(_char_response);

                    //  ListToSend.Add(rootNode);
                }
                P2.TurnSend = true;
                return "Dados do Turno recebidos";
            }


        }
        return null;
    }


    public void PlayerTeam(string Team, string idcliente)
    {
        _PlayersReady++;
        if (P1.IDCliente == idcliente)
        {
            P1.Team = Team;
        }
        else if (P2.IDCliente == idcliente)
        {
            P2.Team = Team;
        }

        if (_PlayersReady == 2)
        {
            CURRENT_STATE = GameState.START_GAME;
        }

    }

    public void setIdOrder(string idCliente)
    {
        _PlayersConnected++;
        if (_PlayersConnected == 1)
        {
            PlayerOneController(string.Empty, idCliente);
        }
        else
        {
            PlayerTwoController(string.Empty, idCliente);
        }

        if (_PlayersConnected == 2)
        {
            CURRENT_STATE = GameState.CHOOSING_TEAM;
        }
    }

    public void JoinnedPlayers(string Name, string idcliente)
    {
        if (P1.IDCliente == idcliente)
        {
            P1.Name = Name;
        }
        else if (P2.IDCliente == idcliente)
        {
            P2.Name = Name;
            CURRENT_STATE = GameState.CHOOSING_TEAM;
        }

    }

    void PlayerOneController(string Name, string idcliente)
    {
        P1.Name = Name;
        P1.IDCliente = idcliente;
        P1.Team = string.Empty;
        P1.TurnSend = false;

        P1.MeeleCurrentAP = 5;
        P1.MeeleCurrentHP = 20;
        P1.MeeleCurrentTile = string.Empty;
        P1.ArcherCurrentAP = 15;
        P1.ArcherCurrentHP = 18;
        P1.ArcherCurrentTile = string.Empty;
        P1.MageCurrentAP = 10;
        P1.MageCurrentHP = 16;
        P1.MageCurrentTile = string.Empty;

        P1.MeeleId = P1.Team == "A" ? 1 : 4;
        P1.MeeleAP = 5;
        P1.MeeleAtk = 5;
        P1.MeeleDef = 10;
        P1.MeeleHP = 20;

        P1.ArcherId = P1.Team == "A" ? 2 : 5;
        P1.ArcherAP = 15;
        P1.ArcherAtk = 8;
        P1.ArcherDef = 4;
        P1.ArcherHP = 18;

        P1.MageId = P1.Team == "A" ? 3 : 6;
        P1.MageAP = 10;
        P1.MageAtk = 10;
        P1.MageDef = 2;
        P1.MageHP = 16;

    }

    void PlayerTwoController(string Name, string idcliente)
    {
        P2.Name = Name;
        P2.IDCliente = idcliente;
        P1.Team = string.Empty;
        P2.TurnSend = false;

        P2.MeeleCurrentAP = 5;
        P2.MeeleCurrentHP = 20;
        P2.MeeleCurrentTile = string.Empty;
        P2.ArcherCurrentAP = 15;
        P2.ArcherCurrentHP = 18;
        P2.ArcherCurrentTile = string.Empty;
        P2.MageCurrentAP = 10;
        P2.MageCurrentHP = 16;
        P2.MageCurrentTile = string.Empty;

        P2.MeeleId = P2.Team == "A" ? 1 : 4;
        P2.MeeleAP = 5;
        P2.MeeleAtk = 5;
        P2.MeeleDef = 10;
        P2.MeeleHP = 20;

        P2.ArcherId = P2.Team == "A" ? 2 : 5;
        P2.ArcherAP = 15;
        P2.ArcherAtk = 8;
        P2.ArcherDef = 4;
        P2.ArcherHP = 18;

        P2.MageId = P2.Team == "A" ? 3 : 6;
        P2.MageAP = 10;
        P2.MageAtk = 10;
        P2.MageDef = 2;
        P2.MageHP = 16;
    }


}