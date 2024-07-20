using Lab.Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadSuccessPopup : AnimatedPanel
{
    public void OnTopButton() {
        GameGui.instance.pushPanel("TopPanel");
    }
}
