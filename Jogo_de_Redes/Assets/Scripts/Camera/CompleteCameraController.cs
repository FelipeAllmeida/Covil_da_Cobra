using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{

    GameObject PlayerGroup;


    private Vector3 offset;

    void Start()
    {
        PlayerGroup = GameObject.Find("Grupo1_Controller");

        offset = transform.position - PlayerGroup.transform.position;
        transform.position = PlayerGroup.transform.position + offset;
    }

    void LateUpdate()
    {
        transform.position = PlayerGroup.transform.position + offset;
    }
}