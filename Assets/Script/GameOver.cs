using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Button exit, restart, lobby;

    private void Start()
    {
        exit.onClick.AddListener(exitmode);
        restart.onClick.AddListener(restartmode);
        lobby.onClick.AddListener(lobbymode);
        
    }

    private void lobbymode()
    {
        SoundManager.Instance.playclip(AudioType.Button);
        SceneManager.LoadScene(0);
    }

    private void restartmode()
    {
        SoundManager.Instance.playclip(AudioType.Button);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void exitmode()
    {
        SoundManager.Instance.playclip(AudioType.Button);
        Debug.Log("Exit");
    }
}
