using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TestSkill02 : Photon.MonoBehaviour
{

    public float maxdistance;
    public bool skillavaliable;
    MoveScript MS;

    // Use this for initialization
    void Start () {
        skillavaliable = true;
        MS = GetComponent<MoveScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire2") && skillavaliable)
        {
            DoSkill.singing = 0;
            gameObject.GetComponent<DoSkill>().Fire = Skill;
        }
    }

    public void Skill(Vector2 actionplace)
    {
        //DoSkill.singing = 0; //停止吟唱中技能
        Vector2 singplace = transform.position;
        Vector2 skilldirection = actionplace - singplace;
        float realdistance = Mathf.Min(skilldirection.magnitude, maxdistance);
        if (realdistance <= 0.6)
        {
            return;
        }   //半径小于自身半径时不施法
        else
        {
            //gameObject.GetComponent<DoSkill>().Fire = null;
            Vector2 realplace = singplace + skilldirection.normalized * realdistance;
            Rigidbody2D selfrb2d = gameObject.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
            MS.controllable = true;
            StartCoroutine(Skillcooldown());
            if (Physics2D.OverlapPoint(realplace))
            {
                Collider2D hit = Physics2D.OverlapPoint(realplace);
                HPScript hps = hit.GetComponent<HPScript>();
                if (hps != null)
                {
                    Rigidbody2D rb2d = hit.GetComponent<Rigidbody2D>();
                    selfrb2d.position = rb2d.position;
                    hps.TransferTo(singplace);
                }
                else
                {
                    transform.position = realplace;
                }
            }
            else
            {
                transform.position = realplace;
            }
        }
    }
    
    IEnumerator Skillcooldown()
    {
        skillavaliable = false;
        yield return new WaitForSeconds(3);
        skillavaliable = true;
    }
}
