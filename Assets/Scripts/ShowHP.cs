using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class ShowHP : Photon.MonoBehaviour
{
    //public Component bar;
    public GameObject MyHPGO;
    GameObject MyHP;
    Text MyHPText;

    // Use this for initialization
    void Start ()
    {
        Canvas TheCanvas = GameObject.FindObjectOfType<Canvas>();
        MyHP = GameObject.Instantiate(MyHPGO, TheCanvas.gameObject.transform);
        MyHPText = MyHP.gameObject.GetComponentInChildren<Text>();
        GetComponent<StealthScript>().BarSR = MyHP;
    }
	
	// Update is called once per frame
	void Update ()
    {
        MyHP.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.7f);
        MyHP.GetComponent<Slider>().value = gameObject.GetComponent<HPScript>().currentHP / gameObject.GetComponent<HPScript>().maxHP;
        MyHPText.text = Mathf.Round(gameObject.GetComponent<HPScript>().currentHP * 10) / 10 + "/" + gameObject.GetComponent<HPScript>().maxHP;
    }

    private void OnDestroy()
    {
        Destroy(MyHP);
    }
}
