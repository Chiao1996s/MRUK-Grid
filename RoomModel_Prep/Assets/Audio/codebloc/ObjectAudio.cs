using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAudio : MonoBehaviour
{
    public AudioSource src;
    
    public AudioClip HoverAudio;
    public AudioClip GrabAudio;
    
    public void HoverVoice()
    {
        Debug.Log("HoverVoice called");
        src.clip = HoverAudio;
        src.Play();
    }

    public void GrabVoice()
    {
        Debug.Log("GrabVoice called");
        src.clip = GrabAudio;
        src.Play();
    }
}