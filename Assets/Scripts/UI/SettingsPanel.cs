using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public Button closePanelButton;
    // 0 - sound, 1 - music, 2 - vibration
    public Button[] buttons;
    private Animator[] animators;
    private bool[] toggleStates;

    private Animator panelAnimator;

    private void Start()
    {
        panelAnimator = GetComponent<Animator>();
        animators = new Animator[buttons.Length];
        toggleStates = new bool[buttons.Length];
        toggleStates[0] = Data.Instance.s_sound;
        toggleStates[1] = Data.Instance.s_music;
        toggleStates[2] = Data.Instance.s_vibration;

        ButtonsSetup();
    }

    private void ButtonsSetup()
    {
        closePanelButton.onClick.RemoveAllListeners();
        closePanelButton.onClick.AddListener(() => { panelAnimator.SetBool("IsOpen", false); });

        for (int i = 0; i < buttons.Length; i++)
        {
            int k = i;
            buttons[k].onClick.RemoveAllListeners();
            buttons[k].onClick.AddListener(() => { Toggle(k); });

            animators[i] = buttons[i].GetComponent<Animator>();

            if (toggleStates[i])
            {
                animators[i].SetBool("IsOn", true);
                animators[i].SetBool("IsOff", false);
            }
            else
            {
                animators[i].SetBool("IsOn", false);
                animators[i].SetBool("IsOff", true);
            }
        }
    }

    private void Toggle(int _num)
    {
        toggleStates[_num] = !toggleStates[_num];

        if (_num == 0)
        {
            Data.Instance.s_sound = toggleStates[_num];
            PlayerPrefs.SetInt("s_sound", toggleStates[_num] ? 1 : 0);
            AudioManager.Instance.ToggleSFX();
        }
        else if (_num == 1)
        {
            Data.Instance.s_music = toggleStates[_num];
            PlayerPrefs.SetInt("s_music", toggleStates[_num] ? 1 : 0);
            AudioManager.Instance.ToggleMusic();
        }
        else if (_num == 2)
        {
            Data.Instance.s_vibration = toggleStates[_num];
            PlayerPrefs.SetInt("s_vibration", toggleStates[_num] ? 1 : 0);
        }

        if (toggleStates[_num])
        {
            animators[_num].SetBool("IsOn", true);
            animators[_num].SetBool("IsOff", false);
        }
        else
        {
            animators[_num].SetBool("IsOn", false);
            animators[_num].SetBool("IsOff", true);
        }
    }
}
