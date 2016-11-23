using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapScript : MonoBehaviour
{

    public Transform tilePrefab;


    public Transform[] Arvores;
   //public Transform obstaclePrefab;


    //public Transform Grupo1;

    //public Transform VilasOrcPrefab;
    //public Transform VilasHumanosPrefab;

    public Vector2 mapSize;

    [Range(0, 1)]
    public float outlinePercent;

    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;

    public int seed = 10;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {

        allTileCoords = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }
        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));

        string holderName = "TileMap";
        if (transform.FindChild(holderName))
        {
            DestroyImmediate(transform.FindChild(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;

                string str_tileX = null;
                string str_tileY = null;
                if (x < 10)
                {
                    str_tileX = "0" + x;
                }
                else
                {
                    str_tileX = x.ToString();
                }
                if (y < 10)
                {
                    str_tileY = "0" + y;
                }
                else
                {
                    str_tileY = y.ToString();
                }
                newTile.name = "S" + str_tileX + str_tileY;

                newTile.GetComponent<Renderer>().material.color = Color.green;

                GlobalVariables.AllTilesInGame.Add(newTile);
            }
        }




        //ARVORES 
        System.Random rndarvore = new System.Random();
        int obstacleCount = 30;
        for (int i = 0; i < obstacleCount; i++)
        {

            int rarvore = rndarvore.Next(0, 2);
            var arvore = Arvores[rarvore];

            Coord randomCoord = GetRandomCoord();
            Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            Transform newObstacle = Instantiate(arvore, obstaclePosition + Vector3.up * 2 , Quaternion.identity) as Transform;
        }

    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
