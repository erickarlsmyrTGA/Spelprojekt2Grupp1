using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyAudioManagers : MonoBehaviour
{
    [SerializeField] private Slider myMusicVolumeSlider = null;
    [SerializeField] private Slider mySFXVolumeSlider = null;

    // Start is called before the first frame update
    void Start()
    {
        SettingsDataManager.ourInstance.SetMusicVolumeSlider(myMusicVolumeSlider);
        SettingsDataManager.ourInstance.SetSFXVolumeSlider(mySFXVolumeSlider);
        SettingsDataManager.ourInstance.Load();
        SettingsDataManager.ourInstance.UpdateSliders();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSFX()
    {
        // Update music and volume temp data
        var settingsDM = SettingsDataManager.ourInstance;
        settingsDM.UpdateSFXData();

        // Update AudioManager (music mixers)
        GameManager.ourInstance.myAudioManager.SetVolumesFromOptionsDataManager();
    }

    public void UpdateMusic()
    {
        // Update music and volume temp data
        var settingsDM = SettingsDataManager.ourInstance;
        settingsDM.UpdateMusicData();

        // Update AudioManager (music mixers)
        GameManager.ourInstance.myAudioManager.SetVolumesFromOptionsDataManager();
    }


}

