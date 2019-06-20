using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI muteText;

    private static bool isMute = false;
    private static int indexQuality = 0;
    private static float volumeOfSound = 1.0f;
    private static float lastVolumeOfSound = 0.0f;
    private static GameObject soundSlider;
    private static GameObject soundToggle;
    public static ToggleGroup graphic_quality;
    
    // Start is called before the first frame update


    

    void Start()
    {
        soundSlider = GameObject.Find("sound_slider");
        soundToggle = GameObject.Find("checkbox_sound");
        graphic_quality = GameObject.Find("graphic_quality").GetComponent<ToggleGroup>();
        soundSlider.GetComponent<Slider>().value = volumeOfSound;
        soundToggle.GetComponent<Toggle>().isOn = isMute;
        SetMuteText();
        SetGraphicQualityLevel(indexQuality);

    }

    void Update() {
        
    }

    public void SetHighQuality(bool on)
    {
        if (on) {
            indexQuality = 5;
            SetGraphicQualityLevel(indexQuality);
        }
    }

    public void SetMediumQuality(bool on)
    {
        if (on) {
            indexQuality = 3;
            SetGraphicQualityLevel(indexQuality);
        }
        
    }

    public void SetLowQuality(bool on)
    {
        if (on) {
            indexQuality = 0;
            SetGraphicQualityLevel(indexQuality);
        }
    }

    public static void SetGraphicQualityLevel(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel, false);
    }


    public static int GetIndexQuality()
    {
        return indexQuality;
    }

    public static void SetIndexQuality(int indexQualityLevel)
    {
        
        indexQuality = indexQualityLevel;
        SetGraphicQualityLevel(indexQuality);
        var toggles = graphic_quality.GetComponentInChildren<Toggle>();
        
        
    }

    public void SetSoundVolume()
    {
        volumeOfSound = soundSlider.GetComponent<Slider>().value;
        if (soundSlider.GetComponent<Slider>().value > 0)
        {
            isMute = false;
            soundToggle.GetComponent<Toggle>().isOn = isMute;
            SetMuteText();
        }
        else
        {
            isMute = true;
            soundToggle.GetComponent<Toggle>().isOn = isMute;
            SetMuteText();
        }
    }

    public static float GetSoundVolume()
    {
        return volumeOfSound;
    }

    public static void SetSoundVolume(float soundVolume)
    {
        volumeOfSound = soundVolume;
    }

    public static float GetLastSoundVolume()
    {
        return lastVolumeOfSound;
    }

    public static void SetLastSoundVolume(float lastSoundVolume)
    {
        lastVolumeOfSound = lastSoundVolume;
    }


    public void SetMute()
    {
        isMute = !isMute;
        if (isMute)
        {
            SetMuteText();
            lastVolumeOfSound = volumeOfSound;
            volumeOfSound = 0.0f;
            soundSlider.GetComponent<Slider>().value = volumeOfSound;
        }
        else
        {
            SetMuteText();
            volumeOfSound = lastVolumeOfSound;
            soundSlider.GetComponent<Slider>().value = volumeOfSound;
        }
    }

    public static bool IsMuteEndabled()
    {
        return isMute;
    }

    public static void SetIsMute(bool isMuteValue)
    {
        isMute = isMuteValue;
    }

    public void SetMuteText()
    {
        if (isMute)
        {
            muteText.text = "Unmute";
        }
        else
        {
            muteText.text = "Mute";
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
