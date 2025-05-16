using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //UI sliders
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundEffectSlider;
    public Slider uISlider;

    public GameObject audioPanel;
    public static AudioManager instance;

    [SerializeField] private string masterName = "MasterVolume";
    [SerializeField] private string musicName = "MusicVolume";
    [SerializeField] private string sfxName = "SoundEffectVolume";
    [SerializeField] private string uiName = "UIVolume";

    private void Start()
    {
        GetValue(masterName);
        GetValue(musicName);
        GetValue(sfxName);
        GetValue(uiName);
    }

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetMasterValue(float value)
    {
        audioMixer.SetFloat(masterName, value);
        PlayerPrefs.SetFloat(masterName, value);
        PlayerPrefs.Save();
    }

    public void SetMusicValue(float value)
    {
        audioMixer.SetFloat(musicName, value);
        PlayerPrefs.SetFloat(musicName, value);
        PlayerPrefs.Save();
    }

    public void SetSoundEffectValue(float value)
    {
        audioMixer.SetFloat(sfxName, value);
        PlayerPrefs.SetFloat(sfxName, value);
        PlayerPrefs.Save();
    }

    public void SetUIValue(float value)
    {
        audioMixer.SetFloat(uiName, value);
        PlayerPrefs.SetFloat(uiName, value);
        PlayerPrefs.Save();
    }

    private void GetValue(string key)
    {
        float value = PlayerPrefs.GetFloat(key);
        audioMixer.SetFloat (key, value);
        switch(key) 
        {
            case "MasterVolume":
                masterSlider.value = value;
                break;
            case "MusicVolume":
                musicSlider.value = value;
                break;
            case "SoundEffectVolume":
                soundEffectSlider.value = value;
                break;
            case "UIVolume":
                uISlider.value = value;
                break;
        }
        
    }

    public void Save()
    {
        SetMasterValue(masterSlider.value);
        SetMusicValue(musicSlider.value);
        SetSoundEffectValue(soundEffectSlider.value);
        SetUIValue(uISlider.value);
    }

    public void Back()
    {
        audioPanel.SetActive(false);
    }

    public void Open()
    {
        audioPanel.SetActive(true);
    }
}
