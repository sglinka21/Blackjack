using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private string SETTING_PATH = "";

    void Start()
    {
        SETTING_PATH = Application.persistentDataPath + "/settings.dat";
        LoadSettingsFromFile();
    }

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void Quit()
    {
        SaveSettingsToFile();
        Application.Quit();
    }

    private void LoadSettingsFromFile()
    {
        if (File.Exists(SETTING_PATH))
        {
            BinaryFormatter bF = new BinaryFormatter();
            FileStream file = File.Open(SETTING_PATH, FileMode.Open);
            Settings settings = (Settings)bF.Deserialize(file);
            file.Close();
            SetSettings(settings);

        }
    }

    private void SetSettings(Settings settings)
    {
        SettingsController.SetSoundVolume(settings.volumeOfSound);
        SettingsController.SetLastSoundVolume(settings.lastVolumeOfSound);
        SettingsController.SetIsMute(settings.isMute);
        SettingsController.SetIndexQuality(settings.indexQuality);

    }

    private void SaveSettingsToFile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(SETTING_PATH);
        Settings settings = CreateSettings();
        bf.Serialize(file, settings);
        file.Close();
    }

    

    private Settings CreateSettings()
    {
        bool isMute = SettingsController.IsMuteEndabled();
        float soundVolume = SettingsController.GetSoundVolume();
        float lastSoundVolume = SettingsController.GetLastSoundVolume();
        int indexQuality = SettingsController.GetIndexQuality();
        return new Settings(isMute, indexQuality, soundVolume, lastSoundVolume);
    }
}
