using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TestSkill03 : Photon.MonoBehaviour
{

    private float timetosing = 1;
    private float timesinged;

    // Use this for initialization
    void Start()
    {
        timesinged = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireF") && gameObject.GetComponent<SelfExplodeScript>().skillavaliable)
        {
            Skill();
        }
    }

    private void FixedUpdate()
    {
        if (DoSkill.singing != 3)
        {
            timesinged = 0;
            return;
        }
        else
        {
            timesinged += Time.fixedDeltaTime;
            if (timesinged >= timetosing)
            {
                gameObject.GetComponent<SelfExplodeScript>().Skill();
                timesinged = 0;
                DoSkill.singing = 0;
            }
        }
    }

    public void Skill()
    {
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        gameObject.GetComponent<StealthScript>().StealthEnd();
        DoSkill.singing = 3;
    }
}
