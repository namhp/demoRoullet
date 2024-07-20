using Lab.Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftSetting : AnimatedPanel
{

    public void OnStorageButton()
    {
        GameGui.instance.pushPanel("StoragePanel");        
    }
    public void OnTopButton()
    {
        GameGui.instance.pushPanel("TopPanel");
    }
}
