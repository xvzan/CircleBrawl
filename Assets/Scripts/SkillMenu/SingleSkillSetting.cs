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
    int SkillLevel = 0;
    public int TopLevel = 6;
    public int[] LevelPrice = { 11, 4, 5, 6, 7, 8, 0 };

    // Use this for initialization
    void Start () {
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
        GoldObj.GetComponent<GoldScript>().GoldPoint += LevelPrice[SkillLevel];
        UpdatePrice();
    }

    public void ClickPlusButton()
    {
        if (!SkillToggle.isOn)
            SkillToggle.isOn = true;
        if (GoldObj.GetComponent<GoldScript>().GoldPoint < LevelPrice[SkillLevel])
            return;
        GoldObj.GetComponent<GoldScript>().GoldPoint -= LevelPrice[SkillLevel];
        SkillLevel += 1;
        UpdatePrice();
    }

    void UpdatePrice()
    {
        LevelText.text = "Lv" + SkillLevel;
        LevelPriceText.text = "Price:" + LevelPrice[SkillLevel];
        SetButtons();
    }

    void SetButtons()
    {
        SetMinusButton();
        SetPlusButton();
    }

    void SetMinusButton()
    {
        if (SkillLevel < 1)
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
