using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class ShowMyName : Photon.MonoBehaviour
{
    public string myname;

    // Use this for initialization
    void Start () {
        myname = photonView.owner.NickName;
    }

    private void OnGUI()
    {
        Vector2 namepos = Camera.main.WorldToScreenPoint(transform.position + Vector3.down * 0.6f);
        Vector2 nameSize = GUI.skin.label.CalcSize(new GUIContent(myname));
        GUI.Label(new Rect(namepos.x - (nameSize.x / 2), Screen.height - namepos.y, nameSize.x, nameSize.y), myname);
    }
}
