using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrossOutAnimation : CloseAnimation {
	[Header("Cross out animation setup")]
	public float to;
	public float duration;
	private RectTransform rectTransform;

	private void Start()
	{
		rectTransform = rectTransform ?? GetComponent<RectTransform>();
	}


	public override void play(System.Action startAction, System.Action finishAction)
	{
		//LeanTween.moveY (rect, to, duration).setOnStart (startAction).setOnComplete (finishAction).setEase(LeanTweenType.easeInBack);
		panel.transform.DOLocalMoveY(to, duration).OnStart(()=> {
			startAction.Invoke();
		}).OnComplete(() => {
			finishAction.Invoke();
		});
	}
}
