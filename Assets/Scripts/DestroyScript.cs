using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class DestroyScript : Photon.MonoBehaviour
{
    public void Destroyself()
    {
        photonView.RPC("SDestroy", PhotonTargets.All);
    }

    [PunRPC]
    void SDestroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
