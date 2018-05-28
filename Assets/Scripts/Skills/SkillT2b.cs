using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillT2b : Photon.MonoBehaviour
{

    public CooldownImage MyImageScript;
    public float maxdistance;
    public float bulletspeed;
    public GameObject fireball;
    public int bulletamount = 4;
    public float force;
    public float damage;
    private float currentcooldown;
    public float cooldowntime = 3;
    public bool skillavaliable;

    // Use this for initialization
    void Start()
    {
        currentcooldown = cooldowntime;
        fireball.GetComponent<BombExplode>().sender = gameObject;
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
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        gameObject.GetComponent<StealthScript>().StealthEnd();
        currentcooldown = 0;
        skillavaliable = false;
        Vector2 skilldirection = actionplace - gameObject.GetComponent<Rigidbody2D>().position;
        FFF(skilldirection);
    }

    void FFF(Vector2 direction)
    {
        direction = Quaternion.AngleAxis(30, Vector3.forward) * direction;
        int bnum = 0;
        while (bnum < bulletamount)
        {
            DoFire(gameObject.GetComponent<Rigidbody2D>().position + 0.5f * direction.normalized, direction.normalized * bulletspeed);
            direction = Quaternion.AngleAxis(-20, Vector3.forward) * direction;
            bnum++;
        }
    }

    void DoFire(Vector2 fireplace, Vector2 speed2d)
    {
        GameObject bullet;
        //fireball.GetComponent<BombExplode>().sender = gameObject;
        bullet = PhotonNetwork.Instantiate(fireball.name, fireplace, Quaternion.identity, 0);
        //bullet.GetComponent<BombExplode>().bombpower = force;
        bullet.GetComponent<BombExplode>().bombdamage = damage;
        bullet.GetComponent<Rigidbody2D>().velocity = speed2d;
        //bullet.GetComponent<BombExplode>().maxtime = maxdistance / bulletspeed;
    }
}
