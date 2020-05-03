using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySetting
{
    /// <summary>
    /// ゲームモード
    /// </summary>
    public enum GameMode {treasure,battle};
    public static GameMode gameMode = GameMode.treasure;
    public static bool isMaster = true;
    public static byte maxPlayerNum = 1;

    /// <summary>
    /// Resourcesフォルダから呼び出す操作キャラクターの名前
    /// </summary>
    public static string playCharacterResoucesName;

    public static string playSceneName;
}
