using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftManager : MonoBehaviourSingleton<GiftManager>
{

    private Dictionary<int, GiftID> giftReferenceBoard = new Dictionary<int, GiftID>()
    {
        {1, GiftID.bathTowel },

        {2, GiftID.plasticBag },

        {3, GiftID.boxTissue },

        {4, GiftID.sealSheet },
    };

    public void ResetGift() {
        for (int i = 0; i < 4; i++)
        {
            var giftID = (GiftID)i;
            int defaultValue = 0;
            switch (giftID)
            {
                case GiftID.bathTowel:
                    defaultValue = 5;
                    break;
                case GiftID.plasticBag:
                    defaultValue = 10;
                    break;
                case GiftID.boxTissue:
                    defaultValue = 50;
                    break;
                case GiftID.sealSheet:
                    defaultValue = 150;
                    break;
            }
            SetGiftData(giftID, defaultValue);
        }
    }

    public int GetGiftData(GiftID giftID) {
       return ES3.Load<int>(giftID.ToString(), 0);
    }    

    public void SetGiftData(GiftID giftID, int num) {
        ES3.Save<int>(giftID.ToString(), num);
    }

    public bool HasGift
    {
        get
        {
            for (int i = 0; i < 4; i++)
            {
                int giftNum = GetGiftData((GiftID)i);
                if (giftNum > 0)
                    return true;
            }
            return false;
        }
    }

    public GiftID? MatchingGift(int giftCode) {
        Debug.Log("gift cod = " + giftCode.ToString());
        var isContainInGiftBoard = this.giftReferenceBoard.ContainsKey(giftCode);

        if (isContainInGiftBoard)
        {
            return this.giftReferenceBoard[giftCode];
        }
        else
        {
            Debug.LogError("Result CODE doesn't matching any REFERENCE CODE!!!");
            return null;
        }
    }
}
