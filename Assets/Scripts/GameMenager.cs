using UnityEngine;

public class GameMenager : MonoBehaviour
{
    public static GameMenager Instance { get; set; }
    public GameObject PausePanel;
    public Animator deathMenuAnim;
    public GameObject Gimper;
    public GameObject Lysy;
    public GameObject SonGimper;
    public GameObject Tata;

    public GameObject continueButton, fakePanel;

    public void Awake()
    {
        fakePanel.SetActive(false);
        continueButton.SetActive(true);

        Instance = this;
        PausePanel.SetActive(false);
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

    public void OnPlayButton()
    {
        if (CameraMenu.Instance.whichRoom == 1)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Run First");
        if (CameraMenu.Instance.whichRoom == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Gym");
        if (CameraMenu.Instance.whichRoom == 3)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Isaac");
    }
    public void OnBackButton()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void OnDeath()
    {
        deathMenuAnim.SetTrigger("Dead");
    }
    public void OnPauseButton()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }
    public void ContiniueFromPause()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
    public void ContiniueFromDeath()
    {
        continueButton.SetActive(false);
        fakePanel.SetActive(true);
        Move.Instance.ContinueFromDeath();
        deathMenuAnim.SetTrigger("FromDeath");
    }

 
}
