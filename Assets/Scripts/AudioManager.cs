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
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    }

    // changes the Background Music Mixer's value
    public void ChangeBGM(float sliderValue)
    {
        // Sets volume on Log scale and stores it in playerprefs
        mixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGMVolume", sliderValue);
    }

    // changes the Sound Effects Mixer's value
    public void ChangeSFX(float sliderValue)
    {
        // Sets volume on Log scale and stores it in playerprefs
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderValue);
    }
}
