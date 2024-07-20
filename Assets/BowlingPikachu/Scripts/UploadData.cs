using System;
using System.Collections.Generic;
using UnityEngine;

public class UploadData
{
    public static string SAVE_LOG_KEY = "save_log_key";
    public int devices;
    public List<Record> data = LoadLog();
    public int campaignId = 000118;
    public int campaingnGroupId = 000118;


    public static void SaveLog() {

        var listRecord = LoadLog();
        UserData.Instance.playingRecord.carrier = (int)UserData.Instance.networkID;
        UserData.Instance.playingRecord.lotName = UserData.Instance.receivedGift.ToString();
        //switch (UserData.Instance.receivedGift)
        //{
        //    case GiftID.bathTowel:
        //    UserData.Instance.playingRecord.lotName = "000092";
        //    break;
        //    case GiftID.boxTissue:
        //    UserData.Instance.playingRecord.lotName = "000093";
        //    break;
        //    case GiftID.plasticBag:
        //    UserData.Instance.playingRecord.lotName = "000094";
        //    break;
        //    case GiftID.sealSheet:
        //    UserData.Instance.playingRecord.lotName = "000095";
        //    break;
        //}
        listRecord.Add(UserData.Instance.playingRecord);        
        ES3.Save<List<Record>>(SAVE_LOG_KEY, listRecord);
        // TODO: Comment out it when release app.
        // For debug only.
        //UploadData uploadData = new UploadData();
        //var jsonData = JsonUtility.ToJson(uploadData);
        //uploadData.devices = InputDeviceIDPanel.DeviceID;
        //Debug.Log("upload data \n " + jsonData);
    }    

    public static List<Record> LoadLog() {
        return ES3.Load<List<Record>>(SAVE_LOG_KEY, new List<Record>());
    }

    public static void ClearLog() {
        ES3.DeleteKey(SAVE_LOG_KEY);
    }
}
