using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    [SerializeField] private Button Level1,Level2;

    private void Start()
    {
        Level1.onClick.AddListener(playscene);
        Level2.onClick.AddListener(playscene2);
    }

    private void playscene()
    {
        SoundManager.Instance.playclip(AudioType.Button);
        SceneManager.LoadScene(1);
    }
    private void playscene2()
    {
        SoundManager.Instance.playclip(AudioType.Button);
        SceneManager.LoadScene(2);
    }
}
