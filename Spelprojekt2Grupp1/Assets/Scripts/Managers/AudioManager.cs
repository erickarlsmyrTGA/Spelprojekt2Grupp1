using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


// Based on code mined from SP1 - original written by Robin Krokfors and Zaid Neurothrone

//-------------------------------------------------------------------------
public class AudioManager : MonoBehaviour
{
    public enum AudioType
    {
        Music,
        SFX,
        Voice,
    }

    [System.Serializable]
    public struct Audio
    {
        public string myAudioName;

        public AudioClip myAudioClip;
    }

    #region Private Serializable Fields

    [SerializeField] private AudioMixer myAudioMixer;

    [Header("Mixers")]
    [SerializeField] private AudioMixerGroup myMusicMixer;
    [SerializeField] private AudioMixerGroup mySFXMixer;
    [SerializeField] private AudioMixerGroup myVoiceMixer;

    [SerializeField] private Audio[] myAudio;

    #endregion

    private static readonly int ourAudioCount = 16;

    private GameObject myAudioRoot;

    private Dictionary<string, AudioClip> myAudioStore = new Dictionary<string, AudioClip>();

    private Queue<AudioSource> myFreeAudioSourceQueue = new Queue<AudioSource>(ourAudioCount);

    private List<AudioSource> myPlayingSources = new List<AudioSource>(ourAudioCount);


    #region Public Methods

    public void SetMasterVolume(float aVolume)
    {
        myAudioMixer.SetFloat(name: "MasterVolume", value: MapVolumeToMixerVolume(aVolume));
    }

    public void SetMusicVolume(float aVolume)
    {
        myAudioMixer.SetFloat(name: "MusicVolume", value: MapVolumeToMixerVolume(aVolume));
    }

    public void SetSFXVolume(float aVolume)
    {
        myAudioMixer.SetFloat(name: "SFXVolume", value: MapVolumeToMixerVolume(aVolume));
    }

    public void SetVoiceVolume(float aVolume)
    {
        myAudioMixer.SetFloat(name: "VoiceVolume", value: MapVolumeToMixerVolume(aVolume));
    }

    public float MapVolumeToMixerVolume(float aVolume)
    {
        // aVolume 0 -> 100

        // More accurate db scaling than linear with volume slider
        float percentage = Mathf.Clamp01(aVolume / 100.0f);
        float volume = Mathf.Log10(Mathf.Lerp(0.0001f, 1.0f, percentage)) * 20.0f;

        return volume;
    }

    public void SetVolumesFromOptionsDataManager()
    {
        // TODO: read from saved sound settings
        //OptionsDataManager optionsDataManager = OptionsDataManager.ourInstance;
        //SetMasterVolume(optionsDataManager.MasterVolume);
        //SetMusicVolume(optionsDataManager.MusicVolume);
        //SetSFXVolume(optionsDataManager.SFXVolume);
        //SetVoiceVolume(optionsDataManager.VoiceVolume);
    }

    #region Play Clips

    /// <summary>
    /// The returned value can be stopped by passing it to <see cref="Stop(AudioSource)"/>.
    /// Do not manually use <see cref="AudioSource.Stop"/> or <see cref="AudioSource.Pause"/> on the returned object!
    /// </summary>
    public AudioSource Play2D(AudioClip anAudioClip, AudioType anAudioType, float aPitch = 1.0f, float someVolume = 1.0f, bool aShouldLoop = false)
    {
        AudioMixerGroup mixer = GetMixerFromAudioType(anAudioType);

        AudioSource audioSource = GetFreeAudioSource();
        if (audioSource == null)
        {
            return null;
        }

        audioSource.gameObject.SetActive(true);
        audioSource.transform.position = Vector3.zero;

        audioSource.outputAudioMixerGroup = mixer;
        audioSource.spatialBlend = 0.0f;
        audioSource.clip = anAudioClip;
        audioSource.loop = aShouldLoop;
        audioSource.pitch = aPitch;
        audioSource.volume = someVolume;
        audioSource.Play();

        myPlayingSources.Add(audioSource);

        return audioSource;
    }

    /// <summary>
    /// The returned value can be stopped by passing it to <see cref="Stop(AudioSource)"/>.
    /// Do not manually use <see cref="AudioSource.Stop"/> or <see cref="AudioSource.Pause"/> on the returned object!
    /// </summary>
    public AudioSource Play3D(AudioClip anAudioClip, AudioType anAudioType, Vector3 aWorldPosition, float aPitch = 1.0f, float someVolume = 1.0f, bool aShouldLoop = false)
    {
        AudioMixerGroup mixer = GetMixerFromAudioType(anAudioType);

        AudioSource audioSource = GetFreeAudioSource();
        if (audioSource == null)
        {
            return null;
        }

        audioSource.gameObject.SetActive(true);
        audioSource.transform.position = aWorldPosition;

        audioSource.outputAudioMixerGroup = mixer;
        audioSource.spatialBlend = 1.0f;
        audioSource.clip = anAudioClip;
        audioSource.loop = aShouldLoop;
        audioSource.pitch = aPitch;
        audioSource.volume = someVolume;
        audioSource.Play();

        myPlayingSources.Add(audioSource);

        return audioSource;
    }
    //-------------------------------------------------
    public AudioSource PlayMusicClip(string anAudioName, float aPitch = 1.0f, float someVolume = 1.0f, bool aShouldLoop = false)
    {
        return Play2D(GetAudioClip(anAudioName), AudioType.Music, aPitch, someVolume, aShouldLoop);
    }

