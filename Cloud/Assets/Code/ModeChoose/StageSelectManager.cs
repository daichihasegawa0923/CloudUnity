using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] private List<SelectedStage> _selectedStages;

    // Start is called before the first frame update
    void Start()
    {
        // 最初の要素に必ずチェックを入れる
        if(_selectedStages.Count > 0)
            _selectedStages[0].Check();

        AddEventToCheckbox();
    }
    


    private void AddEventToCheckbox()
    {
        _selectedStages.ForEach(s =>
        {
            var trigger = s.CheckOrEmptyImage.gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;


            entry.callback.AddListener(be =>
            {
                _selectedStages.ForEach(s2 => s2.RemoveCheck());
                s.Check();
            });

            trigger.triggers.Add(entry);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
