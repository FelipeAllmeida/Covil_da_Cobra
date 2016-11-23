using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InitController : MonoBehaviour
{
    public Camera[] cameras;


    // Use this for initialization
    void Start()
    {
        GlobalVariables.Cameras = cameras;
        GlobalVariables.BLUE_TEAM = GameObject.Find("BLUE_SNAKES_Controller").GetComponentsInChildren<Transform>();
        GlobalVariables.RED_TEAM = GameObject.Find("RED_SNAKES_Controller").GetComponentsInChildren<Transform>();

        //desabilita todas cameras menos a padrao
        for (int i = 1; i < GlobalVariables.Cameras.Length; i++)
        {
            GlobalVariables.Cameras[i].gameObject.SetActive(false);
        }


        if (GlobalVariables.Cameras.Length > 0)
        {
            //time azul
            if (GlobalVariables.Player_Team_Chosen == "A")
            {
                GlobalVariables.currentCameraIndex = 0;
                GlobalVariables.Cameras[0].gameObject.SetActive(true);
                GlobalVariables.Cameras[3].gameObject.SetActive(false);
                // GameObject.Find("BLUE_SNAKES_Controller").SetActive(false);
            }
            else
            {
                GlobalVariables.currentCameraIndex = 3;
                GlobalVariables.Cameras[3].gameObject.SetActive(true);
                GlobalVariables.Cameras[0].gameObject.SetActive(false);
                // GameObject.Find("RED_SNAKES_Controller").SetActive(false);
            }
        }

    }

    bool _drawTurnButton = true;
    void OnGUI()
    {
        if (_drawTurnButton)
        {
            if (GUI.Button(new Rect(Screen.width - 150, 0, 150, 50), "TERMINAR TURNO"))
            {
                _drawTurnButton = false;
                SendActions();
            }
        }
    }

    void SendActions()
    {
        SimpleJSON.JSONArray ListToSend = new SimpleJSON.JSONArray();
        Transform[] p_group;

        if (GlobalVariables.Player_Team_Chosen == "A")
        {
            p_group = GameObject.Find("BLUE_SNAKES_Controller").GetComponentsInChildren<Transform>();
        }
        else
        {
            p_group = GameObject.Find("RED_SNAKES_Controller").GetComponentsInChildren<Transform>();
        }

        foreach (Transform child in p_group)
        {
            for (int i = 0; i < child.childCount; i++)
            {
                var character = child.GetChild(i);
                if (character.tag == "Player")
                {
                    CharactersModel _Character_Actions = new CharactersModel();

                    Player PlayerScript = character.GetComponent<Player>();

                    if (character.name == "Barbaro" || character.name == "Guerreiro")
                    {
                        foreach (var walk in GlobalVariables.WalkedMeele)
                        {
                            _Character_Actions.WalkedTiles.Add(walk.name);
                        }
                        foreach (var atk in GlobalVariables.ActionMeeleAtk)
                        {
                            _Character_Actions.ActionTilesAtk.Add(atk.name);
                        }
                        foreach (var def in GlobalVariables.ActionMeeleDef)
                        {
                            _Character_Actions.ActionTilesDef.Add(def.name);
                        }
                    }
                    else if (character.name == "Ranger" || character.name == "Arqueiro")
                    {
                        foreach (var walk in GlobalVariables.WalkedRange)
                        {
                            _Character_Actions.WalkedTiles.Add(walk.name);
                        }
                        foreach (var atk in GlobalVariables.ActionRangeAtk)
                        {
                            _Character_Actions.ActionTilesAtk.Add(atk.name);
                        }
                        foreach (var def in GlobalVariables.ActionRangeDef)
                        {
                            _Character_Actions.ActionTilesDef.Add(def.name);
                        }
                    }
                    else
                    {
                        foreach (var walk in GlobalVariables.WalkedMage)
                        {
                            _Character_Actions.WalkedTiles.Add(walk.name);
                        }
                        foreach (var atk in GlobalVariables.ActionMageAtk)
                        {
                            _Character_Actions.ActionTilesAtk.Add(atk.name);
                        }
                        foreach (var def in GlobalVariables.ActionMageDef)
                        {
                            _Character_Actions.ActionTilesDef.Add(def.name);
                        }
                    }

                    _Character_Actions.CurrentAP = PlayerScript.ActionPoints;
                    _Character_Actions.CurrentHP = PlayerScript.HP;
                    _Character_Actions.CurrentTile = PlayerScript.CurrentTile;
                    _Character_Actions.id = PlayerScript.id;

                    ListToSend.Add(JsonUtility.ToJson(_Character_Actions));
                }
            }
        }
        GlobalVariables.__socket.SendData(ListToSend.ToString());
        GlobalVariables.__socket.StartReadSocketDataThread();
    }

    void Update()
    {
        if (GlobalVariables.DO_TURN && GlobalVariables.IS_SERVER_READY)
        {
            GlobalVariables.IS_SERVER_READY = false;
            var p_JsonResponse = SimpleJSON.JSON.Parse(GlobalVariables._SERVER_RESPONSE);
            for (int i = 0; i < p_JsonResponse.Count; i++)
            {
                //p_JsonResponse[i]["id"].AsInt

                int id = p_JsonResponse[i]["id"].AsInt;
                int CurrentHP = p_JsonResponse[i]["CurrentHP"].AsInt;
                int CurrentAP = p_JsonResponse[i]["CurrentAP"].AsInt;
                string CurrentTile = p_JsonResponse[i]["CurrentTile"].Value;

                Transform[] p_group;

                p_group = GameObject.Find("RED_SNAKES_Controller").GetComponentsInChildren<Transform>();
                foreach (Transform child in p_group)
                {
                    for (int j = 0; j < child.childCount; j++)
                    {
                        var character = child.GetChild(j);
                        if (character.tag == "Player")
                        {
                            Player PlayerScript = character.GetComponent<Player>();

                            if (PlayerScript.id == id)
                            {
                                PlayerScript.CurrentTile = CurrentTile;
                                PlayerScript.HP = CurrentHP;
                                PlayerScript.ActionPoints = CurrentAP;

                                PlayerScript.MoveToTile(id);
                            }

                        }
                    }
                }
                p_group = GameObject.Find("BLUE_SNAKES_Controller").GetComponentsInChildren<Transform>();
                foreach (Transform child in p_group)
                {
                    for (int j = 0; j < child.childCount; j++)
                    {
                        var character = child.GetChild(j);
                        if (character.tag == "Player")
                        {
                            Player PlayerScript = character.GetComponent<Player>();

                            if (PlayerScript.id == id)
                            {
                                PlayerScript.CurrentTile = CurrentTile;
                                PlayerScript.HP = CurrentHP;
                                PlayerScript.ActionPoints = CurrentAP;

                                PlayerScript.MoveToTile(id);
                            }

                        }
                    }
                }
            }
            GlobalVariables.NEXT_TURN = true;
        }

        if (GlobalVariables.NEXT_TURN)
        {
            foreach (var t in GlobalVariables.AllTilesInGame)
            {
                t.GetComponent<Renderer>().material.color = Color.green;
            }
            GlobalVariables.NEXT_TURN = false;
        }




        if (GlobalVariables.WAITING_PLAYERS_TURN && GlobalVariables.IS_SERVER_READY)
        {
            GlobalVariables.IS_SERVER_READY = false;
            GlobalVariables.__socket.StartReadSocketDataThread();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (GlobalVariables.Player_Team_Chosen == "A")
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 0;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 3;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (GlobalVariables.Player_Team_Chosen == "A")
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 1;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 4;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (GlobalVariables.Player_Team_Chosen == "A")
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 2;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 5;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
        }
    }

}
