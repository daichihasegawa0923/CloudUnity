using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterChooser : MonoBehaviour
{
    [SerializeField] List<SelectedStage> _selectedStages;

    // Start is called before the first frame update
    void Start()
    {
        // 第一要素だけチェックを付ける
        _selectedStages[0].Check();
        PlaySetting.playCharacterResoucesName = _selectedStages[0].Name;

        // イベントハンドラの追加
        this.AddEvent();
    }

    private void AddEvent()
    {
        _selectedStages.ForEach(selectedStage =>
        {
            var eventTrigger = selectedStage.CheckOrEmptyImage.gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;

            entry.callback.AddListener((be) => {
                _selectedStages.ForEach(selectedStages2 => selectedStages2.RemoveCheck());
                selectedStage.Check();
                PlaySetting.playCharacterResoucesName = selectedStage.Name;
            });

            eventTrigger.triggers.Add(entry);
        });
    }
}
