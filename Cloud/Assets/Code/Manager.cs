using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static int _characterName;

    public static int CharacterName { get => _characterName; set => _characterName = value; }
}
