using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public int Score { set; private get; }
    [SerializeField] protected Text _textScore;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            this.Score += 1;
            _textScore.text = this.Score.ToString();
            StartCoroutine("GoalCoroutine", other.gameObject.GetComponent<Ball>());
        }
    }

    IEnumerator GoalCoroutine(Ball ball)
    {
        var go = ball.gameObject;
        var position = ball.FirstPosition;
        Destroy(go.GetComponent<Ball>());
        yield return new WaitForSeconds(3.0f);
        var  newBall = go.AddComponent<Ball>();
        newBall.FirstPosition = position;
        go.transform.position = position;
    }
}
