using Lab.Core.UI;
using Utilities.Components;

public class MobileNetworkPanel : AnimatedPanel
{
    public override void panelDidShow()
    {
        //StartCoroutine(WaitForPlayAudio(1));
        //AudioManager.Instance.PlayMusic();
    }
    public void OnSelectNetwork(int id) {
        UserData.Instance.networkID = (NetworkId)id;
        GameGui.instance.pushPanel("GamePanel");
    }
}
