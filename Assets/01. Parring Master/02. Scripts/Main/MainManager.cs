using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startButton;

    private void Start()
    {
        InitAddButtonListener();
    }

    private void InitAddButtonListener()
    {
        startButton.onClick.AddListener(() => StartGameScene());
    }

    public void StartGameScene() 
    {
        SceneChanger.LoadScene("Game");
    }
}
