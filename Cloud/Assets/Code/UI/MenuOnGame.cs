using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuOnGame : MonoBehaviour
{
    [SerializeField] protected GameObject _panel;
    [SerializeField] protected Button _openClose;
    [SerializeField] protected Slider _soundVolumeSlider;

    public bool IsMenuOpen { private set; get; } = false;

    [SerializeField] private float _panelMove = 400;

    [SerializeField] private Image _openCloseButtonImage;
    [SerializeField] private Sprite _humbergerButton;
    [SerializeField] private Sprite _closeButton;


    private void Awake()
    {
        // MenuButtonのアニメーション
        var menuButtonScale = this._openClose.transform.localScale;
        this._openClose.transform.localScale = Vector3.zero;
        DOTween.Sequence().Append(this._openClose.transform.DOScale(menuButtonScale*1.2f, 1.0f))
        .Append(this._openClose.transform.DOScale(menuButtonScale, 0.5f));

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
            var panelPosition = _panel.transform.position;
            if (!this.IsMenuOpen)
            {
                _panel.SetActive(true);
                _openCloseButtonImage.sprite = _closeButton;
                this.IsMenuOpen = true;
            }
            else
            {
                _panel.SetActive(false);
                _openCloseButtonImage.sprite = _humbergerButton;
                this.IsMenuOpen = false;
            }

            _panel.transform.position = panelPosition;
        });
    }
}
