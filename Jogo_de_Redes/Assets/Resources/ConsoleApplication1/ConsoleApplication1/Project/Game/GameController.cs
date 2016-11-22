using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SimpleJSON;
using System.ComponentModel;


internal class GameController
{

    public enum GameState
    {
        CONNECTING_PLAYERS,
        CHOOSING_TEAM,
        START_GAME
    }

    public GameState CURRENT_STATE = GameState.CONNECTING_PLAYERS;


    struct PlayerOne
    {
        public string Name;
        public string IDCliente;
        public string Team;


        public int MeeleId;
        public int MeeleAP;
        public int MeeleAtk;
        public int MeeleDef;
        public int MeeleHP;

        public int ArcherId;
        public int ArcherAP;
        public int ArcherAtk;
        public int ArcherDef;
        public int ArcherHP;

        public int MageId;
        public int MageAP;
        public int MageAtk;
        public int MageDef;
        public int MageHP;
    }

    struct PlayerTwo
    {
        public string Name;
        public string IDCliente;
        public string Team;

        public int MeeleId;
        public int MeeleAP;
        public int MeeleAtk;
        public int MeeleDef;
        public int MeeleHP;

        public int ArcherId;
        public int ArcherAP;
        public int ArcherAtk;
        public int ArcherDef;
        public int ArcherHP;

        public int MageId;
        public int MageAP;
        public int MageAtk;
        public int MageDef;
        public int MageHP;
    }


    PlayerOne P1;
    PlayerTwo P2;

    public int _PlayersConnected = 0;
    public int _PlayersReady = 0;


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

        P1.MeeleId = 1;
        P1.MeeleAP = 5;
        P1.MeeleAtk = 5;
        P1.MeeleDef = 10;
        P1.MeeleHP = 20;

        P1.ArcherId = 2;
        P1.ArcherAP = 15;
        P1.ArcherAtk = 8;
        P1.ArcherDef = 4;
        P1.ArcherHP = 18;

        P1.MageId = 2;
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

        P2.MeeleId = 1;
        P2.MeeleAP = 5;
        P2.MeeleAtk = 5;
        P2.MeeleDef = 10;
        P2.MeeleHP = 20;

        P2.ArcherId = 2;
        P2.ArcherAP = 15;
        P2.ArcherAtk = 8;
        P2.ArcherDef = 4;
        P2.ArcherHP = 18;

        P2.MageId = 2;
        P2.MageAP = 10;
        P2.MageAtk = 10;
        P2.MageDef = 2;
        P2.MageHP = 16;
    }


}