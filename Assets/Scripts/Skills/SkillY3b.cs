using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillY3b : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public GameObject TheStar;
    public float maxdistance = 6;
    public float powerpersecond = 2;
    public float maxtime = 4;
    private float currentcooldown;
    public float cooldowntime = 10;
    public bool skillavaliable;

    // Use this for initialization
    void Start()
    {
        currentcooldown = cooldowntime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireY") && skillavaliable)
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
        GetComponent<DoSkill>().BeforeSkill();
        if (skilldirection.magnitude > maxdistance)
            actionplace = singplace + skilldirection.normalized * maxdistance;
        GameObject MyRock = PhotonNetwork.Instantiate(TheStar.name, actionplace, Quaternion.identity, 0);
        MyRock.GetComponent<StarScript>().powerpers = powerpersecond;
        MyRock.GetComponent<CountdownScript>().maxtime = maxtime;
        currentcooldown = 0;
        skillavaliable = false;
    }
}
