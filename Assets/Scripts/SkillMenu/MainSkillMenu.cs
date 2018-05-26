using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSkillMenu : MonoBehaviour {

    public GameObject SkillMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (SkillMenu.GetComponent<CanvasGroup>().alpha == 0)
            {
                SkillMenu.GetComponent<CanvasGroup>().alpha = 1;
                SkillMenu.GetComponent<CanvasGroup>().interactable = true;
                SkillMenu.GetComponent<CanvasGroup>().blocksRaycasts = true;
                gameObject.GetComponent<SkillsLink>().alphaset();
            }
            else
                CloseMainSkillMenu();
        }
	}

    public void CloseMainSkillMenu()
    {
        if (SkillMenu.GetComponent<CanvasGroup>().alpha == 0)
            return;
        SkillMenu.GetComponent<CanvasGroup>().alpha = 0;
        SkillMenu.GetComponent<CanvasGroup>().interactable = false;
        SkillMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gameObject.GetComponent<SkillsLink>().alphaset();
    }
}
