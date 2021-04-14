
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioSource[] sounds;

    private void Start()
    {
        MuteSounds(!PlayerPrefs.GetString("SoundsMute", "false").Equals("false"));
    }

    public void MuteSounds(bool flag)
    {
        foreach(AudioSource s in sounds)
               s.mute = flag;
    }

    public void PlayClick()
    {
        sounds[0].Play();
    }

    public void PlayKnifeSlash(float pitch)
    {
        sounds[1].pitch = pitch;
        sounds[1].Play();
    }

    public void PlayCollectCoins()
    {
        sounds[2].Play();
    }

    public void PlaySqualch(float pitch)
    {
        sounds[3].pitch = pitch;
        sounds[3].Play(); 
    }

    public void PlayBuy()
    {
        sounds[4].Play();
    }

    public void PlayNextLevel()
    {
        sounds[5].Play();
    }

    public void PlayError()
    {
        sounds[6].Play();
    }

    public void PlayHit(float pitch)
    {
        sounds[7].pitch = pitch;
        sounds[7].Play();
    }


}
