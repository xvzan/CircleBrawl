using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownImage : MonoBehaviour {

    public float IconFillAmount = 1;
    public Image Icon;

	// Use this for initialization
	void Start () {
        Icon = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        Icon.fillAmount = IconFillAmount;
    }
}
