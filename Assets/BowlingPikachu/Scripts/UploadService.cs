using BestHTTP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class UploadService : MonoBehaviourSingletonPersistent<UploadService>
{
    public void Upload(UnityAction<bool> callback, UnityAction startCallback = null, UnityAction finishCallback = null) {
        UploadData uploadData = new UploadData();
        uploadData.devices = int.Parse(InputDeviceIDPanel.DeviceID);
        var jsonData = JsonUtility.ToJson(uploadData);
        Debug.Log("Upload data \n " + jsonData.ToString());
        StartCoroutine(Upload(jsonData,callback, startCallback, finishCallback));


        //var request = new HTTPRequest(new Uri(""), HTTPMethods.Post, OnRequestFinished);
        //request.AddHeader("api-key", "");
        //request.AddHeader("Content-Type", "application/json");
        //request.FormUsage = BestHTTP.Forms.HTTPFormUsage.Multipart;
        //request.RawData = Encoding.ASCII.GetBytes(jsonData);

        //request.Send();

    }

    void OnRequestFinished(HTTPRequest req, HTTPResponse resp)
    {
        Debug.Log(resp.StatusCode);

        Debug.Log(resp.Data);
    }


    IEnumerator Upload(string data, UnityAction<bool> callback, UnityAction startCallback = null, UnityAction finishCallback = null)
    {
        startCallback?.Invoke();
        //UnityWebRequest www = UnityWebRequest.Post("", "POST"); //Production API
        UnityWebRequest www = UnityWebRequest.PostWwwForm("", "POST"); //Staging API
        byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("api-key", "");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Upload data error \n " + www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            UploadData.ClearLog();
        }
        yield return  new WaitForSeconds(1);

        finishCallback?.Invoke();
        callback.Invoke(www.result == UnityWebRequest.Result.Success);
    }

}
