using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.Text;
using SimpleJSON;
using System.Collections.Generic;

public class Lobby : MonoBehaviour
{

    public Text _text_MsgWating;

    public Button _button_RedTeam;
    public Button _button_BlueTeam;

    // SocketConnector __socket;

    public Canvas _canvas_Login;
    public Canvas _canvas_Lobby;

    public Text _text_DisplayPlayerName;

    public InputField _input_PlayerName;

    void Update()
    {
        if (GlobalVariables.IS_SERVER_READY)
        {
            if (GlobalVariables.WAITING)
            {
                this.enabled = false;
                _canvas_Lobby.enabled = true;

                GlobalVariables.IS_SERVER_READY = false;
            }

            if (GlobalVariables.START)
            {
                GlobalVariables.IS_SERVER_READY = false;
                SceneManager.LoadScene("DefaultScene");
            }

        }
    }

    public struct ConnectionData
    {
        public string ID;
        public string PlayerName;
        public string EnumRequst;
        public bool PlayerReady;
        public char TeamChosen;
    }
    ConnectionData _ConnectionData = new ConnectionData();


    public void Connect()
    {
        _ConnectionData.PlayerReady = false;

        //se o nome nao esta vazio
        if (!string.IsNullOrEmpty(_input_PlayerName.text))
        {
            GlobalVariables.__socket = new SocketConnector();


            GlobalVariables.__socket.AInitialize();
            GlobalVariables.__socket.TryToConnectToSocket("127.0.0.1", 1300,
                //ok
                    delegate
                    {
                        //mensagem de espera de outro jogador
                        _text_MsgWating.enabled = true;
                        _ConnectionData.PlayerName = _input_PlayerName.text;
                        _text_DisplayPlayerName.text = _input_PlayerName.text;
                        GlobalVariables.Player_Name = _input_PlayerName.text;
                        GlobalVariables.__socket.SendData(JsonUtility.ToJson(_ConnectionData));

                    },
                //fail
                    delegate
                    {
                        _text_MsgWating.enabled = true;
                        _text_MsgWating.text = "Problemas na conexao";
                    });

            GlobalVariables.__socket.onSocketResponse += delegate(string p_response)
            {
                Debug.Log("Socket response: " + p_response);
                GlobalVariables.__socket.StopReadingSocketDataThread();
            };

            GlobalVariables.__socket.StartReadSocketDataThread();
        }
    }

    public void PlayerReady()
    {
        _ConnectionData.PlayerReady = true;
        _ConnectionData.TeamChosen = GlobalVariables.Player_Team_Chosen;
        GlobalVariables.__socket.SendData(JsonUtility.ToJson(_ConnectionData));
        GlobalVariables.__socket.StartReadSocketDataThread();
    }

    public void BlueTeam()
    {
        GlobalVariables.Player_Team_Chosen = 'A';
        _button_BlueTeam.interactable = false;
        _button_RedTeam.interactable = true;
    }

    public void ReadTeam()
    {
        GlobalVariables.Player_Team_Chosen = 'V';
        _button_RedTeam.interactable = false;
        _button_BlueTeam.interactable = true;
    }

}
