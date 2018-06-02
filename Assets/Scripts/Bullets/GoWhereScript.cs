using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class GoWhereScript : Photon.MonoBehaviour
{
    public void GoHere(Vector2 des)
    {
        photonView.RPC("AllGoHere", PhotonTargets.All, des);
    }

    [PunRPC]
    public void AllGoHere(Vector2 des)
    {
        GetComponent<Rigidbody2D>().position = des;
    }
}
