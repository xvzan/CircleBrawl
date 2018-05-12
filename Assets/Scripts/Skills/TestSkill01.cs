using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TestSkill01 : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public float maxdistance;
    public float bulletspeed = 5;
    public GameObject fireball;
    public float force = 15;
    public float damage = 10;
    private float currentcooldown;
    public float cooldowntime = 3;
    public bool skillavaliable;

    // Use this for initialization
    void Start ()
    {
        currentcooldown = cooldowntime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireG") && skillavaliable)
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
        Vector2 singplace = transform.position;
        Vector2 skilldirection = actionplace - singplace;
        DoFire (singplace + 0.71f * skilldirection.normalized, skilldirection.normalized * bulletspeed);
        currentcooldown = 0;
        skillavaliable = false;
    }
    
    //[PunRPC]
    void DoFire(Vector2 fireplace,Vector2 speed2d)
    {
        GameObject bullet;
        fireball.GetComponent<BombExplode>().sender = gameObject;
        bullet = PhotonNetwork.Instantiate(fireball.name, fireplace, Quaternion.identity, 0);
        //bullet.GetComponent<BombExplode>().bombpower = force;
        bullet.GetComponent<BombExplode>().bombdamage = damage;
        bullet.GetComponent<Rigidbody2D>().velocity = speed2d;
        //bullet.GetComponent<BombExplode>().maxtime = maxdistance / bulletspeed;
    }
}
