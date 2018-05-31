using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class BoostScript : Photon.MonoBehaviour
{
    public GameObject sender;
    public float maxtime = 2;
    float timepsd = 0;
    
    void OnDestroy()
    {
        if (photonView.isMine && sender != null)
        {
            sender.GetComponent<HPScript>().boostend();
        }
    }
    
    void Update()
    {
        transform.position = sender.transform.position;
        if (timepsd >= maxtime || sender == null)
            gameObject.GetComponent<DestroyScript>().Destroyself();
    }

    void FixedUpdate()
    {
        timepsd += Time.fixedDeltaTime;
    }

    public void SetConf(int ids, float maxt)
    {
        photonView.RPC("SetBConf", PhotonTargets.All, ids, maxt);
    }

    [PunRPC]
    void SetBConf(int senderID, float maxT)
    {
        sender = PhotonView.Find(senderID).gameObject;
        maxtime = maxT;
    }
}
