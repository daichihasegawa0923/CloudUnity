﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOnGame : MonoBehaviour
{
    [SerializeField] protected GameObject _panel;
    [SerializeField] protected Button _openClose;
    [SerializeField] protected Slider _soundVolumeSlider;

    [SerializeField] private float _panelMove = 400;

    private void Awake()
    {
        _soundVolumeSlider.onValueChanged.AddListener(value => 
        {
            foreach (var audioSouce in FindObjectsOfType<AudioSource>())
                audioSouce.volume = value;
        });

        _openClose.onClick.AddListener(() =>
        {
            var buttonText = _openClose.gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
            var panelPosition = _panel.transform.position;
            if (buttonText.text == "Open")
            {
                panelPosition.x += _panelMove;
                buttonText.text = "Close";
            }
            else
            {
                panelPosition.x -= _panelMove;
                buttonText.text = "Open";
            }

            _panel.transform.position = panelPosition;
        });
    }
}
