using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void HoverSound()
    {
        myFx.volume = 0.4f;
        myFx.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        myFx.volume = 0.4f;
        myFx.PlayOneShot(clickFx);
    }
}
