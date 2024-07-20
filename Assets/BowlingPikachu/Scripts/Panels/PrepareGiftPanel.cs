using Lab.Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Components;

public class PrepareGiftPanel : AnimatedPanel
{
    public string soundName;
    public float waitTime = 9;
    public override void panelWillShow()
    {
        AudioManager.Instance.PlaySFX(soundName, 2);
        StartCoroutine(Wait(this.waitTime));
    }

    public override void panelDidHide()
    {
        AudioManager.Instance.StopSFX(SfxIDs.DRUMROLL);
    }

    private IEnumerator Wait(float time) {
        yield return new WaitForSeconds(time);
        GameGui.instance.pushPanel("GiftSummary");        
    }
}
