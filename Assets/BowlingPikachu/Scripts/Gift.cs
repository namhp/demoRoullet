using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    public InputField inputField;
    public GiftID giftID;
    public int availableNumber;

    private void OnEnable()
    {
        LoadGiftData();
        this.inputField.onValueChanged.AddListener(this.OnInputValueChange);
        this.inputField.onEndEdit.AddListener(this.OnInputEndEdit);
    }

    private void OnDisable()
    {
        SetGiftData();
        this.inputField.onValueChanged.RemoveListener(this.OnInputValueChange);
        this.inputField.onEndEdit.RemoveListener(this.OnInputEndEdit);
    }

    public void LoadGiftData() {
        this.availableNumber = GiftManager.Instance.GetGiftData(this.giftID);
        if (availableNumber == 0)
        {
            inputField.text = "";
        }
        else
        {
            inputField.text = this.availableNumber.ToString();
        }
    }

    public void SetGiftData() {
        GiftManager.Instance.SetGiftData(this.giftID, this.availableNumber);
    }

    public void OnInputValueChange(string value) {
    }

    public void OnInputEndEdit(string value) {
        if (value == "")
            this.availableNumber = 0;
        this.availableNumber = int.Parse(value);
    }   
}


public enum GiftID {
    bathTowel ,
    plasticBag ,
    boxTissue ,
    sealSheet 
}
