using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    [SerializeField] private Button play;

    private void Start()
    {
        play.onClick.AddListener(playscene);
    }

    private void playscene()
    {
        SoundManager.Instance.playclip(AudioType.Button);
        SceneManager.LoadScene(1);
    }
}
