
using System;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    [Header("Player Settings")]
    [Range(0.1f, 10f)] public float mouseSensitivity;
    [Range(60f, 120f)] public float fieldOfView = 90f;

    [SerializeField] private GameObject audioPanel;

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
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 2f);
        fieldOfView = PlayerPrefs.GetFloat("FOV", 90f);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
        PlayerPrefs.SetFloat("FieldOfView", fieldOfView);
        PlayerPrefs.Save(); 
    }

    public void OpenAudio()
    {
        AudioManager.instance.Open();
    }
}
