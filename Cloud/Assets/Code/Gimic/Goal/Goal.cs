using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Goal : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _goalCanvas;
    [SerializeField] private float _distance = 1.0f;
    [SerializeField] private GameManager _gameManager;

    private void Update()
    {
        var user = GameObject.FindObjectsOfType<ControledCharacter>();
        user.ToList().ForEach(u => 
        {
            if (Vector3.Distance(u.gameObject.transform.position, transform.position) < _distance)
                this.GoalCharacter(u);
        });
    }

    private void GoalCharacter(ControledCharacter controledCharacter)
    {
        Destroy(controledCharacter);
        _animator.SetTrigger(_animator.parameters[0].name);
        _goalCanvas.SetActive(true);
        StartCoroutine("LeaveRoom");
    }

    private IEnumerator LeaveRoom()
    {
        yield return new WaitForSeconds(5.0f);
        _gameManager.LeaveRoom();
    }
}
