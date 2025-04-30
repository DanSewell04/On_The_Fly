using System;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    [Header("Audio Settings")]
    [Range(0f,1f)] public float masterVolume = 1f;

    [Header("Player Settings")]
    [Range(0.1f, 10f)] public float mouseSensitivity;
    [Range(60f, 120f)] public float fieldOfView = 90f;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }

    public void SetFOV(float fov)
    {
        fieldOfView = fov;
        PlayerPrefs.SetFloat("FieldOfView", fov);
    }

    private void LoadSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 2f);
        fieldOfView = PlayerPrefs.GetFloat("FOV", 90f);

        AudioListener.volume = masterVolume;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        PlayerPrefs.SetFloat("FieldOfView", fieldOfView);
        PlayerPrefs.Save(); 
    }
}
