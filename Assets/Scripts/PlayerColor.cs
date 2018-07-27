using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PlayerColor : Photon.MonoBehaviour
{
    public Component ColorMark;
    public int a;
    public int b;

    // Use this for initialization
    void Start () {
        a = PhotonNetwork.playerList.Length;
        b = photonView.ownerId;
        ColorMark.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(a / b, 1, 1);
	}
}
