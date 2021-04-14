using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSettings : MonoBehaviour
{
    public SoundsManager soundsManager;
    public Toggle soundsToggle;
    public Toggle vibrationToggle;


    private void Start()
    {
        soundsToggle.isOn = PlayerPrefs.GetString("SoundsMute", "false").Equals("false");
        vibrationToggle.isOn = PlayerPrefs.GetString("VibrationMute", "false").Equals("false");
    }

    public void OnSoundsToggleClick()
    {
        if(soundsToggle.isOn)
            PlayerPrefs.SetString("SoundsMute", "false");
        else
            PlayerPrefs.SetString("SoundsMute", "true");
        soundsManager.MuteSounds(!soundsToggle.isOn);
    }
    
    public void OnVibrationToggleClick()
    {
        if (vibrationToggle.isOn)
            PlayerPrefs.SetString("VibrationMute", "false");
        else
            PlayerPrefs.SetString("VibrationMute", "true");
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://udargames.blogspot.com/2019/11/privacy-policy-built-fruit-hit-app-as.html");
    }
}