    //-------------------------------------------------
    public AudioSource PlaySFXClip(string anAudioName, float aPitch = 1.0f, float someVolume = 1.0f, bool aShouldLoop = false)
    {
        return Play2D(GetAudioClip(anAudioName), AudioType.SFX, aPitch, someVolume, aShouldLoop);
    }

    //-------------------------------------------------
    public AudioSource PlayVoiceClip(string anAudioName, float aPitch = 1.0f, float someVolume = 1.0f, bool aShouldLoop = false)
    {
        return Play2D(GetAudioClip(anAudioName), AudioType.Voice, aPitch, someVolume, aShouldLoop);
    }

    #endregion

    #region General

    public bool Stop(AudioSource anAudioSource)
    {
        int playingSourceIndex = myPlayingSources.IndexOf(anAudioSource);
        if (playingSourceIndex >= 0)
        {
            myPlayingSources.RemoveAt(playingSourceIndex);
            RecycleAudioSource(anAudioSource);

            return true;
        }

        return false;
    }

    //-------------------------------------------------
    /// <summary>
    /// Stops all sounds that are currently playing
    /// </summary>
    public void StopAllSounds()
    {
        for (int i = 0; i < myPlayingSources.Count; ++i)
        {
            AudioSource audioSource = myPlayingSources[i];
            RecycleAudioSource(audioSource);
        }

        myPlayingSources.Clear();
    }

    //-------------------------------------------------
    public void StopMusic()
    {
        for (int i = 0; i < myPlayingSources.Count; ++i)
        {
            AudioSource audioSource = myPlayingSources[i];

            if (audioSource.outputAudioMixerGroup == myMusicMixer)
            {
                myPlayingSources.RemoveAt(i);

                RecycleAudioSource(audioSource);

                --i;
            }
        }
    }

    public AudioClip GetAudioClip(string aAudioName)
    {
        Debug.Assert(myAudioStore.ContainsKey(aAudioName));

        return myAudioStore[aAudioName];
    }

    #endregion

    #endregion

    public AudioMixerGroup GetMixerFromAudioType(AudioType anAudioType)
    {
        switch (anAudioType)
        {
            case AudioType.Music:
                return myMusicMixer;
            case AudioType.SFX:
                return mySFXMixer;
            case AudioType.Voice:
                return myVoiceMixer;
        }

#if UNITY_EDITOR

        Debug.LogWarning("Invalid AudioType!");

#endif

        return mySFXMixer;
    }

    private AudioSource GetFreeAudioSource()
    {
        if (myFreeAudioSourceQueue.Count == 0)
        {
            Debug.LogWarning("AudioManager had to generate more audio sources!");

            return GenerateSource(myAudioRoot);
        }

        return myFreeAudioSourceQueue.Count > 0 ? myFreeAudioSourceQueue.Dequeue() : null;
    }

    private void ResetAudioSource(AudioSource anAudioSource)
    {
        if (anAudioSource.isPlaying)
        {
            anAudioSource.Stop();
        }

        // Reset 3D settings
        anAudioSource.dopplerLevel = 1.0f;
        anAudioSource.spread = 0.0f;
        anAudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        anAudioSource.minDistance = 1.0f;
        anAudioSource.maxDistance = 500.0f;

        anAudioSource.loop = false;
        anAudioSource.playOnAwake = false;

        anAudioSource.clip = null;
        anAudioSource.outputAudioMixerGroup = null;
    }

    private void RecycleAudioSource(AudioSource anAudioSource)
    {
        ResetAudioSource(anAudioSource);

        anAudioSource.gameObject.SetActive(false);

        myFreeAudioSourceQueue.Enqueue(anAudioSource);
    }

    private AudioSource GenerateSource(GameObject aParent)
    {
        GameObject audioSourceGo = new GameObject("PooledAudioSource");
        audioSourceGo.transform.SetParent(aParent.transform);

        AudioSource audioSource = audioSourceGo.AddComponent<AudioSource>();

        ResetAudioSource(audioSource);

        audioSourceGo.SetActive(false);

        return audioSource;
    }

    private void GenerateAudioQueueBuffers()
    {
        myAudioRoot = new GameObject("Sources");
        myAudioRoot.transform.SetParent(transform);

        for (int i = 0; i < ourAudioCount; ++i)
        {
            myFreeAudioSourceQueue.Enqueue(GenerateSource(myAudioRoot));
        }
    }

    private void FillAudioStore()
    {
        myAudioStore.Clear();

        // Store audio clips in a more efficient container for near constant time indexing by key O(1)
        for (int i = 0; i < myAudio.Length; ++i)
        {
            myAudioStore.Add(myAudio[i].myAudioName, myAudio[i].myAudioClip);
        }
    }

    private void Update()
    {
        for (int i = 0; i < myPlayingSources.Count; ++i)
        {
            AudioSource audioSource = myPlayingSources[i];
            if (!audioSource.isPlaying)
            {
                myPlayingSources.RemoveAt(i);

                RecycleAudioSource(audioSource);

                --i;
            }
        }
    }

    private void Start()
    {
        SetVolumesFromOptionsDataManager();
    }

    private void Awake()
    {
        FillAudioStore();
        GenerateAudioQueueBuffers();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            FillAudioStore();
        }
    }
#endif
}
