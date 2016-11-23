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
            var TempDefP1 = new Dictionary<int, List<string>>();
            var TempAtkP1 = new Dictionary<int, List<string>>();

            var TempWalkP2 = new Dictionary<int, List<string>>();
            var TempDefP2 = new Dictionary<int, List<string>>();
            var TempAtkP2 = new Dictionary<int, List<string>>();

            foreach (var _character in _ResponseP1)
            {
                foreach (var walked in _character.WalkedTiles)
                {
                    TempWalkP1[_character.id].Add(walked);
                }

                foreach (var def in _character.ActionTilesDef)
                {
                    TempDefP1[_character.id].Add(def);
                }

                foreach (var atk in _character.ActionTilesAtk)
                {
                    TempAtkP1[_character.id].Add(atk);
                }

            }

            foreach (var _character in _ResponseP2)
            {
                foreach (var walked in _character.WalkedTiles)
                {
                    TempWalkP2[_character.id].Add(walked);
                }

                foreach (var def in _character.ActionTilesDef)
                {
                    TempDefP2[_character.id].Add(def);
                }

                foreach (var atk in _character.ActionTilesAtk)
                {
                    TempAtkP2[_character.id].Add(atk);
                }

            }

            //15 action max
            for (int turno = 0; turno < 15; turno++)
            {
                var CHAR1W = TempWalkP1[1][turno];
                var CHAR1A = TempAtkP1[1][turno];
                var CHAR1D = TempDefP1[1][turno];

                var CHAR2W = TempWalkP1[2][turno];
                var CHAR2A = TempAtkP1[2][turno];
                var CHAR2D = TempDefP1[2][turno];

                var CHAR3W = TempWalkP1[3][turno];
                var CHAR3A = TempAtkP1[3][turno];
                var CHAR3D = TempDefP1[3][turno];

                var CHAR4W = TempWalkP1[4][turno];
                var CHAR4A = TempAtkP1[4][turno];
                var CHAR4D = TempDefP1[4][turno];

                var CHAR5W = TempWalkP1[5][turno];
                var CHAR5A = TempAtkP1[5][turno];
                var CHAR5D = TempDefP1[5][turno];

                var CHAR6W = TempWalkP1[6][turno];
                var CHAR6A = TempAtkP1[6][turno];
                var CHAR6D = TempDefP1[6][turno];


                //Team 1
                if (CHAR1W == CHAR4A || CHAR1W == CHAR5A || CHAR1W == CHAR6A)
                {
                    //dano
                }
                if (CHAR2W == CHAR4A || CHAR2W == CHAR5A || CHAR2W == CHAR6A)
                {
                    //dano
                }
                if (CHAR3W == CHAR4A || CHAR3W == CHAR5A || CHAR3W == CHAR6A)
                {
                    //dano
                }


                //Team 2
                if (CHAR4W == CHAR1A || CHAR4W == CHAR2A || CHAR4W == CHAR3A)
                {
                    //dano
                }
                if (CHAR5W == CHAR1A || CHAR5W == CHAR2A || CHAR5W == CHAR3A)
                {
                    //dano
                }
                if (CHAR6W == CHAR1A || CHAR6W == CHAR2A || CHAR6W == CHAR3A)
                {
                    //dano
                }






            }



            //    foreach (var item in _character.ActionTilesDef)
            //    {
            //        TempDef.Add(item);
            //    }
            //    foreach (var item in _character.ActionTilesAtk)
            //    {
            //        TempAtk.Add(item);
            //    }
            //}
















            CURRENT_STATE = GameState.ON;
            if ((Action)p_callbackResponseTurn != null) ((Action)p_callbackResponseTurn)();
        }

        //while (CURRENT_STATE == GameState.ON || CURRENT_STATE == GameState.REQUEST_TURN)
        //{
        //    //Responde os calculos para todos
        //    if (CURRENT_STATE == GameState.REQUEST_TURN)
        //    {
        //        if (P1.TurnSend)//&& )
        //        {
        //            P1.TurnSend = false;
        //            P2.TurnSend = false;

        //            CURRENT_STATE = GameState.ON;
        //            if ((Action)p_callbackResponseTurn != null) ((Action)p_callbackResponseTurn)();
        //        }
        //    }
        //}
    }

    List<CharactersModel> _ResponseP1;
    List<CharactersModel> _ResponseP2;

    public string GameLogic(string IDCLiente = null, string _Turn = null)
    {
        //public int id;
        //public string CurrentTile;
        //public List<string> WalkedTiles = new List<string>();
        //public List<string> ActionTilesAtk = new List<string>();
        //public List<string> ActionTilesDef = new List<string>();

        //public int CurrentHP;
        //public int CurrentAP;


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

                    JSONClass rootNode = new JSONClass();

                    JSONNode _responseJSON = JSONNode.Parse(p_JsonResponseArray[i]);

                    int id_Character = _responseJSON["id"].AsInt;

                    string CurrentTile_Character = _responseJSON["CurrentTile"].Value;

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


                    //var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //  string jsonString = javaScriptSerializer.Serialize(_char_response);
                    //ListToSend.Add(_char_response.ToString());

                    //JSONData WalkedTiles = null;

                    ////////////////

                    /* JSONArray WalkedTiles = new JSONArray();
                     if (_char_response.WalkedTiles.Count > 0)
                     {
                         foreach (var str in _char_response.WalkedTiles)
                         {
                             WalkedTiles.Add(str);
                         }
                     }
                     rootNode.Add("WalkedTiles", new JSONData(WalkedTiles.ToString()));

                     JSONArray ActionTilesAtk = new JSONArray();
                     if (_char_response.ActionTilesAtk.Count > 0)
                     {
                         foreach (var str in _char_response.ActionTilesAtk)
                         {
                             ActionTilesAtk.Add(str);
                         }
                     }
                     rootNode.Add("ActionTilesAtk", new JSONData(ActionTilesAtk.ToString()));

                     JSONArray ActionTilesDef = new JSONArray();
                     if (_char_response.ActionTilesDef.Count > 0)
                     {
                         foreach (var str in _char_response.ActionTilesDef)
                         {
                             ActionTilesDef.Add(str);
                         }
                     }
                     rootNode.Add("ActionTilesDef", new JSONData(ActionTilesDef.ToString()));*/

                    ////////////////


                    rootNode.Add("id", new JSONData(_char_response.id));
                    rootNode.Add("CurrentHP", new JSONData(_char_response.CurrentHP));
                    rootNode.Add("CurrentAP", new JSONData(_char_response.CurrentAP));
                    rootNode.Add("CurrentTile", new JSONData(_char_response.CurrentTile != null ? _char_response.CurrentTile.ToString() : string.Empty));


                    _ResponseP1.Add(_char_response);

                    ListToSend.Add(rootNode);
                }
                P1.TurnSend = true;
                // CURRENT_STATE = GameState.REQUEST_TURN;
                return "Dados do Turno recebidos";
            }
            if (P2.IDCliente == IDCLiente && !P2.TurnSend && _PlayersConnected == 2)
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

                    string CurrentTile_Character = _responseJSON["CurrentTile"].Value;

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


                    //var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //  string jsonString = javaScriptSerializer.Serialize(_char_response);
                    //ListToSend.Add(_char_response.ToString());

                    //JSONData WalkedTiles = null;

                    ////////////////

                    /* JSONArray WalkedTiles = new JSONArray();
                     if (_char_response.WalkedTiles.Count > 0)
                     {
                         foreach (var str in _char_response.WalkedTiles)
                         {
                             WalkedTiles.Add(str);
                         }
                     }
                     rootNode.Add("WalkedTiles", new JSONData(WalkedTiles.ToString()));

                     JSONArray ActionTilesAtk = new JSONArray();
                     if (_char_response.ActionTilesAtk.Count > 0)
                     {
                         foreach (var str in _char_response.ActionTilesAtk)
                         {
                             ActionTilesAtk.Add(str);
                         }
                     }
                     rootNode.Add("ActionTilesAtk", new JSONData(ActionTilesAtk.ToString()));

                     JSONArray ActionTilesDef = new JSONArray();
                     if (_char_response.ActionTilesDef.Count > 0)
                     {
                         foreach (var str in _char_response.ActionTilesDef)
                         {
                             ActionTilesDef.Add(str);
                         }
                     }
                     rootNode.Add("ActionTilesDef", new JSONData(ActionTilesDef.ToString()));*/

                    ////////////////


                    rootNode.Add("id", new JSONData(_char_response.id));
                    rootNode.Add("CurrentHP", new JSONData(_char_response.CurrentHP));
                    rootNode.Add("CurrentAP", new JSONData(_char_response.CurrentAP));
                    rootNode.Add("CurrentTile", new JSONData(_char_response.CurrentTile != null ? _char_response.CurrentTile.ToString() : string.Empty));


                    _ResponseP2.Add(_char_response);

                    ListToSend.Add(rootNode);
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