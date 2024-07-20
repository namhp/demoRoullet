using Lab.Core.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Components;

public class GiftSummary : AnimatedPanel
{
    public string soundName;
    public List<GameObject> giftList;

    private GameObject winGO;
    public override void panelWillShow()
    {
        AudioManager.Instance.PlaySFX(soundName, 1, false, 1,0.5f);
        var giftID = GiftManager.Instance.MatchingGift((GiftGenerator.Instance.WinCode));
        UserData.Instance.receivedGift = giftID.Value;
        winGO = giftList.Find(x => giftID.ToString() == x.name);
        var giftNum = GiftManager.Instance.GetGiftData((GiftID)giftID);
        GiftManager.Instance.SetGiftData((GiftID)giftID, giftNum-1);
        winGO.SetActive(true);
        if (GiftGenerator.Instance.WinCode == 1 || GiftGenerator.Instance.WinCode == 2)
        {
            AudioManager.Instance.PlaySFX(SfxIDs.AB, 1, false, 1);
        }
        else
        {
            AudioManager.Instance.PlaySFX(SfxIDs.CD, 1, false, 1);
        }
    }

    public override void panelDidHide()
    {
        AudioManager.Instance.StopSFX(SfxIDs.APPLAUSE);//.PlaySFX(soundName, 2);
        winGO.SetActive(false);
    }

    public void Close() {
        UploadData.SaveLog();
        GiftGenerator.Instance.Refresh();
        GameGui.instance.pushPanel("TopPanel");
    }
}
