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

    //Start is called before the first frame update
    void Start()
    {
        Button Pause = PauseButton.GetComponent<Button>();
        Pause.onClick.AddListener(TaskOnClickPause);
        Button Play = PlayButton.GetComponent<Button>();
        Play.onClick.AddListener(TaskOnClickPlay);
        Button Exit = ExitButton.GetComponent<Button>();
        Exit.onClick.AddListener(TaskOnClickExit);

        BlackImage.gameObject.SetActive(false);
        PlayBtn.gameObject.SetActive(false);
        ExitBtn.gameObject.SetActive(false);
    }

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

            foreach (var audioSource in FindObjectsOfType<AudioSource>())
            {
                audioSource.Pause();
            }
        }
    }

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

        foreach (var audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.UnPause();
        }
    }

    void TaskOnClickExit()
    {
        Debug.Log("Exiting back to menu...");
        BlackImage.gameObject.SetActive(false);
        PlayBtn.gameObject.SetActive(false);
        ExitBtn.gameObject.SetActive(false);
        changeScene = true;
    }

    void TaskOnClickStart()
    {
        Debug.Log("You have clicked Start Button!");
        changeScene = true;
    }

    public void FadeToScene(int sceneIndex)
    {
        startMenu = sceneIndex;
        Debug.Log($"Fading to scene index {sceneIndex}");
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        Debug.Log("Fade complete. Loading scene...");
        SceneManager.LoadScene(startMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (GamePaused)
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
            Debug.Log("Starting scene transition...");
            FadeToScene(0);
            changeScene = false;
        }
    }
}

