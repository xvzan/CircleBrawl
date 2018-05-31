using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillC1 : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public GameObject MyBoostObj;
    private float currentcooldown;
    public float cooldowntime = 10;
    public bool skillavaliable;
    public float maxtime = 5;

    // Use this for initialization
    void Start()
    {
        currentcooldown = cooldowntime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireC") && skillavaliable)
        {
            DoSkill.singing = 0;
            Skill();
        }
    }

    private void FixedUpdate()
    {
        if (skillavaliable)
            return;
        if (currentcooldown >= cooldowntime)
        {
            skillavaliable = true;
        }
        else
        {
            currentcooldown += Time.fixedDeltaTime;
            MyImageScript.IconFillAmount = currentcooldown / cooldowntime;
        }
    }

    void Skill()
    {
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        gameObject.GetComponent<StealthScript>().StealthEnd();
        currentcooldown = 0;
        skillavaliable = false;
        MyBoostObj.GetComponent<BoostScript>().sender = gameObject;
        GameObject MyBoost = PhotonNetwork.Instantiate(MyBoostObj.name, gameObject.transform.position, Quaternion.identity, 0);
        //MyBoost.layer = 2;
        GetComponent<HPScript>().booststart();
        MyBoost.GetComponent<BoostScript>().SetConf(gameObject.GetPhotonView().viewID, maxtime);
        //gameObject.GetComponent<DoSkill>().DoClearJob();
    }
}
