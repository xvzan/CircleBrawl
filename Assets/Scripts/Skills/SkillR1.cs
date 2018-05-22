using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillR1 : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public float maxdistance;
    private float currentcooldown;
    public float cooldowntime = 3;
    public bool skillavaliable;

    // Use this for initialization
    void Start()
    {
        currentcooldown = cooldowntime;
    }

    // Update is called once per frame
    void Update()
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
        if (realdistance <= 0.51)
        {
            return;
        }   //半径小于自身半径时不施法
        else
        {
            Vector2 realplace = singplace + skilldirection.normalized * realdistance;
            gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
            gameObject.GetComponent<StealthScript>().StealthEnd();
            gameObject.GetComponent<MoveScript>().controllable = true;
            currentcooldown = 0;
            skillavaliable = false;
            transform.position = realplace;
        }
    }
}
