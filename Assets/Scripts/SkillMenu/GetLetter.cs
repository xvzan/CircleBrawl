using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetLetter : MonoBehaviour {
    public string mykeyname = "FireQ";
    public string mykeydefault = "Q";

    // Use this for initialization
    void Start ()
    {
        GetComponent<Text>().text = PlayerPrefs.GetString(mykeyname, mykeydefault);
    }
}
