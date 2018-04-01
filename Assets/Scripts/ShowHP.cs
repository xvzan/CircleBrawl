using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class ShowHP : Photon.MonoBehaviour
{
    Component bar;

	// Use this for initialization
	void Start () {
        bar = gameObject.transform.Find("healthtop");
	}
	
	// Update is called once per frame
	void Update () {
        bar.transform.localScale = new Vector3(gameObject.GetComponent<HPScript>().currentHP / 100, 0.13f, 1);
	}
}
