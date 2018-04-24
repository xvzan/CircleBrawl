using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class SetSkillT : Photon.MonoBehaviour
{

    public Toggle T1;
    public Toggle T1a;
    public Toggle T1b;
    public Toggle T2;
    public Toggle T2a;
    public Toggle T2b;
    public Toggle T3;
    public Toggle T3a;
    public Toggle T3b;
    GameObject Soldier;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetT()
    {
        if (Soldier == null)
            Soldier = gameObject.GetComponent<SkillsLink>().mySoldier;
        AllTOff();
        if (T1.isOn && T1a.isOn)
        {
            Soldier.GetComponent<TestSkillLeech>().enabled = true;
            return;
        }
        if (T2.isOn && T2a.isOn)
        {
            Soldier.GetComponent<SkillT2>().enabled = true;
            return;
        }
    }

    void AllTOff()
    {
        Soldier.GetComponent<TestSkillLeech>().enabled = false;
        Soldier.GetComponent<SkillT2>().enabled = false;
    }
}
