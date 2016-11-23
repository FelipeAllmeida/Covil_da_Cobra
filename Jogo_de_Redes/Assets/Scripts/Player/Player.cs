using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class Player : MonoBehaviour
{

    public int ActionPoints = 10;
    public int Atk = 10;
    public int Def = 10;
    public int HP = 10;
    public int id = 0;

    public string CurrentTile;

    private GameObject objMap;
    MapScript scriptMap;

    string BtnF = null;
    string TileColisor = null;
    // List<Transform> TilesEmJogo = new List<Transform>();

    Material grass_red;

    string Acao = "Movendo";

    bool Acting = false;


    //  public CharactersModel _character = new CharactersModel();
    [SerializeField]
    private float _duration = 1000f;//.5f;
    private float _timer = 0f;
    private bool _moved = false;
    public void MoveToTile(int id)
    {
        //BLUE

        if (this.id == 1 && GlobalVariables.Player_Team_Chosen == "A")
        {
            foreach (var item in GlobalVariables.WalkedMeele)
            {
                while (!_moved)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _duration)
                    {
                        _moved = true;
                        this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                    }
                }
                _timer = 0f;
                _duration = _duration + Time.deltaTime;
                _moved = false;
            }
        }
        else if (this.id == 1 && GlobalVariables.Player_Team_Chosen == "V")
        {
            foreach (var item in GlobalVariables.AllTilesInGame)
            {
                if (this.CurrentTile == item.name)
                {
                    this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                }
            }
        }

        else if (this.id == 2 && GlobalVariables.Player_Team_Chosen == "A")
        {
            foreach (var item in GlobalVariables.WalkedMeele)
            {
                while (!_moved)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _duration)
                    {
                        _moved = true;
                        this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                    }
                }
                _timer = 0f;
                _duration = _duration + Time.deltaTime;
                _moved = false;
            }
        }
        else if (this.id == 2 && GlobalVariables.Player_Team_Chosen == "V")
        {
            foreach (var item in GlobalVariables.AllTilesInGame)
            {
                if (this.CurrentTile == item.name)
                {
                    this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                }
            }
        }

        else if (this.id == 3 && GlobalVariables.Player_Team_Chosen == "A")
        {
            foreach (var item in GlobalVariables.WalkedMeele)
            {
                while (!_moved)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _duration)
                    {
                        _moved = true;
                        this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                    }
                }
                _timer = 0f;
                _duration = _duration + Time.deltaTime;
                _moved = false;
            }
        }
        else if (this.id == 3 && GlobalVariables.Player_Team_Chosen == "V")
        {
            foreach (var item in GlobalVariables.AllTilesInGame)
            {
                if (this.CurrentTile == item.name)
                {
                    this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                }
            }
        }


        //RED
        if (this.id == 4 && GlobalVariables.Player_Team_Chosen == "V")
        {
            foreach (var item in GlobalVariables.WalkedMeele)
            {
                while (!_moved)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _duration)
                    {
                        _moved = true;
                        this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                    }
                }
                _timer = 0f;
                _duration = _duration + Time.deltaTime;
                _moved = false;
            }
        }
        else if (this.id == 4 && GlobalVariables.Player_Team_Chosen == "A")
        {
            foreach (var item in GlobalVariables.AllTilesInGame)
            {
                if (this.CurrentTile == item.name)
                {
                    this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                }
            }
        }

        else if (this.id == 5 && GlobalVariables.Player_Team_Chosen == "V")
        {
            foreach (var item in GlobalVariables.WalkedMeele)
            {
                while (!_moved)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _duration)
                    {
                        _moved = true;
                        this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                    }
                }
                _timer = 0f;
                _duration = _duration + Time.deltaTime;
                _moved = false;
            }
        }
        else if (this.id == 5 && GlobalVariables.Player_Team_Chosen == "A")
        {
            foreach (var item in GlobalVariables.AllTilesInGame)
            {
                if (this.CurrentTile == item.name)
                {
                    this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                }
            }
        }

        else if (this.id == 6 && GlobalVariables.Player_Team_Chosen == "V")
        {
            foreach (var item in GlobalVariables.WalkedMeele)
            {
                while (!_moved)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _duration)
                    {
                        _moved = true;
                        this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                    }
                }
                _timer = 0f;
                _duration = _duration + Time.deltaTime;
                _moved = false;
            }
        }
        else if (this.id == 6 && GlobalVariables.Player_Team_Chosen == "A")
        {
            foreach (var item in GlobalVariables.AllTilesInGame)
            {
                if (this.CurrentTile == item.name)
                {
                    this.transform.position = new Vector3(item.transform.position.x, this.transform.position.y, item.transform.position.z);
                }
            }
        }


    }


    void Start()
    {

        grass_red = (Material)Resources.Load("grass_red", typeof(Material));

        if (this.name == "Barbaro" || this.name == "Guerreiro")
        {
            CurrentTile = this.name == "Barbaro" ? "S1122" : "S1808";
            id = this.name == "Barbaro" ? 4 : 1;

            ActionPoints = 5;
            Atk = 5;
            Def = 10;
            HP = 20;
            BtnF = "F1";
        }
        else if (this.name == "Ranger" || this.name == "Arqueiro")
        {
            CurrentTile = this.name == "Ranger" ? "S0922" : "S2008";
            id = this.name == "Ranger" ? 5 : 2;

            ActionPoints = 15;
            Atk = 8;
            Def = 4;
            HP = 18;
            BtnF = "F2";
        }
        else
        {
            CurrentTile = this.name == "Shaman" ? "S0722" : "S2208";
            id = this.name == "Shaman" ? 6 : 3;

            ActionPoints = 10;
            Atk = 10;
            Def = 2;
            HP = 16;
            BtnF = "F3";
        }


        objMap = GameObject.FindWithTag("Map");
        scriptMap = objMap.GetComponent<MapScript>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (this.ActionPoints >= 3)
            {
                Acao = "Atacou";
                GlobalVariables.PlayerAtk = true;
                PlayerAtk();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (this.ActionPoints >= 3)
            {
                Acao = "Defendeu";
                GlobalVariables.PlayerDef = true;
                PlayerDef();
            }
        }

        if (this.ActionPoints == 0 && GlobalVariables.ClassAction[this.name] == true)
        {
            GlobalVariables.ClassAction[this.name] = false;
        }

    }

    void OnCollisionEnter(Collision col)
    {
        TileColisor = col.gameObject.name;
        GlobalVariables.GlobalTileColisor[this.name] = col.gameObject.name;
        GlobalVariables.LastTileSelected[this.name] = col.gameObject.transform;
        GlobalVariables.ClassAction[this.name] = true;

    }


    void OnGUI()
    {

        if (GlobalVariables._selectedCharacter == this.name)
        {
            Vector3 coords = GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].WorldToScreenPoint(this.transform.position);
            Rect r = new Rect(coords.x, Screen.height - coords.y - 100, 100, 50);
            if (r.x < 0)
                r.x = 0;
            else if (r.xMax >= Screen.width)
                r.x = Screen.width - r.width;
            if (r.y < 0)
                r.y = 0;
            else if (r.yMax >= Screen.height)
                r.y = Screen.height - r.height;

            GUIStyle myStyle = new GUIStyle();
            myStyle.fontStyle = FontStyle.Bold;
            myStyle.fontSize = 26;
            myStyle.normal.textColor = Color.yellow;

            GUI.Box(new Rect(r.x, r.y + 40, 20, 10),
                "AP : " + ActionPoints.ToString() + "\n", myStyle);


            myStyle.fontSize = 32;
            string at = ActionPoints >= 3 ? "Sim" : "Nao";


            GUI.Box(new Rect(0, 0, 90, 70),
                GlobalVariables._selectedCharacter + "    " + BtnF + "\n" +
                "HP : " + HP.ToString() + "\n" +
                "ATK : " + Atk.ToString() + "\n" +
                "DEF : " + Def.ToString() + "\n" +
                "Agir : " + at + "\n" +
                "Ação : " + Acao, myStyle);



        }
    }

    void PlayerDef()
    {

        ///////DEEEEEEEEEEEEEEEEEEEEFFF

        if (this.name == GlobalVariables._selectedCharacter)
        {

            var valorX = GlobalVariables.GlobalTileColisor[this.name].Substring(1, 2);// int.Parse(GlobalVariables.GlobalTileColisor[this.name].Substring(1, 2), NumberStyles.Any);
            var valorY = GlobalVariables.GlobalTileColisor[this.name].Substring(3, 2);// int.Parse(GlobalVariables.GlobalTileColisor[this.name].Substring(3, 2), NumberStyles.Any);

            var tile = "S" + valorX + valorY;

            int cont = 0;

            List<Transform> tilesParaPintar = new List<Transform>();

            foreach (var item in GlobalVariables.AllTilesInGame)
            {
                if (item.ToString().Substring(0, 5) == tile)
                {
                    tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont]);
                    tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont + 1]);
                    tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont - 1]);
                    tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont + 30]);
                    tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont - 30]);
                }
                cont++;
            }

            foreach (var t in tilesParaPintar)
            {
                t.GetComponent<Renderer>().material.color = Color.black;
            }

            foreach (var til in GlobalVariables.AllWalkedTiles)
            {
                til.GetComponent<Renderer>().material.color = new Color32(102, 0, 102, 0);
            }

            foreach (var til in GlobalVariables.AllActionTiles)
            {
                til.GetComponent<Renderer>().material.color = new Color32(255, 0, 255, 0);
            }
        }

    }

    void PlayerAtk()
    {

        ///////ATTTTTTTTTTKKKKKKKKKKKKKKKKKKKK

        if (this.name == GlobalVariables._selectedCharacter)
        {

            var valorX = GlobalVariables.GlobalTileColisor[this.name].Substring(1, 2);// int.Parse(GlobalVariables.GlobalTileColisor[this.name].Substring(1, 2), NumberStyles.Any);
            var valorY = GlobalVariables.GlobalTileColisor[this.name].Substring(3, 2);// int.Parse(GlobalVariables.GlobalTileColisor[this.name].Substring(3, 2), NumberStyles.Any);

            var tile = "S" + valorX + valorY;

            int cont = 0;

            List<Transform> tilesParaPintar = new List<Transform>();

            foreach (var item in GlobalVariables.AllTilesInGame)
            {
                if (item.ToString().Substring(0, 5) == tile)
                {
                    if (GlobalVariables._selectedCharacter == "Guerreiro" || GlobalVariables._selectedCharacter == "Barbaro")
                    {
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont]);
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont + 1]);
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont - 1]);
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont + 30]);
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont - 30]);
                    }
                    else if (GlobalVariables._selectedCharacter == "Arqueiro" || GlobalVariables._selectedCharacter == "Ranger")
                    {
                        Debug.Log("1");
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont]);


                        for (int i = 1; i <= 5; i++)
                        {
                            if ((cont + i) <= 900)
                            {
                                tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont + i]);
                            }
                        }
                        Debug.Log("2");

                        for (int i = 1; i <= 5; i++)
                        {
                            if ((cont - i) >= 0)
                            {
                                tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont - i]);
                            }
                        }

                        int mult = 30;
                        Debug.Log("3");
                        for (int i = 1; i <= 5; i++)
                        {
                            if (((cont + mult + 30) <= 900) && (cont + i) <= 900)
                            {
                                tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont + mult]);
                            }
                            mult += 30;
                        }
                        Debug.Log("4");
                        mult = 30;
                        for (int i = 1; i <= 5; i++)
                        {
                            if (((cont + mult + 30) >= 0) && (cont - i) >= 0)
                            {
                                tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont - mult]);
                            }
                            mult += 30;
                        }
                        Debug.Log("5");
                    }


                    /////MAGE
                    else
                    {
                        Debug.Log("1");
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont]);

                        int contMage = 30;

                        for (int i = 1; i <= 5; i++)
                        {
                            if ((cont + i + contMage + 30 <= 900) && (cont + i) <= 900)
                            {
                                tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont + i + contMage]);
                            }
                            contMage += 30;
                        }
                        Debug.Log("2");

                        contMage = 30;
                        for (int i = 1; i <= 5; i++)
                        {
                            if ((cont - i - contMage - 30 >= 0) && (cont - i) >= 0)
                            {
                                tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont - i - contMage]);
                            }
                            contMage += 30;
                        }

                        contMage = 30;
                        Debug.Log("3");
                        for (int i = 1; i <= 5; i++)
                        {
                            if (((cont - i + contMage + 30) <= 900) && (cont + i) <= 900)
                            {
                                tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont - i + contMage]);
                            }
                            contMage += 30;
                        }
                        Debug.Log("4");

                        contMage = 30;
                        for (int i = 1; i <= 5; i++)
                        {
                            if (((cont + i - contMage - 30) >= 0) && (cont - i) >= 0)
                            {
                                tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont + i - contMage]);
                            }
                            contMage += 30;
                        }
                        Debug.Log("5");
                    }
                }
                cont++;
            }

            foreach (var t in tilesParaPintar)
            {
                t.GetComponent<Renderer>().material.color = Color.red;
            }

            foreach (var til in GlobalVariables.AllWalkedTiles)
            {
                til.GetComponent<Renderer>().material.color = new Color32(102, 0, 102, 0);
            }

            foreach (var til in GlobalVariables.AllActionTiles)
            {
                til.GetComponent<Renderer>().material.color = new Color32(255, 0, 255, 0);
            }
        }

    }

    void OnMouseDown()
    {
        if (GlobalVariables._selectedCharacter != this.name && !GlobalVariables.PlayerAtk && !GlobalVariables.PlayerDef)
        {
            foreach (var t in GlobalVariables.AllTilesInGame)
            {
                t.GetComponent<Renderer>().material.color = Color.green;
            }

            foreach (var til in GlobalVariables.AllWalkedTiles)
            {
                til.GetComponent<Renderer>().material.color = new Color32(102, 0, 102, 0);
            }

            foreach (var til in GlobalVariables.AllActionTiles)
            {
                til.GetComponent<Renderer>().material.color = Color.red;
            }

            GlobalVariables._selectedCharacter = this.name;
        }

        if (GlobalVariables.ClassAction[this.name])
        {

            ////////////////WAAAAAAAAAAAAAAAAAAALK
            if (!GlobalVariables.PlayerAtk && !GlobalVariables.PlayerDef)
            {

                var valorX = GlobalVariables.GlobalTileColisor[this.name].Substring(1, 2);// int.Parse(GlobalVariables.GlobalTileColisor[this.name].Substring(1, 2), NumberStyles.Any);
                var valorY = GlobalVariables.GlobalTileColisor[this.name].Substring(3, 2);// int.Parse(GlobalVariables.GlobalTileColisor[this.name].Substring(3, 2), NumberStyles.Any);

                var tile = "S" + valorX + valorY;

                int cont = 0;

                List<Transform> tilesParaPintar = new List<Transform>();

                foreach (var item in GlobalVariables.AllTilesInGame)
                {
                    if (item.ToString().Substring(0, 5) == tile)
                    {
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont]);
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont + 1]);
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont - 1]);
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont + 30]);
                        tilesParaPintar.Add(GlobalVariables.AllTilesInGame[cont - 30]);
                    }
                    cont++;
                }

                foreach (var t in tilesParaPintar)
                {
                    t.GetComponent<Renderer>().material.color = Color.blue;
                }



            }


        }
    }

}
