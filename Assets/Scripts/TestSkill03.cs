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
    private void FixedUpdate()
    {
        if (DoSkill.singing == 3)
        {
            timesinged += Time.fixedDeltaTime;
        }
        else
        {
            timesinged = 0;
        }
        if (timesinged >= timetosing)
        {
            gameObject.GetComponent<SelfExplodeScript>().Skill();
            timesinged = 0;
            DoSkill.singing = 0;
        }
    }

    public void Skill()
    {
        gameObject.GetComponent<MoveScript>().stopwalking(); //停止走动
        DoSkill.singing = 3;
    }
}
