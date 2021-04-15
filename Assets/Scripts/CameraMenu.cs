using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



public class CameraMenu : MonoBehaviour
{
    public GameObject Gimper;
    public GameObject Lysy;
    public GameObject SonGimper;
    public GameObject Tata;
    private GameObject player;
    public static CameraMenu Instance { get; set; }

	private Vector3 basePosition, basePlayerPosition;
    public Vector3 Right;
    public Vector3 Left;

    public int whichRoom { set;  get; } // -1 - Main Menu, 0 - Gym, 1 - WS3D, 2 - Changing Room
    public int whichChar { get; set; } // 0 Gimper, 1 Egg, 2 Son, 3 Tata

    private void Awake()
    {
        Gimper.SetActive(true);
        Lysy.SetActive(false);
        SonGimper.SetActive(false);
        Tata.SetActive(false);
        player = Gimper;
        whichRoom = -1;
        Instance = this;
        basePosition = new Vector3(0.0f, 42.0f, -4.0f);
        basePlayerPosition = player.transform.position;
        transform.position = basePosition;
        player.GetComponent<GameObject>();
        basePlayerPosition = player.transform.position;
        transform.position = basePosition;
        player.transform.position = new Vector3(0.0f, 0.3f, 6.3f);
    }


    private void CameraToRight()
    {
        if (whichRoom == 1)
        {
            transform.position = new Vector3(0.0f, 12.0f, -16.0f);
            whichRoom = 3;
            transform.rotation = Quaternion.Euler(40, 180, 0);
            player.transform.position = new Vector3(0, 0.3f, -26.5f);
            player.transform.Rotate(0.0f, 90.0f, 0.0f);
        }
        if (whichRoom == 0)
        {
            transform.position = new Vector3(6.0f, 12.0f, -10.0f);
            whichRoom = 1;
            transform.rotation = Quaternion.Euler(40, 90, 0);
            player.transform.position = new Vector3(16.6f, 0.3f, -10f);
            player.transform.Rotate(0.0f, 90.0f, 0.0f);

        }
        if(whichRoom ==2)
        {
            transform.position = new Vector3 (0.0f, 12.0f, -4.0f);
            whichRoom = 0;
            transform.rotation = Quaternion.Euler(40, 0, 0);
            player.transform.position = new Vector3(0.0f, 0.3f, 6.3f);
            player.transform.Rotate(0.0f, 90.0f, 0.0f);
        }
     
    }

    private void CameraToLeft()
    {

        if (whichRoom == 0)
        {
            transform.position = transform.position + Left;
            whichRoom = 2;
            transform.rotation = Quaternion.Euler(40,-90, 0);
            player.transform.position = new Vector3(-16.6f, 0.3f, -10f);
            player.transform.Rotate(0.0f, -90.0f, 0.0f);
        }
        if (whichRoom == 1)
        {
            transform.position = new Vector3(0.0f, 12.0f, -4.0f);
            whichRoom = 0;
            transform.rotation = Quaternion.Euler(40, 0, 0);
            player.transform.position = new Vector3(0.0f, 0.3f, 6.3f);
            player.transform.Rotate(0.0f, -90.0f, 0.0f);
        }
        if (whichRoom == 3)
        {
            transform.position = new Vector3(6.0f, 12.0f, -10.0f);
            whichRoom = 1;
            transform.rotation = Quaternion.Euler(40, 90, 0);
            player.transform.position = new Vector3(16.6f, 0.3f, -10f);
            player.transform.Rotate(0.0f, -90.0f, 0.0f);
        }


    }
    public void DelayToRight(float delayTime)
    {
        Invoke("CameraToRight", delayTime);
    }
    public void DelayToLeft(float delayTime)
    {
        Invoke("CameraToLeft", delayTime);
    }
    private void BackToTitleScreen()
    {
        transform.position = basePosition;
        transform.rotation = Quaternion.Euler(40, 0, 0);
        player.transform.position = basePlayerPosition;
        if (whichRoom == 1)
            player.transform.Rotate(0.0f, -90.0f, 0.0f);
        if (whichRoom == 2)
            player.transform.Rotate(0.0f, 90.0f, 0.0f);
        if(whichRoom ==3)
            player.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public void DelayToTitleScreen(float delayTime)
    {
        Invoke("BackToTitleScreen", delayTime);
    }
    public void ToScenes()
    {
        whichRoom = 0;
        transform.position = new Vector3(0.0f, 12.0f, -4.0f);
    }
    public void DelayToScenes(float delayTime)
    {
        Invoke("ToScenes", delayTime);
    }

    public void CharacterRight()
    {
        if (whichChar == 2 && PlayerProgress.Instance.twoMilionAward == true)
            whichChar++;
        if (whichChar<2)
            whichChar++;

        ChangeCharacter();
    }

    public void CharacterLeft()
    {
        if(whichChar>0)
            whichChar--;
        ChangeCharacter();
    }

    public void ChangeCharacter()
    {
        switch (whichChar)
        {
            case 0:
                Gimper.SetActive(true);
                Lysy.SetActive(false);
                SonGimper.SetActive(false);
                Tata.SetActive(false);
                player = Gimper;
                break;
            case 1:
                Gimper.SetActive(false);
                Lysy.SetActive(true);
                SonGimper.SetActive(false);
                Tata.SetActive(false);
                player = Lysy;
                break;
            case 2:
                Gimper.SetActive(false);
                Lysy.SetActive(false);
                SonGimper.SetActive(true);
                Tata.SetActive(false);
                player = SonGimper;
                break;
            case 3:
                Gimper.SetActive(false);
                Lysy.SetActive(false);
                SonGimper.SetActive(false);
                Tata.SetActive(true);
                player = Tata;
                break;
        }
    }

    private void ModeBack()
    {
        if (whichRoom == 0)
        {
            Camera.main.transform.position = new Vector3(0.0f, 12.0f, -4.0f);
            Camera.main.transform.rotation = Quaternion.Euler(40, 0, 0);
        }
        if (whichRoom == 1)
        {
            Camera.main.transform.position = new Vector3(6.0f, 12.0f, -10.0f);
            Camera.main.transform.rotation = Quaternion.Euler(40, 90, 0);
        }
        if (whichRoom == 3)
        {
            Camera.main.transform.position = new Vector3(0.0f, 12.0f, -16.0f);
            Camera.main.transform.rotation = Quaternion.Euler(40, 180, 0);
        }
    }
    
    public void DelayModeBack(float delayTime)
    {
        Invoke("ModeBack", delayTime);
    }
    

    private void ToMode()
    {
        Camera.main.transform.position = new Vector3(0.0f, -8f, -4.0f);
        Camera.main.transform.rotation = Quaternion.Euler(40, 0, 0);
    }

    public void DelayToMode(float delayTime)
    {
        Invoke("ToMode", delayTime);
    }    


}
