using Lab.Core.UI;
using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Components;

public class TopPanel : AnimatedPanel
{

    [SerializeField]
    private GameObject startButtonGO;
    public Sprite[] musicSpriteState;
    public Image soundButtonImg;
    bool isPaused;

    public override void panelWillShow()
    {
        InputDeviceIDPanel.CheckReset();
        //startButtonGO.SetActive(GiftManager.Instance.HasGift);
    }

    public override void panelDidShow()
    {
        AudioManager.Instance.PlayMusic(pVolume: 0.5f);
        ChangeSoundButtonState();
    }


    private void ChangeSoundButtonState() {
        soundButtonImg.sprite = AudioManager.Instance.EnabledMusic ? musicSpriteState[0]: musicSpriteState[1];
    }


    public override void panelDidHide()
    {
        //AudioManager.Instance.StopMusic();
    }

    public void OnSoundButton() {
        //TODO: turn on/off music.
        AudioManager.Instance.EnableMusic(!AudioManager.Instance.EnabledMusic);
        AudioManager.Instance.EnableSFX(!AudioManager.Instance.EnabledSFX);       
		ChangeSoundButtonState();
    }

    public void OnSettingButton() {
        GameGui.instance.pushPanel("GiftSettingPanel");        
    }

    public void OnStartButton() {
		if(GiftManager.Instance.HasGift){
			GameGui.instance.pushPanel("MobineNetworkPanel");
            UserData.Instance.playingRecord.lotDate = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        } else {
			GameGui.instance.pushPanel("GiftSettingPanel"); 
		}
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            InputDeviceIDPanel.CheckReset();
        }
        isPaused = pauseStatus;
    }
}
