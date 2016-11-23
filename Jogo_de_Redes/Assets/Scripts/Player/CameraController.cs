using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    Player Script;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;

        Script = player.GetComponent<Player>();
    }


    void OnGUI()
    {
        Vector3 coordsBtn = GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].WorldToScreenPoint(this.transform.position);

        GUIStyle myStyle = new GUIStyle();
        myStyle.fontStyle = FontStyle.Bold;
        myStyle.fontSize = 26;
        myStyle.normal.textColor = Color.red;
        GUI.Button(new Rect(coordsBtn.x, 0, 300, 30), GlobalVariables.Player_Name, myStyle);

        if (GlobalVariables.WAITING_PLAYERS_TURN)
        {
            myStyle.normal.textColor = Color.blue;
            GUI.Button(new Rect(coordsBtn.x - 150, 50, 300, 30), "AGUARDANDO INIMIGO", myStyle);
        }

    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}