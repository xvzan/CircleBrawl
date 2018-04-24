using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class BombExplode : Photon.MonoBehaviour
{
    private float pasttime;
    public float maxtime;
    public float bombpower;
    public float bombdamage;
    public GameObject sender;
    private bool selfprotect;

	// Use this for initialization
	void Start () {
        selfprotect = true;
        pasttime = 0;
        maxtime = 2;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        pasttime += Time.fixedDeltaTime;
        if (pasttime >= maxtime)
        {
            gameObject.GetComponent<DestroyScript>().Destroyself();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!photonView.isMine)
            return;
        if (collision.gameObject == sender && selfprotect)
            return;
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        HPScript hp = collision.gameObject.GetComponent<HPScript>();
        if (hp != null && rb != null)
        {
            Vector2 explforce;
            Rigidbody2D selfrb = gameObject.GetComponent<Rigidbody2D>();
            explforce = rb.position - selfrb.position;
            collision.gameObject.GetComponent<RBScript>().GetPushed(explforce.normalized * bombpower, 1f);
            //hp.GetKicked(explforce.normalized * bombpower);
            hp.GetHurt(bombdamage);
        }
        gameObject.GetComponent<DestroyScript>().Destroyself();
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        selfprotect = false;
    }
}
