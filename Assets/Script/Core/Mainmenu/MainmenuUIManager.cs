using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainmenuUIManager : MonoBehaviour
{
    [Required]
    public Button NewGameButton;
    [Required]
    public Button TutorialButton;
    [Required]
    public Button ExitButton;

    [Scene]
    public string GameScene;

    public TutorialPopup TutorialPopup;

    private void Awake()
    {
        NewGameButton.onClick.AddListener(NewGame_OnClick);
        TutorialButton.onClick.AddListener(Tutorial_OnClick);
        ExitButton.onClick.AddListener(Exit_OnClick);
    }

    private void Tutorial_OnClick()
    {
        TutorialPopup.Open();
    }

    private void Exit_OnClick()
    {
        Application.Quit();
    }

    private void NewGame_OnClick()
    {
        SceneManager.LoadScene(GameScene);
    }
}
