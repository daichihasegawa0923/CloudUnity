using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChooseManager : MonoBehaviour
{
    public void ChangeGameModeBattle()
    {
        PlaySetting.gameMode = PlaySetting.GameMode.battle;
    }

    public void ChangeGameModeTreasure()
    {
        PlaySetting.gameMode = PlaySetting.GameMode.treasure;
    }    
}
