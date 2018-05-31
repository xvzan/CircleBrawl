using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillD4 : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public float maxdistance = 10;
    public float maxtime = 2.5f;
    public float bulletspeed = 5;
    public GameObject fireball;
    public float force = 15;
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
        if (Input.GetButtonDown("FireD") && skillavaliable)
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
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        gameObject.GetComponent<StealthScript>().StealthEnd();
        currentcooldown = 0;
        skillavaliable = false;
        bulletspeed = realdistance * 1.13f / (maxtime - 0.5f);
        Vector2 fp1 = Quaternion.AngleAxis(45, Vector3.forward) * skilldirection.normalized;
        DoFire(singplace, fp1 * bulletspeed, true);
        fp1 = Quaternion.AngleAxis(-45, Vector3.forward) * skilldirection.normalized;
        DoFire(singplace, fp1 * bulletspeed, false);
    }

    //[PunRPC]
    void DoFire(Vector2 fireplace, Vector2 speed2d, bool a)
    {
        
        fireball.GetComponent<BananaScript>().sender = gameObject;
        fireball.GetComponent<BananaScript>().bombdamage = damage;
        fireball.GetComponent<BananaScript>().maxtime = maxtime;
        if (a)
            fireball.GetComponent<BananaScript>().turnangle = 90;
        else
            fireball.GetComponent<BananaScript>().turnangle = -90;
        GameObject bullet = PhotonNetwork.Instantiate(fireball.name, fireplace, Quaternion.identity, 0);
        //bullet.GetComponent<BombExplode>().bombpower = force;
        bullet.GetComponent<Rigidbody2D>().velocity = speed2d;
        //bullet.GetComponent<BombExplode>().maxtime = maxdistance / bulletspeed;
    }
}
