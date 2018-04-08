using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsLink : MonoBehaviour {

    public GameObject mySoldier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void linktome()
    {
        mySoldier.GetComponent<LinktoUI>().MyUI = gameObject;
    }
}
