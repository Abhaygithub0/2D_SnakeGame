using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour

{
    private TextMeshPro m_TextMeshPro;
    private int score;
    public int increment = 10;
    // Start is called before the first frame update

    private void Awake()
    {
        m_TextMeshPro = GetComponent<TextMeshPro>();
    }
    void Start()
    {
       
        score = 0;
    }
    public void scoreUpdate()
    {
        score = score + increment;
        ScoreUpdateUI();    

    }
    void ScoreUpdateUI()
    {
        m_TextMeshPro.text =  " value : "+score;
    }
}