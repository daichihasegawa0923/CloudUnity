using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyControlOperator : MonoBehaviour
{
    [SerializeField] private GameObject _a_grip;
    [SerializeField] private GameObject _a_release;
    [SerializeField] private GameObject _w_stepup;

    public GameObject A_grip { get => _a_grip; private set => _a_grip = value; }
    public GameObject A_release { get => _a_release; private set => _a_release = value; }
    public GameObject W_stepup { get => _w_stepup; private set => _w_stepup = value; }
}
