using Lab.Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Components;

public class TutorialPanel : AnimatedPanel
{
    public float showTime = 4f;
    public string soundName;

    public override void panelDidShow()
    {
        //StartCoroutine(WaitForPlayAudio(1));
        //AudioManager.Instance.PlayMusic();
    }

    public override void panelDidHide()
    {
        //AudioManager.Instance.StopMusic();
    }

    public void Close()
    {
        GameGui.instance.pushPanel("GamePanel");
    }

    private IEnumerator WaitForPlayAudio(float s)
    {
        yield return new WaitForSeconds(s);
        AudioManager.Instance.PlaySFX(soundName, 2);
    }

}
