using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.Text;
using SimpleJSON;

public class Lobby : MonoBehaviour
{

    public Text msgAguardando;
    public Button Criar;
    public Button Vermelho;
    public Button Azul;

    SocketConnector __socket;//= new SocketConnector();

    public Canvas CanvasLogin;
    public Canvas CanvasLobby;

    public Button ok;

    public Text lblJogador;
    public Text lblPlayer;
    public InputField lblName;
    // private int _nJogador = 1;

    void Start()
    {
    }

    public void Ready()
    {
        GlobalVariables.__socket.StartReadSocketDataThread();
        GlobalVariables.__socket.SendData(PlayerOk());
    }

    void Update()
    {
        if (GlobalVariables.IS_SERVER_READY)
        {
            if (GlobalVariables.WAITING)
            {
                this.enabled = false;
                CanvasLobby.enabled = true;

                GlobalVariables.IS_SERVER_READY = false;
            }

            if (GlobalVariables.START)
            {
                SceneManager.LoadScene("DefaultScene");
            }

        }
    }

    // int c = 9
    public void Conectar()
    {
        if (!string.IsNullOrEmpty(lblName.text))
        {

            //iniciar conexao
            GlobalVariables.__socket = new SocketConnector();

            Action __callbackOk = delegate
            {
                msgAguardando.enabled = true;

                GlobalVariables.__socket.StartReadSocketDataThread();
                GlobalVariables.__socket.SendData(MyNickname());


            };
            Action __callbackFail = delegate
            {
                msgAguardando.enabled = true;
                msgAguardando.text = "Problemas na conexao";
            };


            GlobalVariables.__socket.AInitialize();
            GlobalVariables.__socket.TryToConnectToSocket("127.0.0.1", 1300, __callbackOk, __callbackFail);

        }
    }

    private string MyNickname()
    {
        JSONClass Node = new JSONClass();
        //Node.Add("SendPlayer", new JSONData(1));
        GlobalVariables.PlayerName = lblName.text;
        Node.Add("SendName", new JSONData("D" + lblName.text));
        lblJogador.text = lblName.text;
        return Node["SendName"];
    }


    private string PlayerOk()
    {
        JSONClass Node = new JSONClass();
        Node.Add("SendName", new JSONData("D" + GlobalVariables.P1_Escolha));
        return Node["SendName"];
    }

    private string RequestStart()
    {
        JSONClass Node = new JSONClass();
        Node.Add("Request", new JSONData("CAN_START"));
        return Node["Request"];
    }

    public void TimeAzul()
    {
        GlobalVariables.P1_Escolha = 'A';
        Azul.interactable = false;
        Vermelho.interactable = true;
        //  SceneManager.LoadScene("DefaultScene");
    }

    public void TimeVermelho()
    {
        GlobalVariables.P1_Escolha = 'V';
        Vermelho.interactable = false;

        Azul.interactable = true;
        //  SceneManager.LoadScene("DefaultScene");
    }

}
