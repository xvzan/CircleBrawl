using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class ShieldScript : Photon.MonoBehaviour
{
    public GameObject sender;
    public float maxtime = 2;
    float timepsd = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
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
        photonView.RPC("SetMyConf", PhotonTargets.All, ids, maxt);
    }

    [PunRPC]
    void SetMyConf(int senderID, float maxT)
    {
        sender = PhotonView.Find(senderID).gameObject;
        maxtime = maxT;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DestroyScript>() != null && collision.GetComponent<DestroyScript>().breakable)
            collision.GetComponent<DestroyScript>().Destroyself();
    }
}
