using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapScript : MonoBehaviour
{

    public Transform tilePrefab;


    public Transform[] Arvores;
    public Transform obstaclePrefab;


    public Transform Grupo1;

    public Transform VilasOrcPrefab;
    public Transform VilasHumanosPrefab;

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

                //  newTile.name = "Campo" + x + y;
                newTile.name = "S" + x + y;
                //  newTile. = "Tile";
            }
        }

        //-14.5, -14.5;
        //14.5, -14.5;
        //-14.5, 14.5;
        //14.5, 14.5;

        Coord Cord1 = new Coord(-9, 12);
        Coord Cord2 = new Coord(15, 12);
        Coord Cord3 = new Coord(-9, 10);
        Coord Cord4 = new Coord(-9, -11);

        var randCoord = new List<Coord>();
        randCoord.Add(Cord1);
        randCoord.Add(Cord2);
        randCoord.Add(Cord3);
        randCoord.Add(Cord4);

        ///////////////////////////gerar vila 1
        System.Random rnd = new System.Random();
        int r = rnd.Next(randCoord.Count);

        Coord randomCoordVila = new Coord(randCoord[r].x, randCoord[r].y);
        Vector3 VilaPosition = new Vector3(randomCoordVila.x, 0, randomCoordVila.y);// CoordToPosition(randomCoordVila.x, randomCoordVila.y);
        Transform newVila = Instantiate(VilasOrcPrefab, VilaPosition, Quaternion.identity) as Transform;
        newVila.parent = mapHolder;

        //grupo 1 posicao
        var playerPosition = new Vector3(VilaPosition.x+10, 0, VilaPosition.y+10);
        Transform newGroup = Instantiate(Grupo1, playerPosition + Vector3.up * 2f, Quaternion.identity) as Transform;
        newGroup.parent = mapHolder;
        


        randCoord.Remove(randCoord[r]);

        /////////////////////////////gerar vila 2
        System.Random rnd2 = new System.Random();
        int r2 = rnd2.Next(randCoord.Count);

        Coord randomCoordVila2 = new Coord(randCoord[r2].x, randCoord[r2].y);
        Vector3 VilaPosition2 = new Vector3(randomCoordVila2.x, 0, randomCoordVila2.y);// CoordToPosition(randomCoordVila2.x, randomCoordVila2.y);
        Transform newVila2 = Instantiate(VilasHumanosPrefab, VilaPosition2, Quaternion.identity) as Transform;
        newVila2.parent = mapHolder;






        //ARVORES 
        int obstacleCount = 20;
        for (int i = 0; i < obstacleCount; i++)
        {
            System.Random rndarvore = new System.Random();
            int rarvore = rndarvore.Next(3);
           

            Coord randomCoord = GetRandomCoord();
            Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            Transform newObstacle = Instantiate(Arvores[rarvore], obstaclePosition + Vector3.up * 2f, Quaternion.identity) as Transform;
            newObstacle.parent = mapHolder;
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
