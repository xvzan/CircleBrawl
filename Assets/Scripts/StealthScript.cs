using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class StealthScript : Photon.MonoBehaviour
{
    public GameObject MyMarkicon;
    public GameObject MyHealthbar;
    public GameObject MyColorMark;
    public SpriteRenderer BigSR;
    SpriteRenderer SmallSR;
    SpriteRenderer BarSR;
    SpriteRenderer ColorSR;
    ShowMyName MyName;
    Color ColorBefore;
    public Color ColorChangeTo;
    float currenttime;
    float maxtime = 1;
    public static float Speed = 0;
    bool WindWalkByTime = false;
    bool UCME = false;

    // Use this for initialization
    void Start () {
        SmallSR = MyMarkicon.GetComponent<SpriteRenderer>();
        BarSR = MyHealthbar.GetComponent<SpriteRenderer>();
        ColorSR = MyColorMark.GetComponent<SpriteRenderer>();
        MyName = GetComponent<ShowMyName>();
    }

    void FixedUpdate()
    {
        if (!WindWalkByTime)
            return;
        currenttime += Time.fixedDeltaTime;
        if (currenttime >= maxtime)
            StealthEnd();
    }

    [PunRPC]
    void Vanish()
    {
        if (photonView.isMine)
            SelfChange();
        else
        {
            BigSR.enabled = false;
            SmallSR.enabled = false;
            BarSR.enabled = false;
            ColorSR.enabled = false;
            MyName.enabled = false;
        }
    }

    [PunRPC]
    void Appear()
    {
        if (photonView.isMine)
            SelfChangeBack();
        else
        {
            BigSR.enabled = true;
            SmallSR.enabled = true;
            BarSR.enabled = true;
            ColorSR.enabled = true;
            MyName.enabled = true;
        }
    }

    void SelfChange()
    {
        ColorBefore = BigSR.color;
        BigSR.color = ColorChangeTo;
        ColorSR.enabled = false;
    }

    void SelfChangeBack()
    {
        BigSR.color = ColorBefore;
        ColorSR.enabled = true;
    }

    public void StealthByTime(float time, bool DoLSDS)
    {
        currenttime = 0;
        maxtime = time;
        WindWalkByTime = true;
        if (DoLSDS)
            GetComponent<ColliderScript>().LSDSatAll();
        StealthStart();
    }

    public void StealthStart()
    {
        UCME = true;
        photonView.RPC("Vanish", PhotonTargets.All);
    }

    public void StealthEnd()
    {
        if (UCME == false)
            return;
        WindWalkByTime = false;
        GetComponent<ColliderScript>().DSWLatAll();
        photonView.RPC("Appear", PhotonTargets.All);
        Speed = 0;
        UCME = false;
    }
}
