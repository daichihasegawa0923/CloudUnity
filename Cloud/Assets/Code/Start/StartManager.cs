using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject _cmaeraBody;

    // Update is called once per frame
    void Update()
    {
        var spin = _cmaeraBody.transform.eulerAngles;
        spin.y += 1.0f;
        _cmaeraBody.transform.eulerAngles = spin;
    }

    public void GoToControlPanel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
