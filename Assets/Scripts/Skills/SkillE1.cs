using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillE1 : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public GameObject TheRock;
    public float maxdistance = 5;
    public float damage = 10;
    public float bombforce = 18;
    private float currentcooldown;
    public float cooldowntime = 3;
    public bool skillavaliable;

    // Use this for initialization
    void Start ()
    {
        currentcooldown = cooldowntime;
    }
	
    // Update is called once per frame
    void Update ()
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
        Vector2 singplace = transform.position;
        Vector2 skilldirection = actionplace - singplace;
        if (skilldirection.magnitude > maxdistance)
            actionplace = singplace + skilldirection.normalized * maxdistance;
        GameObject MyRock = PhotonNetwork.Instantiate(TheRock.name, actionplace, Quaternion.identity, 0);
        MyRock.GetComponent<RockExplode>().damage = damage;
        MyRock.GetComponent<RockExplode>().bombforce = bombforce;
        currentcooldown = 0;
        skillavaliable = false;
    }
}
