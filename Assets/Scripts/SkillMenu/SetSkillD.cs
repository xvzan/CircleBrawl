using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class SetSkillD : Photon.MonoBehaviour
{

    public Toggle D1;
    public Toggle D2;
    public Toggle D3;
    public Toggle D4;
    public Toggle D5;
    public Image IconD;
    GameObject Soldier;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void SetD()
    {
        if (Soldier == null)
            Soldier = gameObject.GetComponent<SkillsLink>().mySoldier;
        AllDOff();
        if (D1.isOn)
        {
            Soldier.GetComponent<TestSkillLightning>().MyImageScript = IconD.GetComponent<CooldownImage>();
            Soldier.GetComponent<TestSkillLightning>().enabled = true;
            return;
        }
        if (D2.isOn)
        {
            Soldier.GetComponent<SkillD2>().MyImageScript = IconD.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillD2>().enabled = true;
            return;
        }
        if (D3.isOn)
        {
            Soldier.GetComponent<SkillD3>().MyImageScript = IconD.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillD3>().enabled = true;
            return;
        }
    }

    public void AllDOff()
    {
        IconD.GetComponent<CooldownImage>().IconFillAmount = 1;
        Soldier.GetComponent<TestSkillLightning>().enabled = false;
        Soldier.GetComponent<TestSkillLightning>().MyImageScript = null;
        Soldier.GetComponent<SkillD2>().enabled = false;
        Soldier.GetComponent<SkillD2>().MyImageScript = null;
        Soldier.GetComponent<SkillD3>().enabled = false;
        Soldier.GetComponent<SkillD3>().MyImageScript = null;
    }
}
