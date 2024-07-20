using UnityEngine;

//[CreateAssetMenu(fileName = "UserData",menuName = "UserData")]
public class UserData : MonoBehaviourSingleton<UserData>
{
    public static string SOUND_KEY = "sound";
    public static string MUSIC_KEY = "music";
    public NetworkId networkID;
    public GiftID receivedGift;

    public Record playingRecord = new Record();

    public static bool Sound {
        get
        {
            return ES3.Load<bool>(SOUND_KEY, true);
        }
        set
        {
            ES3.Save<bool>(SOUND_KEY, value);
        }
    }
    public static bool Music {
        get
        {
            return ES3.Load<bool>(MUSIC_KEY, true);
        }
        set
        {
            ES3.Save<bool>(MUSIC_KEY, value);
        }
    }
}
