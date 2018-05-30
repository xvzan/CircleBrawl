using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class SetSkillE : Photon.MonoBehaviour
{

    public Toggle E1;
    public Toggle E1a;
    public Toggle E1b;
    public Toggle E2;
    public Toggle E2a;
    public Toggle E2b;
    public Toggle E3;
    public Toggle E3a;
    public Toggle E3b;
    public Toggle E4;
    public Toggle E4a;
    public Toggle E4b;
    public Image IconE;
    GameObject Soldier;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void SetE()
    {
        if (Soldier == null)
            Soldier = gameObject.GetComponent<SkillsLink>().mySoldier;
        AllEOff();
        if (E1.isOn && E1a.isOn)
        {
            Soldier.GetComponent<SkillE1>().MyImageScript = IconE.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillE1>().enabled = true;
            return;
        }
        if (E1.isOn && E1b.isOn)
        {
            Soldier.GetComponent<SkillE1b>().MyImageScript = IconE.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillE1b>().enabled = true;
            return;
        }
        if (E2.isOn && E2a.isOn)
        {
            Soldier.GetComponent<SkillE2>().MyImageScript = IconE.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillE2>().enabled = true;
            return;
        }
        if (E2.isOn && E2b.isOn)
        {
            Soldier.GetComponent<SkillE2b>().MyImageScript = IconE.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillE2b>().enabled = true;
            return;
        }
        if (E3.isOn && E3a.isOn)
        {
            Soldier.GetComponent<SkillE3>().MyImageScript = IconE.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillE3>().enabled = true;
            return;
        }
        if (E3.isOn && E3b.isOn)
        {
            Soldier.GetComponent<SkillE3b>().MyImageScript = IconE.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillE3b>().enabled = true;
            return;
        }
    }

    public void AllEOff()
    {
        IconE.GetComponent<CooldownImage>().IconFillAmount = 1;
        Soldier.GetComponent<SkillE1>().enabled = false;
        Soldier.GetComponent<SkillE1>().MyImageScript = null;
        Soldier.GetComponent<SkillE1b>().enabled = false;
        Soldier.GetComponent<SkillE1b>().MyImageScript = null;
        Soldier.GetComponent<SkillE2>().enabled = false;
        Soldier.GetComponent<SkillE2>().MyImageScript = null;
        Soldier.GetComponent<SkillE2b>().enabled = false;
        Soldier.GetComponent<SkillE2b>().MyImageScript = null;
        Soldier.GetComponent<SkillE3>().enabled = false;
        Soldier.GetComponent<SkillE3>().MyImageScript = null;
        Soldier.GetComponent<SkillE3b>().enabled = false;
        Soldier.GetComponent<SkillE3b>().MyImageScript = null;
    }
}
