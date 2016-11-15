using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    private int ActionPoints = 10;
    private int Atk = 10;
    private int Def = 10;
    private int HP = 10;

    private GameObject objMap;
    MapScript scriptMap;
    Camera camera;

    string TileColisor = null;
    List<Transform> TilesEmJogo = new List<Transform>();

    Material grass_red;
    void Start()
    {
        grass_red = (Material)Resources.Load("grass_red", typeof(Material));

        if (this.name == "Guerreiro")
        {
            ActionPoints = 5;
            Atk = 5;
            Def = 10;
            HP = 20;
        }
        else if (this.name == "Arqueiro")
        {
            ActionPoints = 15;
            Atk = 8;
            Def = 4;
            HP = 18;
        }
        else
        {
            ActionPoints = 10;
            Atk = 10;
            Def = 2;
            HP = 16;
        }


        objMap = GameObject.FindWithTag("Map");
        scriptMap = objMap.GetComponent<MapScript>();

        var tileMap = objMap.GetComponentsInChildren<Transform>();
        foreach (var item in tileMap)
        {
            var nomeTile = item.ToString().Substring(0, 1);
            if (nomeTile == "S")
            {
                TilesEmJogo.Add(item);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        camera = Camera.current;
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        TileColisor = col.gameObject.name;
    }


    void OnGUI()
    {
        foreach (var player in GlobalVariables.ListaPersonagens)
        {
            if (player.name == this.name)
            {
                Transform target = player.transform;
                Vector3 screenPos = camera.WorldToScreenPoint(target.position);
                GUI.Label(new Rect(screenPos.x, screenPos.y, 80, 20), "AP : " + ActionPoints.ToString());
                GUI.Label(new Rect(screenPos.x, screenPos.y + 10, 80, 20), "HP : " + HP.ToString());
                GUI.Label(new Rect(screenPos.x, screenPos.y + 20, 80, 20), "ATK : " + Atk.ToString());
                GUI.Label(new Rect(screenPos.x, screenPos.y + 30, 80, 20), "DEF : " + Def.ToString());
            }
        }

    }



    void OnMouseDown()
    {
        GlobalVariables.personagemSelecionado = this.name;

        int valorX = int.Parse(TileColisor.Substring(1, 1));
        int valorY = int.Parse(TileColisor.Substring(2, 1));

        var tile = "S" + valorX + valorY;

        int cont = 0;

        List<Transform> tilesParaPintar = new List<Transform>();

        foreach (var item in TilesEmJogo)
        {
            if (item.ToString().Substring(0, 3) == tile)
            {
                tilesParaPintar.Add(TilesEmJogo[cont]);
                tilesParaPintar.Add(TilesEmJogo[cont + 1]);
                tilesParaPintar.Add(TilesEmJogo[cont - 1]);
                tilesParaPintar.Add(TilesEmJogo[cont + 10]);
                tilesParaPintar.Add(TilesEmJogo[cont - 10]);
            }
            cont++;
        }

        foreach (var t in tilesParaPintar)
        {
            t.GetComponent<Renderer>().material.color = Color.red;
            //  t.GetComponent<Renderer>().material = grass_red;

        }

    }

}
