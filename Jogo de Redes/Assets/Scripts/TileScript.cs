using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour
{
    private GameObject[] player;
    Player playerScript;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");

        GlobalVariables.ListaPersonagens = player;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        foreach (var item in player)
        {
            if (item.name == GlobalVariables.personagemSelecionado)
            {
                item.transform.position = new Vector3(this.transform.position.x, item.transform.position.y, this.transform.position.z);

            }
        }

    }
}
