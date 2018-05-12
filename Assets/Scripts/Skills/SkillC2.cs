using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillC2 : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public GameObject MyShieldObj;
    public GameObject MyShield;
    private float currentcooldown;
    public float cooldowntime = 5;
    public bool skillavaliable;
    public float maxtime = 2;

    // Use this for initialization
    void Start ()
    {
        currentcooldown = cooldowntime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("FireC"))
        {
            if (skillavaliable)
            {
                DoSkill.singing = 0;
                Skill();
            }
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
        MyShield = PhotonNetwork.Instantiate(MyShieldObj.name, gameObject.transform.position, Quaternion.identity, 0);
        MyShield.GetComponent<ShieldScript>().SetConf(gameObject.GetPhotonView().viewID, maxtime);
        gameObject.GetComponent<DoSkill>().DoClearJob();
    }
}
