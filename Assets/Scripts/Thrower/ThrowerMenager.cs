using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerMenager : MonoBehaviour
{
    public GameObject PausePanel;
    public Animator deathMenuAnim;


    public GameObject Gimper;
    public GameObject Lysy;
    public GameObject SonGimper;
    public GameObject Tata;

    public bool gamePaused;
    public bool alive;

    public static ThrowerMenager Instance { set; get; }

    private void Awake()
    {
        gamePaused = false;
    }
    private void Start()
    {
        Instance = this;
        alive = true;

        switch (CameraMenu.Instance.whichChar)
        {

            case 0:
                Gimper.SetActive(true);
                Lysy.SetActive(false);
                SonGimper.SetActive(false);
                Tata.SetActive(false);

                break;
            case 1:
                Gimper.SetActive(false);
                Lysy.SetActive(true);
                SonGimper.SetActive(false);
                Tata.SetActive(false);

                break;
            case 2:
                Gimper.SetActive(false);
                Lysy.SetActive(false);
                SonGimper.SetActive(true);
                Tata.SetActive(false);
                break;
            case 3:
                Gimper.SetActive(false);
                Lysy.SetActive(false);
                SonGimper.SetActive(false);
                Tata.SetActive(true);
                break;


        }
    }
    public void OnBackButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void OnPauseButton()
    {
        gamePaused = true;
        PausePanel.SetActive(true);
    }

    public void ContiniueFromPause()
    {
        gamePaused = false;
        PausePanel.SetActive(false);
    }


    public void OnDeath()
    {
        gamePaused = true;
        alive = false;
        tMovement.Instance.anim.SetTrigger("Die");
        deathMenuAnim.SetTrigger("OnDeath");
    }

    public void OnPlayButton()
    {
        if (CameraMenu.Instance.whichRoom == 1)
            UnityEngine.SceneManagement.SceneManager.LoadScene("DefWS");
        if (CameraMenu.Instance.whichRoom == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("DefGym");
        if (CameraMenu.Instance.whichRoom == 3)
            UnityEngine.SceneManagement.SceneManager.LoadScene("DefIsaac");
    }
}
