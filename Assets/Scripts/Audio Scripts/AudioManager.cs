using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider BGMSlider; 
    [SerializeField] Slider SFXSlider;

    private void Start()
    {
        // Gets the volumes from playerprefs and sets them
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        BGMSlider.value = bgmVolume;
        mixer.SetFloat("BGMVolume", Mathf.Log10(bgmVolume) * 20);

        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        SFXSlider.value = sfxVolume;
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }

    // changes the Background Music Mixer's value
    public void ChangeBGM()
    {
        // Sets volume on Log scale and stores it in playerprefs
        mixer.SetFloat("BGMVolume", Mathf.Log10(BGMSlider.value) * 20);
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
    }

    // changes the Sound Effects Mixer's value
    public void ChangeSFX()
    {
        // Sets volume on Log scale and stores it in playerprefs
        mixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }
}
