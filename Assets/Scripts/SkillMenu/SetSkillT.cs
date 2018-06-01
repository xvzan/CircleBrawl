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
    public Image IconT;
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
            Soldier.GetComponent<TestSkillLeech>().MyImageScript = IconT.GetComponent<CooldownImage>();
            Soldier.GetComponent<TestSkillLeech>().enabled = true;
            return;
        }
        if (T1.isOn && T1b.isOn)
        {
            Soldier.GetComponent<SkillT1b>().MyImageScript = IconT.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillT1b>().enabled = true;
            return;
        }
        if (T2.isOn && T2a.isOn)
        {
            Soldier.GetComponent<SkillT2>().MyImageScript = IconT.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillT2>().enabled = true;
            return;
        }
        if (T2.isOn && T2b.isOn)
        {
            Soldier.GetComponent<SkillT2b>().MyImageScript = IconT.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillT2b>().enabled = true;
            return;
        }
        if (T3.isOn && T3a.isOn)
        {
            Soldier.GetComponent<SkillT3>().MyImageScript = IconT.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillT3>().enabled = true;
            return;
        }
        if (T3.isOn && T3b.isOn)
        {
            Soldier.GetComponent<SkillT3b>().MyImageScript = IconT.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillT3b>().enabled = true;
            return;
        }
    }

    public void AllTOff()
    {
        IconT.GetComponent<CooldownImage>().IconFillAmount = 1;
        Soldier.GetComponent<TestSkillLeech>().enabled = false;
        Soldier.GetComponent<TestSkillLeech>().MyImageScript = null;
        Soldier.GetComponent<SkillT1b>().enabled = false;
        Soldier.GetComponent<SkillT1b>().MyImageScript = null;
        Soldier.GetComponent<SkillT2>().enabled = false;
        Soldier.GetComponent<SkillT2>().MyImageScript = null;
        Soldier.GetComponent<SkillT2b>().enabled = false;
        Soldier.GetComponent<SkillT2b>().MyImageScript = null;
        Soldier.GetComponent<SkillT3>().enabled = false;
        Soldier.GetComponent<SkillT3>().MyImageScript = null;
        Soldier.GetComponent<SkillT3b>().enabled = false;
        Soldier.GetComponent<SkillT3b>().MyImageScript = null;
    }
}
