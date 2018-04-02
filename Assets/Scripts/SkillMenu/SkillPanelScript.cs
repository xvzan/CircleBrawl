using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelScript : MonoBehaviour {

    public Toggle PanelSwitch;

	// Use this for initialization
	void Start () {
        OnOffSwitch();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnOffSwitch()
    {
        if (PanelSwitch.isOn)
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
