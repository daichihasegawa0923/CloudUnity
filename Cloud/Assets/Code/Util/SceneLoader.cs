using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image _fadeoutPanel;
    [SerializeField] private AudioSource _audioSource;


    public void LoadSceneByButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByButtonWithFadeoutAndSound(string sceneName)
    {
        this._audioSource.Play();
        StartCoroutine("Fadeout",sceneName);
    }

    private IEnumerator Fadeout(string sceneName)
    {
        this._fadeoutPanel.gameObject.SetActive(true);

        while (_fadeoutPanel.color.a <= 1.0f)
        {
            var color = _fadeoutPanel.color;
            color.a += 0.05f;
            _fadeoutPanel.color = color;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(sceneName);
    }
}
