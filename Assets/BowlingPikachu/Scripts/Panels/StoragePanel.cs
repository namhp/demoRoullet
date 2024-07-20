using Lab.Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoragePanel : AnimatedPanel
{
    public Text txtDevice;
    public GameObject loadingGO;
    public override void panelWillShow()
    {
        txtDevice.text = "端末番号 " + InputDeviceIDPanel.DeviceID.ToString();
    }

    public void OnUploadButton() {

        UploadService.Instance.Upload((s) => {      
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

    private void ShowProcess() {
        loadingGO.SetActive(true);
    }

    private void HideProcess() {
        loadingGO.SetActive(false);
    }

    private bool PushData() {
        // TODO. push data to server.
        return Random.value > 0.5f;
    }

}
