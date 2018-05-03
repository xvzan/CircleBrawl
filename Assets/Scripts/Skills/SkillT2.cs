using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillT2 : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public float maxdistance;
    public float bulletspeed = 5;
    public GameObject fireball;
    public int bulletamount;
    public float force;
    public float damage;
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
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        currentcooldown = 0;
        skillavaliable = false;
        Vector2 skilldirection = actionplace - gameObject.GetComponent<Rigidbody2D>().position;
        StartCoroutine(FFF(skilldirection, 0.1f));
    }

    IEnumerator FFF(Vector2 direction, float waittime)
    {
        direction = Quaternion.AngleAxis(2 * (bulletamount - 2), Vector3.forward) * direction;
        int bnum = 0;
        while (bnum < bulletamount)
        {
            DoFire(gameObject.GetComponent<Rigidbody2D>().position + 0.6f * direction.normalized, direction.normalized * bulletspeed);
            direction = Quaternion.AngleAxis(-2, Vector3.forward) * direction;
            bnum++;
            yield return new WaitForSeconds(waittime);
        }
    }

    //[PunRPC]
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
