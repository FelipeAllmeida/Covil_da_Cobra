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
        REQUEST_PLAYER_NUMBER,
        CAN_START
    }

    //public enum GameState
    //{
    //   // [Description("REQUEST_PLAYER_NUMBER")]
    //    REQUEST_PLAYER_NUMBER
    //}


    //  private GameState _currentGameState;//= GameState.REQUEST_PLAYER_NUMBER;

    //string P1;
    //string P2;

    struct PlayerOne
    {
        public string Name;
        public string IDCliente;
        public char Team;


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
        public char Team;

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

    int _PlayersConnected = 0;


    public void PlayerOK(char Team, string idcliente)
    {
        if (P1.IDCliente == idcliente)
        {
            P1.Team = Team;
        }
        else if (P2.IDCliente == idcliente)
        {
            P2.Team = Team;
        }
    }

    public void Players(string Name, string idcliente)
    {
        _PlayersConnected++;
        if (_PlayersConnected == 1)
        {
            PlayerOneController(Name, idcliente);
        }
        else
        {
            PlayerTwoController(Name, idcliente);
        }

    }

    void PlayerOneController(string Name, string idcliente)
    {
        P1.Name = Name;
        P1.IDCliente = idcliente;
        P1.Team = 'X';

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
         P1.Team = 'X';

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


    int _nPlayers = 1;

    public int get_nPlayer()
    {
        int newPlayer = _nPlayers;
        _nPlayers++;
        return newPlayer;
    }

    public bool CanStart()
    {
        return P1.Team != 'X' ;//&& P2.Team != 'X';
    }


}