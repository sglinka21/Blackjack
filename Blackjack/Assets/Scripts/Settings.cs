using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{
    public bool isMute;
    public int indexQuality;
    public float volumeOfSound;
    public float lastVolumeOfSound;

    public Settings(bool isMute, int indexQuality, float volumeOfSound, float lastVolumeOfSound)
    {
        this.isMute = isMute;
        this.indexQuality = indexQuality;
        this.volumeOfSound = volumeOfSound;
        this.lastVolumeOfSound = lastVolumeOfSound;
    }
}
