using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsLink : MonoBehaviour {

    public GameObject mySoldier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void linktome()
    {
        mySoldier.GetComponent<LinktoUI>().MyUI = gameObject;
    }

    public void alphaset()
    {
        gameObject.GetComponent<SetSkillC>().SetC();
        gameObject.GetComponent<SetSkillD>().SetD();
        gameObject.GetComponent<SetSkillE>().SetE();
        gameObject.GetComponent<SetSkillF>().SetF();
        gameObject.GetComponent<SetSkillG>().SetG();
        gameObject.GetComponent<SetSkillR>().SetR();
        gameObject.GetComponent<SetSkillT>().SetT();
        gameObject.GetComponent<SetSkillY>().SetY();
    }

    /*
    public void SetY1()
    {
        SkillY1 MySkill = mySoldier.GetComponent<SkillY1>();
        MySkill.enabled = true;
    }
    public void SetC3()
    {
        SkillC3 MySkill = mySoldier.GetComponent<SkillC3>();
        MySkill.enabled = true;
    }
    public void SetE1()
    {
        SkillE1 MySkill = mySoldier.GetComponent<SkillE1>();
        MySkill.enabled = true;
    }
    public void SetTS01()
    {
        TestSkill01 MySkill = mySoldier.GetComponent<TestSkill01>();
        MySkill.enabled = true;
    }
    public void SetTS02()
    {
        TestSkill02 MySkill = mySoldier.GetComponent<TestSkill02>();
        MySkill.enabled = true;
    }
    public void SetTS03()
    {
        TestSkill03 MySkill = mySoldier.GetComponent<TestSkill03>();
        MySkill.enabled = true;
        mySoldier.GetComponent<SelfExplodeScript>().enabled = true;
    }
    public void SetTSle()
    {
        TestSkillLeech MySkill = mySoldier.GetComponent<TestSkillLeech>();
        MySkill.enabled = true;
    }
    public void SetTSli()
    {
        TestSkillLightning MySkill = mySoldier.GetComponent<TestSkillLightning>();
        MySkill.enabled = true;
    }
    */
}
