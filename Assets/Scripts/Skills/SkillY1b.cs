using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillY1b : Photon.MonoBehaviour
{
    public CooldownImage MyImageScript;
    public GameObject MyLineObj;
    public GameObject MyLine;
    public float maxdistance = 10;
    private float currentcooldown;
    public float cooldowntime = 3;
    public bool skillavaliable;
    public float speed;
    public float damage;
    public float maxtime;

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
            if (MyLine != null)
                MyLine.GetComponent<DestroyScript>().Destroyself();
            MyLine = PhotonNetwork.Instantiate(MyLineObj.name, gameObject.transform.position, Quaternion.identity, 0);
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
        if (realdistance <= 0.6)
        {
            return;
        }   //半径小于自身半径时不施法
        Vector2 realplace = singplace + skilldirection.normalized * realdistance;
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        gameObject.GetComponent<StealthScript>().StealthEnd();
        currentcooldown = 0;
        skillavaliable = false;
        if (Physics2D.OverlapPoint(realplace))
        {
            Collider2D hit = Physics2D.OverlapPoint(realplace);
            if (hit.GetComponent<HPScript>() != null)
            {
                MyLine.GetComponent<RedLineScript>().DoMyJob(hit.gameObject.GetPhotonView().photonView.viewID, gameObject.GetPhotonView().viewID, realplace, speed, damage, maxtime);
                return;
            }
        }
        MyLine.GetComponent<RedLineScript>().DoMyJob(0, gameObject.GetPhotonView().viewID, realplace, speed, damage, maxtime);
    }
}
