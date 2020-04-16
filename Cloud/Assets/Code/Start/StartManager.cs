using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject _cmaeraBody;
    [SerializeField] private AudioSource _audioSouce;
    [SerializeField] private Animator[] _animator;
    // Update is called once per frame
    void Update()
    {
        _animator.All(ani => { ani.SetInteger("time", (int)_audioSouce.time); return true; });
    }

    public void GoToControlPanel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
