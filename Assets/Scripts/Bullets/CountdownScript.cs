using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class CountdownScript : Photon.MonoBehaviour
{
    public float maxtime = 3;
    float currenttime = 0;

    private void FixedUpdate()
    {
        if (!photonView.isMine)
            return;
        currenttime += Time.fixedDeltaTime;
        if (currenttime >= maxtime)
            GetComponent<DestroyScript>().Destroyself();
    }
}
