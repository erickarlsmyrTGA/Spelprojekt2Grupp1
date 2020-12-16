using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlayButtonSound()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Return");
    }
}
