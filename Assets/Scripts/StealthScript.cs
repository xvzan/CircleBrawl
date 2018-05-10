using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class StealthScript : Photon.MonoBehaviour
{
    public GameObject MyMarkicon;
    public GameObject MyHealthbar;
    public SpriteRenderer BigSR;
    SpriteRenderer SmallSR;
    SpriteRenderer BarSR;
    Color ColorBefore;
    public Color ColorChangeTo;
    float currenttime;
    float maxtime = 1;
    bool WindWalkByTime = false;
    bool UCME = false;

    // Use this for initialization
    void Start () {
        SmallSR = MyMarkicon.GetComponent<SpriteRenderer>();
        BarSR = MyHealthbar.GetComponent<SpriteRenderer>();
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
        }
    }

    void SelfChange()
    {
        ColorBefore = BigSR.color;
        BigSR.color = ColorChangeTo;
    }

    void SelfChangeBack()
    {
        BigSR.color = ColorBefore;
    }

    public void StealthByTime(float time)
    {
        currenttime = 0;
        maxtime = time;
        WindWalkByTime = true;
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
        photonView.RPC("Appear", PhotonTargets.All);
        UCME = false;
    }
}
