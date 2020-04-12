using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterChoice : MonoBehaviour
{
    [SerializeField] private ControledCharacter[] _controledCharacters;
    [SerializeField] private int _choiced = 0;
    [SerializeField] private ControledCharacter _choiceCharacter;
    [SerializeField] private Vector3 _position;

    public ControledCharacter ChoiceCharacter { get => _choiceCharacter; private set => _choiceCharacter = value; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.ChangeChoiceCharacter(0);
    }
    
    public void ChangeChoiceCharacter(int i)
    {
        this._choiced += i;
        if (this._choiced < 0)
            this._choiced = this._controledCharacters.Length - 1;
        if (this._choiced >= this._controledCharacters.Length)
            this._choiced = 0;
        if(this.ChoiceCharacter)
            Destroy(this.ChoiceCharacter.gameObject);
        this.ChoiceCharacter= Instantiate(this._controledCharacters[this._choiced]);
        DontDestroyOnLoad(this.ChoiceCharacter);
        this.ChoiceCharacter.transform.position = this._position;
        SceneManager.MoveGameObjectToScene(this.ChoiceCharacter.gameObject,SceneManager.GetActiveScene());
    }

    public GameObject GetGameInstance()
    {
        return this._controledCharacters[this._choiced].gameObject;
    }
}
