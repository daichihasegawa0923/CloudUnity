using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowLoading : MonoBehaviour
{
    [SerializeField] Text _commentText;
    [SerializeField] List<string> _comments;
    
    // Start is called before the first frame update
    void Start()
    {
        _commentText.text = _comments[(int)Random.Range(0,_comments.Count - 1)];
    }
}
