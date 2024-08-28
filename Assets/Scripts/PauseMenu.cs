using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Security.Authentication.ExtendedProtection;
using System.Globalization;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Public Fields
    public GameObject PauseBtn;
    public GameObject PlayBtn;
    public GameObject ExitBtn;
    public GameObject BlackImage;
    public AudioSource audioSource; //Using lower case for consistency
    public AudioSource streamAudioSource;
    public Button PauseButton;
    public Button PlayButton;
    public Button ExitButton;
    public static bool GamePaused { get; private set; } = false;
    public Animator animator;

    //Private Fields
    private bool controlEnable = true;
    private bool musicPaused = false;
    private bool changeScene = false;
    private int startMenu;

    // Start is called before the first frame update
    void Start()
    {
        //Pause button code
        Button Pause = PauseButton.GetComponent<Button>();
        Pause.onClick.AddListener(TaskOnClickPause);
        //Play button code
        Button Play = PlayButton.GetComponent<Button>();
        Play.onClick.AddListener(TaskOnClickPlay);
        //Exit to Menu button code
        Button Exit = ExitButton.GetComponent<Button>();
        Exit.onClick.AddListener(TaskOnClickExit);

        BlackImage.gameObject.SetActive(false);
        PlayBtn.gameObject.SetActive(false);
        ExitBtn.gameObject.SetActive(false);

    }

    //Task to pause game
    void TaskOnClickPause()
    {
        Debug.Log("Game Paused");
        if (!GamePaused)
        {
            Time.timeScale = 0;
            GamePaused = true;
            controlEnable = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            BlackImage.gameObject.SetActive(true);
            PlayBtn.gameObject.SetActive(true);
            ExitBtn.gameObject.SetActive(true);
        }
        //Mute audio
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            musicPaused = true;
        }

        //Mute stream audio
        if (streamAudioSource.isPlaying)
        {
            streamAudioSource.Pause();
            musicPaused = true;
        }

    }

    //Task to play game
    void TaskOnClickPlay()
    {
        Debug.Log("Resuming Game");
        if (GamePaused)
        {
            Time.timeScale = 1;
            GamePaused = false;
            controlEnable = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            BlackImage.gameObject.SetActive(false);
            PlayBtn.gameObject.SetActive(false);
            ExitBtn.gameObject.SetActive(false);
        }
        //Unmute audio
        if (musicPaused)
        {
            audioSource.UnPause();
            streamAudioSource.UnPause();
            musicPaused = false;
        }
    }

    //Task for selecting exit button
    void TaskOnClickExit()
    {
        Debug.Log("Exiting back to menu...");
        BlackImage.gameObject.SetActive(false);
        PlayBtn.gameObject.SetActive(false);
        ExitBtn.gameObject.SetActive(false);
        changeScene = true;
        SceneManager.LoadScene(startMenu);
    }

    //Task to start app (switch scenes to VR Contemplation)
    void TaskOnClickStart()
    {
        Debug.Log("You have clicked Start Button!");
        changeScene = true;
    }

    //Method to trigger the FadeOut animation
    public void FadeToScene(int sceneIndex)
    {
        startMenu = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(startMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            if(GamePaused)
            {
                TaskOnClickPlay();
            }

            else 
            {
                TaskOnClickPause();
            }
        }

        if (changeScene)
        {
            FadeToScene(startMenu);
            changeScene = false;
        }
    }
}
