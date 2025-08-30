using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static readonly string bgm = "bgmVol";
    public static readonly string sfx = "sfxVol";

    public AudioMixer audioMixer;

    private float bgmDb;
    private float sfxDb;

    public void SetBGMVolume(float volume)
    {
        bgmDb = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        audioMixer.SetFloat(bgm, bgmDb);
    }

    public void SetSFXVolume(float volume)
    {
        sfxDb = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        audioMixer.SetFloat(sfx, sfxDb);
    }

    public void SoundOnOff(bool isOn)
    {
        if (isOn)
        {
            audioMixer.SetFloat(bgm, bgmDb);
            audioMixer.SetFloat(sfx, sfxDb);
        }
        else
        {
            audioMixer.SetFloat(bgm, -80f);
            audioMixer.SetFloat(sfx, -80f);
        }
    }
}
