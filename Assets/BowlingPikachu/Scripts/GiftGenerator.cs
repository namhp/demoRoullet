using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftGenerator : MonoBehaviourSingletonPersistent<GiftGenerator>
{    
    [SerializeField]
    private List<int> firstSetInt = new List<int>();
    [SerializeField]
    private List<int> secondSetInt = new List<int>();

    public int WinCode = 0;
    public int PlayCode = 0;
    private int playTimes = 0;
    public int PlayTimes => this.playTimes;

    public int RanValue => this.WinCode;

    public bool IsFinish => this.playTimes == 2;


    public void Refresh() {
        this.playTimes = 0;
        WinCode = 0;
    }
    private void SetupFirstSet() {
        firstSetInt.Clear();
        // if A > 0.
        if (GiftManager.Instance.GetGiftData(GiftID.bathTowel) > 0)
        {
            for (int i = 0; i < GiftManager.Instance.GetGiftData(GiftID.bathTowel); i++)
            {
                firstSetInt.Add(1);
            }
        }
        if (GiftManager.Instance.GetGiftData(GiftID.plasticBag) > 0)
        {
            for (int i = 0; i < GiftManager.Instance.GetGiftData(GiftID.plasticBag); i++)
            {
                firstSetInt.Add(2);
            }
        }
        if (GiftManager.Instance.GetGiftData(GiftID.boxTissue) > 0)
        {
            for (int i = 0; i < GiftManager.Instance.GetGiftData(GiftID.boxTissue); i++)
            {
                firstSetInt.Add(3);
            }
        }
        if (GiftManager.Instance.GetGiftData(GiftID.sealSheet) > 0)
        {
            for (int i = 0; i < GiftManager.Instance.GetGiftData(GiftID.sealSheet); i++)
            {
                firstSetInt.Add(4);
            }
        }
        Shuffle(firstSetInt);
    }
    
    private int RandomFirstNum() {
        SetupFirstSet();
        int firstRan = Random.Range(0, this.firstSetInt.Count);
        WinCode = firstSetInt[firstRan];
        return WinCode;
    }
    

    public void RandomNum() {
        if (this.PlayTimes == 0)
        {
            RandomFirstNum();
        }
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

}
