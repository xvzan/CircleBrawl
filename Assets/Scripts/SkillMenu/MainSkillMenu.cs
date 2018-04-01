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
            if (SkillMenu.activeInHierarchy)
                SkillMenu.SetActive(false);
            else
                SkillMenu.SetActive(true);
        }
	}
}
