using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class SetSkillR : Photon.MonoBehaviour
{
    public Toggle R1;
    public Toggle R1a;
    public Toggle R1b;
    public Toggle R2;
    public Toggle R2a;
    public Toggle R2b;
    public Toggle R3;
    public Toggle R3a;
    public Toggle R3b;
    public Image IconR;
    GameObject Soldier;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetR()
    {
        if (Soldier == null)
            Soldier = gameObject.GetComponent<SkillsLink>().mySoldier;
        AllROff();
        if (R1.isOn && R1a.isOn)
        {
            Soldier.GetComponent<TestSkill02>().MyImageScript = IconR.GetComponent<CooldownImage>();
            Soldier.GetComponent<TestSkill02>().enabled = true;
            return;
        }
    }

    void AllROff()
    {
        Soldier.GetComponent<TestSkill02>().enabled = false;
        Soldier.GetComponent<TestSkill02>().MyImageScript = null;
    }
}
