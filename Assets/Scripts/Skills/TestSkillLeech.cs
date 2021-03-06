﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TestSkillLeech : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public float maxdistance;
    public float bulletspeed = 5;
    public GameObject leecher;
    public float damage = 10;
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
        if (Input.GetButtonDown("FireT") && skillavaliable)
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
        //DoSkill.singing = 0;
        //gameObject.GetComponent<DoSkill>().Fire = null;
        GetComponent<DoSkill>().BeforeSkill();
        GameObject bullet;
        Vector2 skilldirection;
        Vector2 singplace = transform.position;
        skilldirection = actionplace - singplace;
        float turntime = (skilldirection.magnitude - 0.71f) / bulletspeed;
        leecher.GetComponent<LeechScript>().sender = gameObject;
        leecher.GetComponent<LeechScript>().turntime = turntime;
        leecher.GetComponent<LeechScript>().speed = bulletspeed;
        leecher.GetComponent<LeechScript>().leechdamage = damage;
        bullet = PhotonNetwork.Instantiate(leecher.name, singplace + 0.71f * skilldirection.normalized, Quaternion.identity, 0);
        bullet.GetComponent<Rigidbody2D>().velocity = skilldirection.normalized * bulletspeed;
        //bullet.GetComponent<LeechScript>().maxtime = maxdistance / bulletspeed;
        currentcooldown = 0;
        skillavaliable = false;
    }
}
