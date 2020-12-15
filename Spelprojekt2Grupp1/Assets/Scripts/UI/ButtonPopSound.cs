using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPopSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonPressedSound()
    {
        GameManager.ourInstance.myAudioManager.PlaySFXClip("Pop_Press");
    }
}
