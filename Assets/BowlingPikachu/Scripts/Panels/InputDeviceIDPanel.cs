using Lab.Core.UI;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class InputDeviceIDPanel : AnimatedPanel
{
    public static string LOCK_KEY = "LOCKED";
    public static string DEVICE_ID_KEY = "DEVICE_ID_KEY";
    public static string LAST_RESET_DATE_KEY = "RESET_KEY";

    public Color from = Color.white;
    public Color to = Color.red;
    public Text errorMessage;
    private Tween tween;

    public static bool HasLocked
    {
        get => ES3.Load<bool>(LOCK_KEY, false);
        set
        {
            ES3.Save<bool>(LOCK_KEY, value);
        }
    }

    public static string DeviceID
    {
        get => ES3.LoadString(DEVICE_ID_KEY, string.Empty);
        set
        {
            ES3.Save<String>(DEVICE_ID_KEY, value);
        }
    }


    public InputField inputField;
    //public Button lockButton;

    public override void panelWillShow()
    {
        //lockButton.interactable = !HasLocked;
        LoadLastDeviceID();
        inputField.onValueChanged.AddListener(this.OnInputChangeValue);
        inputField.onEndEdit.AddListener(this.OnEndEdit);
    }

    private void LoadLastDeviceID()
    {
        inputField.text = DeviceID.ToString();
    }

    private void CheckInput(System.Action<bool> callback)
    {
        bool isValidate = !string.IsNullOrEmpty(inputField.text);
        callback.Invoke(isValidate);

    }

    public override void panelWillHide()
    {
        inputField.onValueChanged.RemoveListener(this.OnInputChangeValue);
        inputField.onEndEdit.RemoveListener(this.OnEndEdit);
    }

    private void SaveDID()
    {
        DeviceID = inputField.text;
    }

    public void OnOKButton()
    {
        CheckInput((s) =>
        {
            if (s)
            {
                SaveDID();
                GameGui.instance.pushPanel("TopPanel");
            }
            else
            {
                ShowInputError();
            }
        });
    }

    public void OnLockButton()
    {
        Debug.Log("Click log button");
        //lockButton.interactable = false;
        CheckInput((s) =>
        {
            if (s)
            {
                SaveDID();
                GameGui.instance.pushPanel("TopPanel");
                HasLocked = true;
            }
            else
            {
                ShowInputError();
            }
        });
    }
    public void OnInputChangeValue(string value)
    {
        Debug.Log("OnInputChangeValue =" + value);
        if (tween != null && tween.IsPlaying())
        {
            tween.Pause();
        }
        errorMessage.color = from;
    }

    public void OnEndEdit(string value)
    {
        Debug.Log("OnEndEdit =" + value);
    }

    private void ShowInputError()
    {
        if (tween == null)
        {
            tween = errorMessage.DOColor(to, 2f).SetLoops(-1);
        }
        else
        {
            tween.Play();
        }
    }

    public static void CheckReset()
    {
        if (!ES3.KeyExists(LAST_RESET_DATE_KEY))
        {
            ResetApp();
        }
        else
        {
            if (IsResetDate && DateTime.Compare(DateTime.Today, LastResetDate) != 0)
            {
                if (DateTime.Now.Hour > 6 || (DateTime.Now.Hour == 6 && DateTime.Now.Minute > 0) || (DateTime.Now.Hour == 6 && DateTime.Now.Minute == 0 && DateTime.Now.Second > 0))
                {
                    ResetApp();
                }
            }
        }
    }

    public static void ResetApp()
    {
        DateTime today = DateTime.Today;
        DateTime n = today.DateByDayOfWeek(DayOfWeek.Tuesday);
        LastResetDate = n;
        GiftManager.Instance.ResetGift();
    }

    public static bool IsResetDate
    {
        get
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Tuesday;
        }
    }

    public static DateTime LastResetDate
    {
        get
        {
            DateTime today = DateTime.Today;
            return ES3.Load<DateTime>(LAST_RESET_DATE_KEY, today.DateByDayOfWeek(DayOfWeek.Tuesday));
        }
        set
        {
            ES3.Save<DateTime>(LAST_RESET_DATE_KEY, value);
        }
    }


}
