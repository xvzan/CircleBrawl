using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillY1 : Photon.MonoBehaviour
{
    public GameObject MyLineObj;
    BlueLineScript MylineScript;
    public float maxdistance = 10;
    public bool skillavaliable;
    public Rigidbody2D center;
    public float speed = 3;
    public float damage = 5;
    public float maxtime = 2;

    // Use this for initialization
    void Start ()
    {
        skillavaliable = true;
        //MylineScript = MyLineObj.GetComponent<BlueLineScript>();
        //MylineScript.sender = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("FireQ") && skillavaliable)
        {
            DoSkill.singing = 0;
            gameObject.GetComponent<DoSkill>().Fire = Skill;
        }
    }

    void FixedUpdate()
    {

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
        StartCoroutine(Skillcooldown());//技能冷却
        if (Physics2D.OverlapPoint(realplace))
        {
            Collider2D hit = Physics2D.OverlapPoint(realplace);
            if (hit.GetComponent<HPScript>() != null)
            {
                /*
                MylineScript.missed = false;
                MylineScript.receiver = hit.gameObject;
                MylineScript.maxtime = maxtime;
                MylineScript.damage = damage;
                */
                GameObject LineHit = PhotonNetwork.Instantiate(MyLineObj.name, gameObject.transform.position, Quaternion.identity, 0);
                LineHit.GetComponent<BlueLineScript>().sender = gameObject.GetComponent<Rigidbody2D>();
                LineHit.GetComponent<BlueLineScript>().maxtime = maxtime;
                LineHit.GetComponent<BlueLineScript>().damage = damage;
                LineHit.GetComponent<BlueLineScript>().receiver = hit.gameObject;
                LineHit.GetComponent<BlueLineScript>().IHit(hit.gameObject);
                return;
            }
        }
        GameObject MissedLine = PhotonNetwork.Instantiate(MyLineObj.name, gameObject.transform.position, Quaternion.identity, 0);
        MissedLine.GetComponent<BlueLineScript>().sender = gameObject.GetComponent<Rigidbody2D>();
        MissedLine.GetComponent<BlueLineScript>().IMissed(realplace);
    }

    IEnumerator Skillcooldown()
    {
        skillavaliable = false;
        yield return new WaitForSeconds(3);
        skillavaliable = true;
    }
}
