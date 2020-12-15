﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using UnityEngine.UI;

public class SettingsDataManager : MonoBehaviour
{

    [SerializeField] private Slider myMusicVolumeSlider = null;
    [SerializeField] private Slider mySFXVolumeSlider = null;

    public static SettingsDataManager ourInstance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (ourInstance != null && ourInstance != this)
        {
            Destroy(this);
        }
        else
        {
            ourInstance = this;
        }
        Load();
        myMusicVolumeSlider.value = MusicVolume;
        mySFXVolumeSlider.value = SFXVolume;
    }

    private SettingsData mySettingsData = new SettingsData();
    

    private static readonly string myFileName = "settingsData.conf";



    private static string GetFileName() => $"{Application.persistentDataPath}/{myFileName}";



    public void Save()
    {
        mySettingsData.myMusicVolume = myMusicVolumeSlider.value;
        mySettingsData.mySFXVolume = mySFXVolumeSlider.value;
        string json = JsonUtility.ToJson(mySettingsData);
        string fileName = GetFileName();

        FileStream fileStream = new FileStream(path: fileName, mode: FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
#if UNITY_EDITOR
        Debug.Log("Options settings saved!");
#endif
    }

    public bool Load()
    {
        string fileName = GetFileName();

        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            JsonUtility.FromJsonOverwrite(json, mySettingsData);            
            return true;
#if UNITY_EDITOR
            Debug.Log("Options settings loaded!");
#endif
        }

        return false;
    }
   
    public void Delete() => File.Delete(GetFileName());

        
    public float MusicVolume
    {
        get => mySettingsData.myMusicVolume;
        set => mySettingsData.myMusicVolume = value;
    }

    public float SFXVolume
    {
        get => mySettingsData.mySFXVolume;
        set => mySettingsData.mySFXVolume = value;
    }
}
