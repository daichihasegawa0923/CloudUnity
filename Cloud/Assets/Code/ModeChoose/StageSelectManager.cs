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

    [SerializeField] private Text _playerNumberText;
    [SerializeField] private byte _minimumPlayerNumber = 2;
    [SerializeField] private byte _maxPlayerNumber = 4;

    private readonly string MAX_PLAYER_NUMBER_STRAGED_NAME = "max_player_number";

    // Start is called before the first frame update
    void Start()
    {
        // 最初の要素に必ずチェックを入れる
        if(_selectedStages.Count > 0)
            _selectedStages[0].Check();

        AddEventToCheckbox();

        // 参加ユーザー数を保存してある値から取得
        PlaySetting.maxPlayerNum = (byte)PlayerPrefs.GetInt(MAX_PLAYER_NUMBER_STRAGED_NAME, this._minimumPlayerNumber);
        this._playerNumberText.text = PlaySetting.maxPlayerNum.ToString();
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
            this._treasureModeStagesParent.SetActive(false);
        }
        else
        {
            _modeText.text = "宝探しモード";
            this.InitializeStages(this._treasureModeStagesParent, this._treasureModeStages);
            this._battleModeStagesParent.SetActive(false);
        }
    }

    private void AddEventToCheckbox(List<SelectedStage> stages)
    {
        stages.ForEach(s =>
        {
            // 既にイベントが降られているとき、イベントを追加しない
            if (s.CheckOrEmptyImage.GetComponent<EventTrigger>())
                return;

            var trigger = s.CheckOrEmptyImage.gameObject.AddComponent<EventTrigger>();

            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;


            entry.callback.AddListener(be =>
            {
                stages.ForEach(s2 => s2.RemoveCheck());
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

    public void SetSelectedStage(List<SelectedStage> selectedStages)
    {
        selectedStages.ForEach(s =>
        {
            if (s.IsSelected)
            {
                Debug.Log(s.Name);
                PlaySetting.playSceneName = s.Name;
                return;
            }
        });
    }

    public void SetSelectedStagesToPlaySettings()
    {
        if(PlaySetting.gameMode == PlaySetting.GameMode.battle)
        {
            this.SetSelectedStage(this._battleModeStages);
        }
        else
        {
            this.SetSelectedStage(this._treasureModeStages);
        }
    }

    public void SetPlayerSettingIsMaster(bool isMaster)
    {
        PlaySetting.isMaster = isMaster;
    }

    public void ChangePlayerMaxNum(int change)
    {
        PlaySetting.maxPlayerNum += (byte)change;

        if (PlaySetting.maxPlayerNum > this._maxPlayerNumber)
            PlaySetting.maxPlayerNum = this._maxPlayerNumber;
        else if (PlaySetting.maxPlayerNum < this._minimumPlayerNumber)
            PlaySetting.maxPlayerNum = this._minimumPlayerNumber;

        PlayerPrefs.SetInt(MAX_PLAYER_NUMBER_STRAGED_NAME, (int)PlaySetting.maxPlayerNum);
        _playerNumberText.text = PlaySetting.maxPlayerNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
