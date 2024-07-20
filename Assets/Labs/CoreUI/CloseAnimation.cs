using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAnimation : MonoBehaviour {

	protected CanvasGroup panel;
	protected RectTransform rect;
	void Awake() {
		panel = GetComponent<CanvasGroup> ();
		rect = GetComponent<RectTransform> ();
	}

	public virtual void play(System.Action startAction = null, System.Action finishAction = null) {

	}
}
