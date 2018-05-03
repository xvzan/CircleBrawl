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
    }

    void AllDOff()
    {
        Soldier.GetComponent<TestSkillLightning>().enabled = false;
        Soldier.GetComponent<TestSkillLightning>().MyImageScript = null;
    }
}
