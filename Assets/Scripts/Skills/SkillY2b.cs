using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillY2b : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public GameObject MySuiteObj;
    //public GameObject MySuite;
    public float maxdistance = 10;
    private float currentcooldown;
    public float cooldowntime = 3;
    public bool skillavaliable;
    //public float maxtime;

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
            /*
            if (MySuite != null)
                MySuite.GetComponent<DestroyScript>().Destroyself();
            MySuite = PhotonNetwork.Instantiate(MySuiteObj.name, gameObject.transform.position, Quaternion.identity, 0);
            */
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
        float realdistance = Mathf.Min(skilldirection.magnitude, maxdistance) + 2;
        if (realdistance <= 2.5)
        {
            return;
        }   //半径小于自身半径时不施法
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        gameObject.GetComponent<StealthScript>().StealthEnd();
        currentcooldown = 0;
        skillavaliable = false;
        Vector2 Suiteplace = singplace - skilldirection.normalized * 2;
        GameObject MySuite = PhotonNetwork.Instantiate(MySuiteObj.name, Suiteplace, Quaternion.identity, 0);
        MySuite.GetComponent<SilenceSuiteScript>().work(skilldirection.normalized * realdistance);
    }
}
