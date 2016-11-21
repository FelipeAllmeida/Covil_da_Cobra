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
        GUI.Button(new Rect(coordsBtn.x * 2 - 100 , 0, 90, 30), "PRONTO");

    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}