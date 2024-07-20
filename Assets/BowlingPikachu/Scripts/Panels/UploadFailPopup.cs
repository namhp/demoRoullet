using Lab.Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UploadFailPopup : AnimatedPanel
{
    public GameObject loadingGO;
    public void OnReupButton()
    {

        UploadService.Instance.Upload((s) =>
        {
            if (s)
            {
                GameGui.instance.pushPanel("UploadSuccessPopup");
            }
            else
            {
                GameGui.instance.pushPanel("UploadFailPopup");
            }
        }, this.ShowProcess, this.HideProcess);

    }

    private void ShowProcess()
    {
        loadingGO.SetActive(true);
    }

    private void HideProcess()
    {
        loadingGO.SetActive(false);
    }

    public void OnTopButton() {
        GameGui.instance.pushPanel("TopPanel");
    }
}
