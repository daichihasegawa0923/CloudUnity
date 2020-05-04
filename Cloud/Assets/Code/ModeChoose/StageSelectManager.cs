using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] private PlaySetting.GameMode gameMode;

    [SerializeField] private List<SelectedStage> _selectedStages;

    [SerializeField] private List<SelectedStage> _treasureModeStages;
    [SerializeField] private GameObject _treasureModeStagesParent;

    [SerializeField] private List<SelectedStage> _battleModeStages;
    [SerializeField] private GameObject _battleModeStagesParent;

    [SerializeField] private Text _modeText;

    // Start is called before the first frame update
    void Start()
    {
        // 最初の要素に必ずチェックを入れる
        if(_selectedStages.Count > 0)
            _selectedStages[0].Check();

        AddEventToCheckbox();
    }

    private void InitializeStages(GameObject listsParentObject,List<SelectedStage> stages)
    {
        listsParentObject.SetActive(true);
        if (stages.Count > 0)
            stages[0].Check();

        AddEventToCheckbox(stages);
    }

    public void InitializeMode()
    {
        if(PlaySetting.gameMode == PlaySetting.GameMode.battle)
        {
            _modeText.text = "バトルモード";
            this.InitializeStages(this._battleModeStagesParent,this._battleModeStages);
        }
        else
        {
            _modeText.text = "宝探しモード";
            this.InitializeStages(this._treasureModeStagesParent, this._treasureModeStages);
        }
    }

    private void AddEventToCheckbox(List<SelectedStage> stages)
    {
        stages.ForEach(s =>
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

    public void SetSelectedStageName()
    {
        _selectedStages.ForEach(s=>
        {
            if(s.IsSelected)
            {
                PlaySetting.playSceneName = s.Name;
                return;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
