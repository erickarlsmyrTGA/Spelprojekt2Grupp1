
//-------------------------------------------------------------------------
[System.Serializable]
public class SettingsData
{
    // Audio Data
    public float myMusicVolume;
    public float mySFXVolume;

    public SettingsData()
    {
        myMusicVolume = 1.0f;
        mySFXVolume = 1.0f;
    }
}
