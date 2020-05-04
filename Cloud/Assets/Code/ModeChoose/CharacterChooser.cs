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
        _selectedStages.ForEach(selectedStage => 
        {
            var eventTrigger = selectedStage.CheckOrEmptyImage.gameObject.AddComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;

            entry.callback.AddListener((be) => {
                _selectedStages.ForEach(selectedStages2 => selectedStages2.RemoveCheck());
                selectedStage.Check();
            });

            eventTrigger.triggers.Add(entry);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
