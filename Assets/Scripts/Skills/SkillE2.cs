using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillE2 : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    //public float SpeedR2;
    public float maxTimeE2;
    public float pushPower;
    public float pushTime;
    public float pushDamage;
    private float currentcooldown;
    public float cooldowntime = 5;
    public bool skillavaliable;
    MoveScript MS;

    // Use this for initialization
    void Start ()
    {
        currentcooldown = cooldowntime;
        MS = GetComponent<MoveScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("FireE") && skillavaliable)
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

    public void Skill()
    {
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        gameObject.GetComponent<StealthScript>().StealthEnd();
        MS.controllable = true;
        currentcooldown = 0;
        skillavaliable = false;
        gameObject.GetComponent<ColliderScript>().SetPower(pushPower, pushTime, pushDamage);
        gameObject.GetComponent<ColliderScript>().StartKick(maxTimeE2);
        gameObject.GetComponent<StealthScript>().StealthByTime(maxTimeE2, false);
    }
}
