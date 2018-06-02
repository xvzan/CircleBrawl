using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleSkillSetting : MonoBehaviour {

    public GameObject GoldObj;
    public GameObject MinusButton;
    public GameObject PlusButton;
    public Text LevelText;
    public Text LevelPriceText;
    public Toggle SkillToggle;
    public int SkillLevel = 0;
    public int BottomLevel = 0;
    public int TopLevel = 6;
    public int TopLevel00 = 6;
    public int TopLevel01;
    public int TopLevel02;
    public int TopLevel03;
    public int TopLevelJordan;
    public int LevelPrice;// = { 11, 4, 5, 6, 7, 8, 0 };
    public int Level1Price = 11;
    public int Level2Price = 3;
    public int MultiInt = 1;

    // Use this for initialization
    void Start () {
        TopLevel = 1;//Under Construction
        UpdatePrice();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SkillToggleCtrl()
    {
        if (SkillToggle.isOn)
            TurnOn();
        else
            TurnOff();
    }

    void TurnOn()
    {
        if (SkillLevel == 0)
            ClickPlusButton();
    }

    void TurnOff()
    {
        while (SkillLevel > 0)
            ClickMinusButton();
    }

    public void ClickMinusButton()
    {
        SkillLevel -= 1;
        UpdatePrice();
        GoldObj.GetComponent<GoldScript>().GoldPoint += LevelPrice;
    }

    public void ClickPlusButton()
    {
        if (GoldObj.GetComponent<GoldScript>().GoldPoint < LevelPrice)
            return;
        GoldObj.GetComponent<GoldScript>().GoldPoint -= LevelPrice;
        SkillLevel += 1;
        if (!SkillToggle.isOn)
            SkillToggle.isOn = true;
        UpdatePrice();
    }

    void UpdatePrice()
    {
        if (SkillLevel == 0)
            LevelPrice = Level1Price;
        else if (SkillLevel == TopLevel)
            LevelPrice = 0;
        else
            LevelPrice = SkillLevel * MultiInt + Level2Price;
        LevelText.text = "Lv" + SkillLevel;
        LevelPriceText.text = "Price:" + LevelPrice;
        SetButtons();
    }

    void SetButtons()
    {
        SetMinusButton();
        SetPlusButton();
    }

    void SetMinusButton()
    {
        if (SkillLevel < 1 + BottomLevel)
        {
            MinusButton.SetActive(false);
            if (SkillToggle.isOn)
                SkillToggle.isOn = false;
        }
        else
            MinusButton.SetActive(true);
    }

    void SetPlusButton()
    {
        if (SkillLevel >= TopLevel)
            PlusButton.SetActive(false);
        else
            PlusButton.SetActive(true);
    }
}
