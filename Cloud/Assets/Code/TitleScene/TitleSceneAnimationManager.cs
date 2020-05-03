using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleSceneAnimationManager : MonoBehaviour
{
    [SerializeField] GameObject _titleImage;
    [SerializeField] GameObject _startButton;
    [SerializeField] GameObject _character;
    [SerializeField] GameObject _ohuzake;
    

    private void Awake()
    {
        var sequence = DOTween.Sequence();
        this.TitleImaeAnimation(ref sequence);
        this.ButtomAnimation(ref sequence);
        this.CharacterAnimation(ref sequence);
        this.OhuzakeAnimation(ref sequence);
    }


    /// <summary>
    /// タイトル画像のアニメーション
    /// </summary>
    private void TitleImaeAnimation(ref Sequence sequence)
    {
        var titleImageScale = _titleImage.transform.localScale;
        _titleImage.transform.localScale = Vector3.zero;
        var titleImageRotation = _titleImage.transform.eulerAngles;
        var tempRotation = _titleImage.transform.eulerAngles;
        tempRotation.z += Random.Range(-40, 40);
        _titleImage.transform.eulerAngles = tempRotation;

        sequence.Append
            (
            _titleImage.transform.DOScale(titleImageScale, 2.50f)
            ).Join
            (
            _titleImage.transform.DORotate(titleImageRotation, 2.50f)
            );
    }
    
    private void ButtomAnimation(ref Sequence sequence)
    {
        var imagePosition = this._startButton.GetComponent<RectTransform>().position;
        Debug.Log(imagePosition);
        var tempPosition = this._startButton.GetComponent<RectTransform>().position;
        tempPosition.y -= 400;
        this._startButton.GetComponent<RectTransform>().position = tempPosition;

        // 謎の微調整が必要
        sequence.Append(_startButton.GetComponent<RectTransform>().DOMoveY(imagePosition.y + 50 ,1.5f));
    }

    private void CharacterAnimation(ref Sequence sequence)
    {
        var characterPosition = this._character.transform.position;
        var tempPosition = this._character.transform.position;
        tempPosition.x -= 50.0f;
        this._character.transform.position = tempPosition;

        sequence.Join(_character.transform.DOMove(characterPosition, 1.5f));
    }

    private void OhuzakeAnimation(ref Sequence sequence)
    {
        var ohuzakePosition = this._ohuzake.transform.position;
        var tempPosition = this._ohuzake.transform.position;
        tempPosition.x += 50.0f;
        this._ohuzake.transform.position = tempPosition;

        sequence.Join(_ohuzake.transform.DOMove(ohuzakePosition, 1.5f));
    }
}
