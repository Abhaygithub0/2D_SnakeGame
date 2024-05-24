using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance {  get { return instance; } }

    public AudioSource soundFX;
    public AudioSource soundMusic;

    [SerializeField] private soundlist[] soundtypes;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

 public void playclip(AudioType sound)
    {
        AudioClip clip = getclip(sound);
        if (clip != null)
        {
            soundFX.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Clip not found for sound type: " + sound);
        }
    }
   
    private AudioClip getclip(AudioType sound)
    {
     soundlist item = Array.Find(soundtypes,item=>item.audiolist==sound);
        if (item != null)
      return item.clip;                        
        return null;
        
    }
}   


[Serializable]//ye isliye h q ki upper serializeField kaam kar sake or hm inspector m ja kar name or clip match kara sake .
public class soundlist
{
    public AudioType audiolist;//this is a string name but it will work in int value due to enum.
    public AudioClip clip;//this is a clip 
}

public enum AudioType
{
    Button,
    Levelload,
    death,
    pickablelight,
    pickableheavy
}