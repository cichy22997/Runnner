
using System;
using TMPro;
using UnityEngine;

public class MenuMenager : MonoBehaviour
{

    public Animator changeRoom;
    public Animator menuRooms;
    public Animator playAnim;
    public Animator startButton;
    public Animator functionButtons;
    public Animator arrows;
    public Animator options;
    public Animator CharArrow;
    public Animator Boxes;
    public Animator Mode;

    public static bool soundsOn = true;
    public static bool musicOn = true;

    public GameObject musicOffBtn, musicOnBtn, soundsOffBtn, soundsOnBtn, infoMenu;

    public float delayTime;

    private static bool firstPlay = false;
    private bool optionsActive = false;
    private bool infoActive = false;



    public void OnRightButton()
    {
        if (ClickTime.Instance.lastClickTime > 1.1f)
        {
            changeRoom.SetTrigger("changeRoom");
            CameraMenu.Instance.DelayToRight(delayTime);

            if (CameraMenu.Instance.whichRoom == 0)
            {
                menuRooms.SetTrigger("GymDown");
                menuRooms.SetTrigger("WSUp");
                playAnim.SetTrigger("isChanging");
            }
            if (CameraMenu.Instance.whichRoom == 1)
            {
                menuRooms.SetTrigger("WSDown");
                menuRooms.SetTrigger("IsaacUp");
                playAnim.SetTrigger("isChanging");
            }

            if (CameraMenu.Instance.whichRoom == 2)
            {
                menuRooms.SetTrigger("CRDown");
                menuRooms.SetTrigger("GymUp");
                playAnim.SetTrigger("ButtonAppear");
                CharArrow.SetTrigger("CharArDisappear");

            }
        }

    }

    public void OnLeftButton()
    {
        if (ClickTime.Instance.lastClickTime > 1.1f)
        {
            changeRoom.SetTrigger("changeRoom");
            CameraMenu.Instance.DelayToLeft(delayTime);

            if (CameraMenu.Instance.whichRoom == 0)
            {
                menuRooms.SetTrigger("GymDown");
                menuRooms.SetTrigger("CRUp");
                playAnim.SetTrigger("ButtonDisappear");
                CharArrow.SetTrigger("CharArAppear");
            }

            if (CameraMenu.Instance.whichRoom == 1)
            {
                menuRooms.SetTrigger("WSDown");
                menuRooms.SetTrigger("GymUp");
                playAnim.SetTrigger("isChanging");
            }
            if (CameraMenu.Instance.whichRoom == 3)
            {
                menuRooms.SetTrigger("IsaacDown");
                menuRooms.SetTrigger("WSUp");
                playAnim.SetTrigger("isChanging");
            }
        }

    }

    public void onPlayButton()
    {
        changeRoom.SetTrigger("changeRoom");
        if (CameraMenu.Instance.whichRoom == 1)
            menuRooms.SetTrigger("WSDown");
        if (CameraMenu.Instance.whichRoom == 0)
            menuRooms.SetTrigger("GymDown");
        if (CameraMenu.Instance.whichRoom == 3)
            menuRooms.SetTrigger("IsaacDown");

        playAnim.SetTrigger("ButtonDisappear");
        functionButtons.SetTrigger("ToMode");
        arrows.SetTrigger("ToTitle");
        Boxes.SetTrigger("BoxDisappear");

        Mode.SetTrigger("ToMode");

        CameraMenu.Instance.DelayToMode(delayTime);
    }

