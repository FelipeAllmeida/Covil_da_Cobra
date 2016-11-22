using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;


    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }


    void OnGUI()
    {
        Vector3 coordsBtn = GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].WorldToScreenPoint(this.transform.position);


        //  GlobalVariables.Player_Name

        GUIStyle myStyle = new GUIStyle();
        myStyle.fontStyle = FontStyle.Bold;
        myStyle.fontSize = 26;
        myStyle.normal.textColor = Color.red;
        GUI.Button(new Rect(coordsBtn.x * 2 - 150, 0, 130, 30), GlobalVariables.Player_Name, myStyle);

        GUI.Button(new Rect(coordsBtn.x * 2 - 150, 0, 130, 30), "TERMINAR TURNO");

    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}