using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    public Button continueButton;
    public Button restartButton;
    public Button quitButton;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        continueButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() => { UIManager.Instance.PausePanelToggle(); });
        restartButton.onClick.AddListener(() => { SceneManager.LoadScene(0); });
        quitButton.onClick.AddListener(() => { Application.Quit(); });
        AudioManager.Instance.masterMixer.SetFloat("MusicVol", musicSlider.value); //Mathf.Log10(musicSlider.value)
        AudioManager.Instance.masterMixer.SetFloat("SFXsVol", sfxSlider.value); //Mathf.Log10(musicSlider.value)
        musicSlider.onValueChanged.AddListener(delegate { AudioManager.Instance.masterMixer.SetFloat("MusicVol", musicSlider.value); });
        sfxSlider.onValueChanged.AddListener(delegate { AudioManager.Instance.masterMixer.SetFloat("SFXsVol", sfxSlider.value); });
    }
}
