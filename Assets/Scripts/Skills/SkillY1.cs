using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SkillY1 : Photon.MonoBehaviour
{
    public GameObject MyLineObj;
    public GameObject MyLine;
    //BlueLineScript MylineScript;
    public float maxdistance = 10;
    public bool skillavaliable;
    public float speed;
    public float damage;
    public float maxtime;

    // Use this for initialization
    void Start ()
    {
        if (!photonView.isMine)
            enabled = false;
        skillavaliable = true;
        //MylineScript = MyLineObj.GetComponent<BlueLineScript>();
        MyLineObj.GetComponent<BlueLineScript>().sender = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("FireQ") && skillavaliable)
        {
            DoSkill.singing = 0;
            if (MyLine != null)
                MyLine.GetComponent<DestroyScript>().Destroyself();
            MyLine = PhotonNetwork.Instantiate(MyLineObj.name, gameObject.transform.position, Quaternion.identity, 0);
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
                MyLine.GetComponent<BlueLineScript>().sender = gameObject.GetComponent<Rigidbody2D>();
                MyLine.GetComponent<BlueLineScript>().maxtime = maxtime;
                MyLine.GetComponent<BlueLineScript>().damage = damage;
                MyLine.GetComponent<BlueLineScript>().speed = speed;
                MyLine.GetComponent<BlueLineScript>().receiverID = hit.gameObject.GetPhotonView().photonView.viewID;
                MyLine.GetComponent<BlueLineScript>().EnableSelf();
                return;
            }
        }
        MyLine.GetComponent<BlueLineScript>().sender = gameObject.GetComponent<Rigidbody2D>();
        MyLine.GetComponent<BlueLineScript>().receiverID = 0;
        MyLine.GetComponent<BlueLineScript>().IMissed(realplace);
        MyLine.GetComponent<BlueLineScript>().EnableSelf();
    }

    IEnumerator Skillcooldown()
    {
        skillavaliable = false;
        yield return new WaitForSeconds(3);
        skillavaliable = true;
    }
}
