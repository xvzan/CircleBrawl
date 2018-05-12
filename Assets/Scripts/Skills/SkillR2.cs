using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillR2 : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public float SpeedR2;
    public float maxdistance;
    public float pushPower;
    public float pushTime;
    public float pushDamage;
    private float currentcooldown;
    public float cooldowntime = 3;
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
        if (Input.GetButtonDown("FireR") && skillavaliable)
        {
            DoSkill.singing = 0;
            gameObject.GetComponent<DoSkill>().Fire = Skill;
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

    public void Skill(Vector2 actionplace)
    {
        Vector2 singplace = transform.position;
        Vector2 skilldirection = actionplace - singplace;
        float realdistance = Mathf.Min(skilldirection.magnitude, maxdistance);
        if (realdistance <= 0.6)
        {
            return;
        }   //半径小于自身半径时不施法
        else
        {
            gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
            gameObject.GetComponent<StealthScript>().StealthEnd();
            MS.controllable = true;
            currentcooldown = 0;
            skillavaliable = false;
            float TimeR2 = realdistance / SpeedR2;
            gameObject.GetComponent<ColliderScript>().SetPower(pushPower, pushTime, pushDamage);
            gameObject.GetComponent<ColliderScript>().StartKick(TimeR2);
            gameObject.GetComponent<RBScript>().GetPushed(skilldirection.normalized * SpeedR2, TimeR2);
        }
    }
}
