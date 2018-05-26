using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class SetSkillC : UnityEngine.MonoBehaviour
{

    public Toggle C1;
    public Toggle C2;
    public Toggle C3;
    public Toggle C4;
    public Image IconC;
    GameObject Soldier;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetC()
    {
        if (Soldier == null)
            Soldier = gameObject.GetComponent<SkillsLink>().mySoldier;
        AllCOff();
        if (C2.isOn)
        {
            Soldier.GetComponent<SkillC2>().MyImageScript = IconC.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillC2>().enabled = true;
            return;
        }
        if (C3.isOn)
        {
            Soldier.GetComponent<SkillC3>().MyImageScript = IconC.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillC3>().enabled = true;
            return;
        }
    }

    public void AllCOff()
    {
        IconC.GetComponent<CooldownImage>().IconFillAmount = 1;
        Soldier.GetComponent<SkillC2>().enabled = false;
        Soldier.GetComponent<SkillC2>().MyImageScript = null;
        Soldier.GetComponent<SkillC3>().enabled = false;
        Soldier.GetComponent<SkillC3>().MyImageScript = null;
    }
}
