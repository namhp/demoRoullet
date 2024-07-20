using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeInAnimation : OpenAnimation {

	[Header("Fade in animation setup")]
	public float to;
	public float duration;

	public override void play (System.Action startAction, System.Action finishAction)
	{
		//LeanTween.alphaCanvas (panel, to, duration).setOnStart(startAction).setOnComplete (finishAction);
		this.panel.DOFade(this.to, this.duration)
		//.OnStart(() => { startAction?.Invoke(); })
		.OnComplete(() => { finishAction?.Invoke(); }); 
	}
}
