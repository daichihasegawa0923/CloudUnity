using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOnGame : MonoBehaviour
{
    [SerializeField] protected GameObject _panel;
    [SerializeField] protected Button _openClose;
    [SerializeField] protected Slider _soundVolumeSlider;

    public bool IsMenuOpen { private set; get; }

    [SerializeField] private float _panelMove = 400;

    private void Awake()
    {
        // メニューは閉じている
        this.IsMenuOpen = false;

        // 音量の初期設定
        foreach (var audioSouce in FindObjectsOfType<AudioSource>())
        {
            audioSouce.volume = PlayerPrefs.GetFloat("sound_volume", 1.0f);
            _soundVolumeSlider.value = audioSouce.volume;
        }
        _soundVolumeSlider.onValueChanged.AddListener(value => 
        {
            foreach (var audioSouce in FindObjectsOfType<AudioSource>())
            {
                audioSouce.volume = value;
                PlayerPrefs.SetFloat("sound_volume", value);
            }
        });

        _openClose.onClick.AddListener(() =>
        {
            var buttonText = _openClose.gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
            var panelPosition = _panel.transform.position;
            if (buttonText.text == "Menu")
            {
                _panel.SetActive(true);
                buttonText.text = "Close";
                this.IsMenuOpen = true;
            }
            else
            {
                _panel.SetActive(false);
                buttonText.text = "Menu";
                this.IsMenuOpen = false;
            }

            _panel.transform.position = panelPosition;
        });
    }
}
