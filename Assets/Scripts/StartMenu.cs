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
    //Fields
    public GameObject Startbtn;
    public Button StartButton;
    public string VRContemplation;

    // Start is called before the first frame update
    void Start()
    {
        Button Startbtn = StartButton.GetComponent<Button>();
        Startbtn.onClick.AddListener(TaskOnClickStart);
    }

    //Task to start app (switch scenes to VR Contemplation)
    void TaskOnClickStart()
    {
        Debug.Log("You have clicked Start Button!");
        SceneManager.LoadScene(VRContemplation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
