using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChooseManager : MonoBehaviour
{
    [SerializeField] StageSelectManager _stageSelectManager;

    public void ChangeGameModeBattle()
    {
        PlaySetting.gameMode = PlaySetting.GameMode.battle;
        _stageSelectManager.InitializeMode();
    }

    public void ChangeGameModeTreasure()
    {
        PlaySetting.gameMode = PlaySetting.GameMode.treasure;
        _stageSelectManager.InitializeMode();
    }    
}
