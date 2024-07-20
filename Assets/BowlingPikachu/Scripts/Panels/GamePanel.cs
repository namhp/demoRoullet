using Lab.Core.UI;
using UnityEngine.UI;
using UnityEngine;
using Utilities.Components;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class GamePanel : AnimatedPanel
{
    public string hintSound;
    [SerializeField]
    public string soundName;
    
    [SerializeField] private GameObject checkPanelAB;
    [SerializeField] private GameObject checkPanelCD;

    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject spinScreen;
    [SerializeField] private GameObject specialScreen;
    [SerializeField] private GameObject secondSpinScreen;

    public List<int> prize;

    [SerializeField]
    AnimationCurve _spinCurve14 = new AnimationCurve(
    // Starting keyframe (no rotation)
     new Keyframe(0f, 0f, 1f, 1f),

    // Increase speed rapidly (adjust values for desired acceleration)
    new Keyframe(0.05f, 0.02f, 1f, 1f),
    new Keyframe(0.1f, 0.05f, 1f, 1f),
    new Keyframe(0.15f, 0.1f, 1f, 1f),
    new Keyframe(0.2f, 0.15f, 1f, 1f),
    new Keyframe(0.25f, 0.2f, 1f, 1f),
    new Keyframe(0.3f, 0.3f, 1f, 1f),
    new Keyframe(0.35f, 0.4f, 1f, 1f), // Start maintaining high speed here
    new Keyframe(0.4f, 0.5f, 1f, 1f),
    new Keyframe(0.45f, 0.6f, 1f, 1f),
    new Keyframe(0.5f, 0.67f, 1f, 1f),

    new Keyframe(0.55f, 0.72f, 1f, 1f),
    new Keyframe(0.6f, 0.77f, 1f, 1f),
    new Keyframe(0.65f, 0.82f, 1f, 1f),
    new Keyframe(0.7f, 0.86f, 1f, 1f),
    new Keyframe(0.75f, 0.9f, 1f, 1f),
    new Keyframe(0.8f, 0.94f, 1f, 1f), // Extend high speed duration
    new Keyframe(0.85f, 0.96f, 1f, 1f), // Extend high speed duration
    new Keyframe(0.9f, 0.98f, 1f, 1f),
    new Keyframe(1f, 1f, 0f, 0f)
    );

    public List<AnimationCurve> animationCurves;
    [Range(0, 4)]
    [SerializeField] private int animationSelectNo;

    [SerializeField] private Transform rotateTransfrom;
    [Range(0, 10)]
    [SerializeField] private int randomTime;

    [Range(0, 10)]
    [SerializeField] private float speed;

    private bool spinning;
    private float anglePerItem;
    private int itemNumber;

    public System.Action<int> OnStartSpinAction;
    public System.Action<int> OnEndSpinAction;


    public override void panelWillShow()
    {
        spinning = false;
        anglePerItem = 360 / prize.Count;
        rotateTransfrom.eulerAngles = new Vector3(0,0,0);
        startScreen.SetActive(true);
        spinScreen.SetActive(false);
        specialScreen.SetActive(false);
        secondSpinScreen.SetActive(false);
    }

    public override void panelDidShow()
    {
        SetUpForTurn();
    }

    public override void panelWillHide()
    {
    }

    public void SetUpForTurn()
    {
    }

    public void Play(int start)
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic(pVolume: 0.35f);
        //GiftGenerator.Instance.PlayCode = start;
        if (!spinning)
        {
            spinning = true;
            spinScreen.SetActive(true);
            startScreen.SetActive(false);           
            OnResult(start);
        }
    }

    public void OnResult(int start)
    {
        GiftGenerator.Instance.RandomNum();
        StartCoroutine(ShowResult(start));
    }

    public IEnumerator ShowResult(int start)
    {
        Debug.Log(start.ToString() + " " + GiftGenerator.Instance.WinCode.ToString());
        yield return new WaitForSeconds(1);
        
                switch (GiftGenerator.Instance.WinCode)
                {
                    case 1:
                        Spin(8);
                        yield return new WaitForSeconds(0.2f);
                        AudioManager.Instance.PlaySFX(SfxIDs.SPIN, 1);
                        yield return new WaitForSeconds(5f);
                        secondSpinScreen.SetActive(true);
                        spinScreen.SetActive(false);
                        specialScreen.SetActive(true);
                        AudioManager.Instance.StopSFX(SfxIDs.SPIN);
                        AudioManager.Instance.PlaySFX(SfxIDs.SChance, 1);
                        yield return new WaitForSeconds(2f);
                        specialScreen.SetActive(false);
                        Spin(8);
                        yield return new WaitForSeconds(0.2f);
                        AudioManager.Instance.StopSFX(SfxIDs.SChance);
                        AudioManager.Instance.PlaySFX(SfxIDs.SPIN, 1);
                        yield return new WaitForSeconds(4f);
                        AudioManager.Instance.StopSFX(SfxIDs.SPIN);
                        AudioManager.Instance.PlaySFX(SfxIDs.STOP, 1);
                        yield return new WaitForSeconds(2f);
                        checkPanelAB.SetActive(true);
                        break;
                    case 2:
                        int i2 = Random.Range(0, 2);
                        Spin(8);
                        yield return new WaitForSeconds(0.2f);
                        AudioManager.Instance.PlaySFX(SfxIDs.SPIN, 1);
                        yield return new WaitForSeconds(5f);
                        secondSpinScreen.SetActive(true);
                        spinScreen.SetActive(false);
                        specialScreen.SetActive(true);
                        AudioManager.Instance.StopSFX(SfxIDs.SPIN);
                        AudioManager.Instance.PlaySFX(SfxIDs.SChance, 1);
                        yield return new WaitForSeconds(2f);
                        specialScreen.SetActive(false);
                        yield return new WaitForSeconds(1f);
                        if (i2 == 0) {
                            Spin(1);
                            Debug.Log("ball 1");

                        }
                        else
                                {
                                    Spin(5);
                            Debug.Log("ball 2");
                        }
                        yield return new WaitForSeconds(0.2f);
                        AudioManager.Instance.StopSFX(SfxIDs.SChance);
                        AudioManager.Instance.PlaySFX(SfxIDs.SPIN, 1);
                        yield return new WaitForSeconds(4f);
                        AudioManager.Instance.StopSFX(SfxIDs.SPIN);
                        AudioManager.Instance.PlaySFX(SfxIDs.STOP, 1);
                        yield return new WaitForSeconds(2f);
                        checkPanelAB.SetActive(true);
                        break;
                    case 3:
                        int i3 = Random.Range(0, 2);
                        if (i3 == 0)
                        {
                            Spin(1);
                            Debug.Log("ball 1");

                        }
                        else
                        {
                            Spin(5);
                            Debug.Log("ball 2");
                        }
                        yield return new WaitForSeconds(0.2f);
                        AudioManager.Instance.PlaySFX(SfxIDs.SPIN, 1);
                        yield return new WaitForSeconds(4f);
                        AudioManager.Instance.StopSFX(SfxIDs.SPIN);
                        AudioManager.Instance.PlaySFX(SfxIDs.STOP, 1);
                        yield return new WaitForSeconds(2f);
                        checkPanelCD.SetActive(true);
                        break;
                    case 4:
                        int i4 = Random.Range(0, 7);
                        if (i4 == 0)
                        {
                            Spin(0);
                        }
                        else if (i4 == 1)
                        {
                            Spin(2);
                        }
                        else if (i4 == 2)
                        {
                            Spin(3);
                        }
                        else if (i4 == 3)
                        {
                            Spin(4);
                        }                     
                        else if (i4 == 4)
                        {
                            Spin(6);
                        }
                        else if (i4 == 5)
                        {
                            Spin(7);
                        }
                        else if (i4 == 6)
                        {
                            Spin(9);
                        }
                        yield return new WaitForSeconds(0.2f);
                        AudioManager.Instance.PlaySFX(SfxIDs.SPIN, 1);
                        yield return new WaitForSeconds(4f);
                        AudioManager.Instance.StopSFX(SfxIDs.SPIN);
                        AudioManager.Instance.PlaySFX(SfxIDs.STOP, 1);
                        yield return new WaitForSeconds(2f);
                        checkPanelCD.SetActive(true);
                        break;
                }
        AudioManager.Instance.PlayMusic(pVolume: 0.5f);
        //AudioManager.Instance.StopSFX(SfxIDs.SPIN);
        //AudioManager.Instance.PlaySFX(SfxIDs.DRUMROLL, 1);
        yield return new WaitForSeconds(4f);
    }

    private void MakeHintSound()
    {
        //if (_hintTimes > 0)
        //{
        //    AudioManager.Instance.PlaySFX(hintSound, 2);
        //    _hintTimes--;
        //}
        //else

        //{
        //    CancelInvoke("MakeHintSound");
        //}
    }

    public void Hint()
    {
        InvokeRepeating("MakeHintSound", 0, 0.8f);
    }

    public void OnCheck()
    {
        AudioManager.Instance.StopSFX(SfxIDs.DRUMROLL);
        checkPanelAB.SetActive(false);
        checkPanelCD.SetActive(false);
        GameGui.instance.pushPanel("GiftSummary");
    }

    void Spin(int order)
    {
        if (OnStartSpinAction != null)
            OnStartSpinAction.Invoke(order);
        itemNumber = order;
        var _itemNumber = itemNumber;
        if (order == 0) _itemNumber = 0;
        else _itemNumber = prize.Count - order;
        Debug.Log("itemNumber No. : " + itemNumber);

        float maxAngle = 360 * randomTime + (_itemNumber * anglePerItem) ; // - angleAdjust
        StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));
    }

    IEnumerator SpinTheWheel(float time, float maxAngle)
    {

        spinning = true;

        float timer = 0.0f;
        float startAngle = -rotateTransfrom.eulerAngles.z;
        maxAngle = startAngle - maxAngle + 18 ;

        int animationCurveNumber = animationSelectNo;

        while (timer < time)
        {
            float angle = maxAngle * animationCurves[0].Evaluate(timer / time);
            rotateTransfrom.eulerAngles = new Vector3(0.0f, 0.0f, angle- startAngle);
            timer += speed * Time.deltaTime;
            yield return 0;
        }

        rotateTransfrom.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle- startAngle);
        spinning = false;


        if (OnEndSpinAction != null)
            OnEndSpinAction.Invoke(itemNumber);
        Debug.Log("Prize: " + prize[itemNumber]);


    }
}
