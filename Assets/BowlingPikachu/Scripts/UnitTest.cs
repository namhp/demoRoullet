using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Lab.Core.UI;
using UnityEngine.Android;

public class UnitTest : AnimatedPanel
{
    [SerializeField]
    private List<int> firstSetInt = new List<int>();
    private List<RecordTest> result = new List<RecordTest>();
    public int WinCode = 0;

    public void Test()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        result.Clear();
        int total = GiftManager.Instance.GetGiftData(GiftID.bathTowel) + GiftManager.Instance.GetGiftData(GiftID.plasticBag) + GiftManager.Instance.GetGiftData(GiftID.boxTissue) + GiftManager.Instance.GetGiftData(GiftID.sealSheet);
        print("total: " + total);
        for (int i = 0; i < total; i++)
        {
            RandomNum(i);
            if (i == total-1)
            {
                Export(result, Application.persistentDataPath+"result.txt");        
            }
        }
       
    }

    public static void Export(List<RecordTest> numbers, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (RecordTest number in numbers)
            {
                writer.WriteLine("Time " + number.time.ToString() +": " + number.lotName + " (A-" + number.a + " B-" + number.b + " C-" + number.c + " D-" + number.d +")");
            }
        }
    }

    private void SetupFirstSet()
    {
        firstSetInt.Clear();
        if (GiftManager.Instance.GetGiftData(GiftID.bathTowel) > 0)
        {
            for (int i = 0; i < GiftManager.Instance.GetGiftData(GiftID.bathTowel); i++)
            {
                firstSetInt.Add(0);
            }
        }
        if (GiftManager.Instance.GetGiftData(GiftID.plasticBag) > 0)
        {
            for (int i = 0; i < GiftManager.Instance.GetGiftData(GiftID.plasticBag); i++)
            {
                firstSetInt.Add(1);
            }
        }
        if (GiftManager.Instance.GetGiftData(GiftID.boxTissue) > 0)
        {
            for (int i = 0; i < GiftManager.Instance.GetGiftData(GiftID.boxTissue); i++)
            {
                firstSetInt.Add(2);
            }
        }
        if (GiftManager.Instance.GetGiftData(GiftID.sealSheet) > 0)
        {
            for (int i = 0; i < GiftManager.Instance.GetGiftData(GiftID.sealSheet); i++)
            {
                firstSetInt.Add(3);
            }
        }
        Shuffle(firstSetInt);
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
    private void RandomNum(int i)
    {
        SetupFirstSet();
        int firstRan = Random.Range(0, this.firstSetInt.Count);
        WinCode = firstSetInt[firstRan];
        RecordTest r = new RecordTest();
        switch (WinCode)
        {
            case 0:
                r.lotName = "A";
                break;
            case 1:
                r.lotName = "B";
                break;
            case 2:
                r.lotName = "C";
                break;
            case 3:
                r.lotName = "D";
                break;
        }
        r.time = (i+1).ToString();     
        var giftNum = GiftManager.Instance.GetGiftData((GiftID)WinCode);
        GiftManager.Instance.SetGiftData((GiftID)WinCode, giftNum - 1);
        r.a = GiftManager.Instance.GetGiftData(GiftID.bathTowel);
        r.b = GiftManager.Instance.GetGiftData(GiftID.plasticBag);
        r.c = GiftManager.Instance.GetGiftData(GiftID.boxTissue);
        r.d = GiftManager.Instance.GetGiftData(GiftID.sealSheet);
        print("Time " + r.time.ToString() + ": " + r.lotName + " (A-" + r.a + " B-" + r.b + " C-" + r.c + " D-" + r.d + ")");
        result.Add(r);
    }

    public class RecordTest
    {
        public string time;
        public string lotName;
        public int a;
        public int b;
        public int c;
        public int d;
    }
}
