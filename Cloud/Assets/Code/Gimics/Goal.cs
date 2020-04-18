using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public int Score { set; private get; }
    [SerializeField] protected Text _textScore;
    [SerializeField] protected GameObject _goalText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            if (!other.gameObject.GetComponent<Ball>().enabled)
                return;
            this.Score += 1;
            _textScore.text = this.Score.ToString();
            StartCoroutine("GoalCoroutine", other.gameObject.GetComponent<Ball>());
        }
    }

    IEnumerator GoalCoroutine(Ball ball)
    {
        _goalText.SetActive(true);
        var go = ball.gameObject;
        var position = ball.FirstPosition;
        ball.enabled = false;
        yield return new WaitForSeconds(3.0f);
        _goalText.SetActive(false);
        ball.enabled = true;
        ball.ResetPosition();
    }
}
