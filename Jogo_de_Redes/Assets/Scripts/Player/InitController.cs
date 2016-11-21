using UnityEngine;
using System.Collections;

public class InitController : MonoBehaviour
{
    public Camera[] cameras;


    // Use this for initialization
    void Start()
    {
        GlobalVariables.Cameras = cameras;

        

        //desabilita todas cameras menos a padrao
        for (int i = 1; i < GlobalVariables.Cameras.Length; i++)
        {
            GlobalVariables.Cameras[i].gameObject.SetActive(false);
        }


        if (GlobalVariables.Cameras.Length > 0)
        {
            //time azul
            if (GlobalVariables.P1_Escolha == 'A')
            {
                GlobalVariables.currentCameraIndex = 0;
                GlobalVariables.Cameras[0].gameObject.SetActive(true);
                GameObject.Find("RED_SNAKES_Controller").SetActive(false);
            }
            else
            {
                GlobalVariables.currentCameraIndex = 3;
                GlobalVariables.Cameras[3].gameObject.SetActive(true);
                GameObject.Find("BLUE_SNAKES_Controller").SetActive(false);
            }
        }

    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (GlobalVariables.P1_Escolha == 'A')
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 0;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 3;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (GlobalVariables.P1_Escolha == 'A')
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 1;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 4;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (GlobalVariables.P1_Escolha == 'A')
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 2;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < GlobalVariables.Cameras.Length; i++)
                {
                    GlobalVariables.Cameras[i].gameObject.SetActive(false);
                }
                GlobalVariables.currentCameraIndex = 5;
                GlobalVariables.Cameras[GlobalVariables.currentCameraIndex].gameObject.SetActive(true);
            }
        }
    }

}
