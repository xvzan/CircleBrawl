using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class ShowMyName : Photon.MonoBehaviour
{
    public GameObject MyNameText;
    GameObject MyName;

    // Use this for initialization
    void Start () {
        Canvas TheCanvas = GameObject.FindObjectOfType<Canvas>();
        MyName = GameObject.Instantiate(MyNameText, TheCanvas.gameObject.transform);
        MyName.GetComponent<Text>().text = photonView.owner.NickName;
        GetComponent<StealthScript>().MyName = MyName;
    }

    private void Update()
    {
        MyName.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down * 0.8f);
    }
}
