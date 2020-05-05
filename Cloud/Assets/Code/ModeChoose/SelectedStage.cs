using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedStage : MonoBehaviour
{


    private string _stageSceneName;
    public string StageSceneName { get => _stageSceneName; private set => _stageSceneName = value; }

    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private Sprite _checkedSprite;

    [SerializeField] private Image _checkOrEmptyImage;

    public bool IsSelected { private set { } get { return _checkOrEmptyImage.sprite.Equals(_checkedSprite); } }

    public Image CheckOrEmptyImage { get => _checkOrEmptyImage; set => _checkOrEmptyImage = value; }

    public string Name;

    public void Check()
    {
        this._checkOrEmptyImage.sprite = _checkedSprite;
    }

    public void RemoveCheck()
    {
        this._checkOrEmptyImage.sprite = _emptySprite;
    }

    private void AddEvent()
    {
        // チェックボックスのイメージを変更する処理
        var clickEvent = _checkOrEmptyImage.gameObject.AddComponent<EventTrigger>();
        var entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((baseEventData) =>
        {
            this._checkOrEmptyImage.sprite = this._checkOrEmptyImage.sprite.Equals(_emptySprite)
            ? this._checkOrEmptyImage.sprite = _checkedSprite
            : this._checkOrEmptyImage.sprite = _emptySprite;
        });
        clickEvent.triggers.Add(entry);
    }
}
