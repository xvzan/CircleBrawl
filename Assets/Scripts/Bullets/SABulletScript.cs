using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SABulletScript : Photon.MonoBehaviour
{

    public GameObject sender;
    //public Rigidbody2D bulletRB2D;
    public float Damage = 2;
    float currenttime = 0;
    public float maxtime = 2;

    void FixedUpdate()
    {
        if (!photonView.isMine)
            return;
        currenttime += Time.fixedDeltaTime;
        if (currenttime >= maxtime)
            gameObject.GetComponent<DestroyScript>().Destroyself();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!photonView.isMine)
            return;
        if (other.gameObject.GetComponent<DestroyScript>() != null && other.gameObject.GetComponent<DestroyScript>().breakable)
        {
            other.gameObject.GetComponent<DestroyScript>().Destroyself();
            //gameObject.GetComponent<DestroyScript>().Destroyself();
            return;
        }
        if (other.gameObject == sender)
            return;
        if (other.GetComponent<HPScript>() != null)
        {
            other.GetComponent<HPScript>().GetHurt(Damage);
            gameObject.GetComponent<DestroyScript>().Destroyself();
        }
    }
}
