using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class SetSkillY : Photon.MonoBehaviour
{

    public Toggle Y1;
    public Toggle Y1a;
    public Toggle Y1b;
    public Toggle Y2;
    public Toggle Y2a;
    public Toggle Y2b;
    public Toggle Y3;
    public Toggle Y3a;
    public Toggle Y3b;
    public Toggle Y4;
    public Toggle Y4a;
    public Toggle Y4b;
    public Image IconY;
    GameObject Soldier;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetY()
    {
        if (Soldier == null)
            Soldier = gameObject.GetComponent<SkillsLink>().mySoldier;
        AllYOff();
        if (Y1.isOn && Y1a.isOn)
        {
            Soldier.GetComponent<SkillY1>().MyImageScript = IconY.GetComponent<CooldownImage>();
            Soldier.GetComponent<SkillY1>().enabled = true;
            return;
        }
    }

    void AllYOff()
    {
        Soldier.GetComponent<SkillY1>().enabled = false;
        Soldier.GetComponent<SkillY1>().MyImageScript = null;
    }
}