    public void onBackToMapsButton()
    {
        changeRoom.SetTrigger("changeRoom");

        Mode.SetTrigger("ToMaps");
        playAnim.SetTrigger("ButtonAppear");
        functionButtons.SetTrigger("ToMaps");
        arrows.SetTrigger("ToRooms");
        Boxes.SetTrigger("BoxAppear");

        if (CameraMenu.Instance.whichRoom == 1)
            menuRooms.SetTrigger("WSUp");
        if (CameraMenu.Instance.whichRoom == 0)
            menuRooms.SetTrigger("GymUp");
        if (CameraMenu.Instance.whichRoom == 3)
            menuRooms.SetTrigger("IsaacUp");

        CameraMenu.Instance.DelayModeBack(delayTime);
    }
    public void OnBackButton()
    {
        if (ClickTime.Instance.lastClickTime > 1.1f)
        {
            if (CameraMenu.Instance.whichRoom == 0)
            {
                changeRoom.SetTrigger("changeRoom");
                CameraMenu.Instance.DelayToTitleScreen(delayTime);
                menuRooms.SetTrigger("GymDown");
            }
            if (CameraMenu.Instance.whichRoom == 1)
            {
                changeRoom.SetTrigger("changeRoom");
                CameraMenu.Instance.DelayToTitleScreen(delayTime);
                menuRooms.SetTrigger("WSDown");
            }
            if (CameraMenu.Instance.whichRoom == 2)
            {
                changeRoom.SetTrigger("changeRoom");
                CameraMenu.Instance.DelayToTitleScreen(delayTime);
                menuRooms.SetTrigger("CRDown");
            }
            if (CameraMenu.Instance.whichRoom == 3)
            {
                changeRoom.SetTrigger("changeRoom");
                CameraMenu.Instance.DelayToTitleScreen(delayTime);
                menuRooms.SetTrigger("IsaacDown");
            }
            playAnim.SetTrigger("ButtonDisappear");
            startButton.SetTrigger("ToTitle");
            functionButtons.SetTrigger("ToTitle");
            arrows.SetTrigger("ToTitle");
            Boxes.SetTrigger("BoxDisappear");
        }
    }
    public void OnStartButton()
    {
        if (!firstPlay)
        {
            firstPlay = true;
            PlayerProgress.Instance.LoadProgress();
        }


        menuRooms.SetTrigger("GymUp");
        changeRoom.SetTrigger("changeRoom");
        CameraMenu.Instance.DelayToScenes(delayTime);
        playAnim.SetTrigger("ButtonAppear");
        startButton.SetTrigger("ToRooms");
        functionButtons.SetTrigger("ToRooms");
        arrows.SetTrigger("ToRooms");
        Boxes.SetTrigger("BoxAppear");
    }


    public void OnOptions()
    {
        if (optionsActive)
        {
            optionsActive = false;
            options.SetTrigger("OptionsDisappear");
        }
        else
        {
            optionsActive = true;
            options.SetTrigger("OptionsAppear");
        }

    }


    public void MusicOn()
    {
        musicOnBtn.SetActive(true);
        musicOn = true;
        Sounds.Instance.mainMusic.mute = false;
    }

    public void MusicOff()
    {
        musicOnBtn.SetActive(false);
        musicOn = false;
        Sounds.Instance.mainMusic.mute = true;
    }
    public void SoundsOn()
    {
        soundsOnBtn.SetActive(true);
        soundsOn = true;
    }

    public void SoundsOff()
    {
        soundsOnBtn.SetActive(false);
        soundsOn = false;
    }

    public void onExitButton()
    {
        Application.Quit();
    }


    public void ChangeRight()
    {
        CameraMenu.Instance.CharacterRight();
    }
    public void ChangeLeft()
    {
        CameraMenu.Instance.CharacterLeft();
    }

    public void onRunner()
    {
        if (CameraMenu.Instance.whichRoom == 1)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Run First");
        if (CameraMenu.Instance.whichRoom == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Gym");
        if (CameraMenu.Instance.whichRoom == 3)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Isaac");
    }

    public void OnTowerDefence()
    {
        if (CameraMenu.Instance.whichRoom == 1)
            UnityEngine.SceneManagement.SceneManager.LoadScene("DefWS");
        if (CameraMenu.Instance.whichRoom == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("DefGym");
        if (CameraMenu.Instance.whichRoom == 3)
            UnityEngine.SceneManagement.SceneManager.LoadScene("DefIsaac");
    }
    public void OpenInfo()
    {
        if (infoActive)
        {
            infoActive = false;
            infoMenu.SetActive(false);
        }
        else
        {
            infoActive = true;
            infoMenu.SetActive(true);
        }
    }

}
