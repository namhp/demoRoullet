using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    public float speed = 5f;
    public void Move(Action onFinishMove = null) {

        var targetPos = new Vector3(UnityEngine.Random.Range(-100, 100), 100f, 0f);
        
        var _rotateTween = transform.DOLocalRotate(new Vector3(0, 0, 960f), 4f);
        var _scaleTween = transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 4f);

        transform.DOLocalMove(targetPos, this.speed).SetEase(Ease.Linear).SetSpeedBased(true).OnComplete(()=> {
            _rotateTween.Complete();
            _scaleTween.Complete();
            onFinishMove?.Invoke();
        });
    }

    public void Setup() {
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
