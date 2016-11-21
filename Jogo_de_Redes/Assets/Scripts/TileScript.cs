using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileScript : MonoBehaviour
{
    private GameObject[] player;

    private bool ServResponse = false;

    private bool CanWalk = true;
    private bool CanAtk = true;
    private bool CanDef = true;


    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");

        GlobalVariables.ListaPersonagens = player;
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        if (GlobalVariables.personagemSelecionado != null)
        {
            //resposta do servidor
            //foreach (var item in player)
            //{
            //    if (item.name == GlobalVariables.personagemSelecionado)
            //    {
            //        item.transform.position = new Vector3(this.transform.position.x, item.transform.position.y, this.transform.position.z);

            //    }
            //}

            if (!ServResponse)
            {

                if (this.GetComponent<Renderer>().material.color != Color.green)
                {


                    foreach (var per in GlobalVariables.ListaPersonagens)
                    {
                        if (per.name == GlobalVariables.personagemSelecionado)
                        {
                            CanWalk = true;
                            CanAtk = true;
                            CanDef = true;

                            var scr = per.GetComponent<Player>();
                            if (scr.ActionPoints != 0 && !GlobalVariables.PlayerAtk && !GlobalVariables.PlayerDef)
                            {
                                scr.ActionPoints--;
                            }
                            else
                            {
                                CanWalk = false;
                            }

                            if (scr.ActionPoints >= 3 && GlobalVariables.PlayerAtk)
                            {
                                scr.ActionPoints -= 3;

                            }
                            else
                            {
                                CanAtk = false;
                            }

                            if (scr.ActionPoints >= 3 && GlobalVariables.PlayerDef)
                            {
                                scr.ActionPoints -= 3;

                            }
                            else
                            {
                                CanDef = false;
                            }

                        }
                    }

                    if (CanDef && GlobalVariables.PlayerDef)
                    {


                        GlobalVariables.GlobalTileColisor[GlobalVariables.personagemSelecionado] = this.name;

                        GlobalVariables.tilesAcoes.Add(this.transform);
                        foreach (var til in GlobalVariables.tilesAcoes)
                        {
                            til.GetComponent<Renderer>().material.color = Color.red;
                        }
                        GlobalVariables.PlayerDef = false;

                        var ultTile = GlobalVariables.UltimoTileSelecionado[GlobalVariables.personagemSelecionado];
                        var scr = ultTile.GetComponent<TileScript>();
                        scr.OnMouseDown();

                    }


                    if (CanAtk && GlobalVariables.PlayerAtk)
                    {

                        GlobalVariables.GlobalTileColisor[GlobalVariables.personagemSelecionado] = this.name;

                        GlobalVariables.tilesAcoes.Add(this.transform);
                        foreach (var til in GlobalVariables.tilesAcoes)
                        {
                            til.GetComponent<Renderer>().material.color = new Color32(255, 0, 255, 0);
                        }
                        GlobalVariables.PlayerAtk = false;


                        var ultTile = GlobalVariables.UltimoTileSelecionado[GlobalVariables.personagemSelecionado];
                        var scr = ultTile.GetComponent<TileScript>();
                        scr.OnMouseDown();
                    }


                    if (CanWalk && !GlobalVariables.PlayerAtk && !GlobalVariables.PlayerDef)
                    {

                        GlobalVariables.GlobalTileColisor[GlobalVariables.personagemSelecionado] = this.name;

                        var valorX = GlobalVariables.GlobalTileColisor[GlobalVariables.personagemSelecionado].Substring(1, 2);
                        var valorY = GlobalVariables.GlobalTileColisor[GlobalVariables.personagemSelecionado].Substring(3, 2);

                        var tile = "S" + valorX + valorY;

                        int cont = 0;

                        List<Transform> tilesParaPintar = new List<Transform>();

                        //voltar a cor original
                        foreach (var item in GlobalVariables.TilesEmJogo)
                        {
                            item.GetComponent<Renderer>().material.color = Color.green;
                        }

                        //pintar
                        foreach (var item in GlobalVariables.TilesEmJogo)
                        {
                            if (item.ToString().Substring(0, 5) == tile)
                            {
                                tilesParaPintar.Add(GlobalVariables.TilesEmJogo[cont]);
                                tilesParaPintar.Add(GlobalVariables.TilesEmJogo[cont + 1]);
                                tilesParaPintar.Add(GlobalVariables.TilesEmJogo[cont - 1]);
                                tilesParaPintar.Add(GlobalVariables.TilesEmJogo[cont + 30]);
                                tilesParaPintar.Add(GlobalVariables.TilesEmJogo[cont - 30]);
                            }
                            cont++;
                        }

                        foreach (var t in tilesParaPintar)
                        {
                            if (t.ToString().Substring(0, 5) != tile)
                            {
                                t.GetComponent<Renderer>().material.color = Color.blue;
                            }
                        }

                        GlobalVariables.tilesCaminhados.Add(this.transform);
                        foreach (var til in GlobalVariables.tilesCaminhados)
                        {
                            til.GetComponent<Renderer>().material.color = new Color32(102, 0, 102, 0);
                        }

                        foreach (var til in GlobalVariables.tilesAcoes)
                        {
                            til.GetComponent<Renderer>().material.color = new Color32(255, 0, 255, 0);
                        }

                        GlobalVariables.UltimoTileSelecionado[GlobalVariables.personagemSelecionado] = this.transform;
                    }
                }
            }
        }
    }
}
