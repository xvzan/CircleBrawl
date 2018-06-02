using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillT1b : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    //public float maxdistance;
    public float bulletspeed = 20;
    public GameObject fireball;
    public float damage = 5;
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
        GetComponent<DoSkill>().BeforeSkill();
        Vector2 singplace = transform.position;
        Vector2 skilldirection = actionplace - singplace;
        DoFire(singplace + 0.81f * skilldirection.normalized, skilldirection.normalized * bulletspeed);
        currentcooldown = 0;
        skillavaliable = false;
    }

    //[PunRPC]
    void DoFire(Vector2 fireplace, Vector2 speed2d)
    {
        GameObject bullet;
        fireball.GetComponent<LeebScript>().sender = gameObject;
        fireball.GetComponent<LeebScript>().speed = bulletspeed;
        bullet = PhotonNetwork.Instantiate(fireball.name, fireplace, Quaternion.identity, 0);
        bullet.GetComponent<LeebScript>().Damage = damage;
        bullet.GetComponent<Rigidbody2D>().velocity = speed2d;
    }
}
