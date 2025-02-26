using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Security.Authentication.ExtendedProtection;
using System.Globalization;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    //Public Fields
    public GameObject Startbtn;
    public Button StartButton;
    public Button ExitButton;
    public Animator animator;

    //Private Fields
    private int ContemplationZone;
    private bool changeScene = false;

    // Start is called before the first frame update
    void Start()
    {
        Button Startbtn = StartButton.GetComponent<Button>();
        Startbtn.onClick.AddListener(TaskOnClickStart);

        Button Exitbtn = ExitButton.GetComponent<Button>();
        Exitbtn.onClick.AddListener(TaskOnClickExit);
    }

    //Task to start app (switch scenes to VR Contemplation)
    void TaskOnClickStart()
    {
        Debug.Log("You have clicked Start Button!");
        changeScene = true;
    }

    //Task to exit app
    void TaskOnClickExit()
    {
        Debug.Log("You have clicked Exit Button!");
        Application.Quit();
    }

    //Method to trigger the FadeOut animation
    public void FadeToScene(int sceneIndex)
    {
        ContemplationZone = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(ContemplationZone);
    }

    // Update is called once per frame
    void Update()
    {
        if(changeScene == true)
        {
            FadeToScene(1);
        }
    }
}
