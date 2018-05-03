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

    void SetSkillImages()
    {

    }
}
