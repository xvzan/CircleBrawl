using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillE3b : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public float maxdistance = 15;
    public float maxtime = 3;
    public GameObject SA;
    //public GameObject SAB;
    public float damage = 3;
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
        if (Input.GetButtonDown("FireE") && skillavaliable)
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
        float realdistance = Mathf.Min(skilldirection.magnitude, maxdistance);
        float bulletspeed = realdistance / maxtime;
        DoFire(singplace, skilldirection.normalized * bulletspeed);
        currentcooldown = 0;
        skillavaliable = false;
    }

    //[PunRPC]
    void DoFire(Vector2 fireplace, Vector2 speed2d)
    {
        GameObject bullet;
        SA.GetComponent<SAScript>().sender = gameObject;
        bullet = PhotonNetwork.Instantiate(SA.name, fireplace, Quaternion.identity, 0);
        bullet.GetComponent<Rigidbody2D>().velocity = speed2d;
        bullet.GetComponent<SAScript>().maxtime = maxtime;
        bullet.GetComponent<SAScript>().StartFire();
    }
}
