using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossInAnimation : OpenAnimation {
	[Header("Cross in animation setup")]
	public float to;
	public float duration;
	private RectTransform rectTransform;

    private void Start()
    {
		rectTransform = rectTransform ?? GetComponent<RectTransform>();
    }

    public override void play (System.Action startAction, System.Action finishAction)
	{
		//LeanTween.moveY(rect, to, duration).setOnStart(startAction).setOnComplete (finishAction).setEase(LeanTweenType.easeOutBack);
		panel.transform.DOLocalMoveY(to, duration).
		OnStart(() => {
			panel.alpha = 1;
			startAction.Invoke();
		}).OnComplete(()=> { 
			finishAction.Invoke();
			rectTransform.anchoredPosition = Vector2.zero;
		});
	}
}
