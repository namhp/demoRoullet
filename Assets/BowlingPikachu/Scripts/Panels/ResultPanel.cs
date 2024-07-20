using Lab.Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Components;

public class ResultPanel : AnimatedPanel
{
    public List<GameObject> winList;

    public string soundName;
    public float waitTime = 2;

    public override void panelWillShow()
    {
        AudioManager.Instance.PlaySFX(soundName, 1);
        StartCoroutine(Wait());
    }
    
    public void Close()
    {
        
            GameGui.instance.pushPanel("PrepareGiftPanel");
       
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        Close();
    }
}
