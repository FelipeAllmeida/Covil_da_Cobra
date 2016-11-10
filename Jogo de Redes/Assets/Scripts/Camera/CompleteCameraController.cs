using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{

    GameObject PlayerGroup;      


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        PlayerGroup = GameObject.Find("Grupo1_Controller");

        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - PlayerGroup.transform.position;
        transform.position = PlayerGroup.transform.position + offset;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = PlayerGroup.transform.position + offset;
    }
}