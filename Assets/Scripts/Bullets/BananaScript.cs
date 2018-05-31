using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class BananaScript : Photon.MonoBehaviour
{
    public Rigidbody2D selfRB;
    public GameObject sender;
    public float turnangle;
    public float maxtime = 2;
    float pasttime = 0;
    public float bombpower = 5;
    public float pushtime = 1;
    public float bombdamage = 5;

    void FixedUpdate()
    {
        /*if (!photonView.isMine)
            return;*/
        pasttime += Time.fixedDeltaTime;
        if (pasttime >= maxtime)
        {
            gameObject.GetComponent<DestroyScript>().Destroyself();
            return;
        }
        selfRB.velocity = Quaternion.AngleAxis(turnangle * Time.fixedDeltaTime / (maxtime - 0.5f), Vector3.back) * selfRB.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!photonView.isMine)
            return;
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        HPScript hp = collision.gameObject.GetComponent<HPScript>();
        if (hp != null && rb != null)
        {
            Vector2 explforce;
            Rigidbody2D selfrb = gameObject.GetComponent<Rigidbody2D>();
            explforce = rb.position - selfrb.position;
            collision.gameObject.GetComponent<RBScript>().GetPushed(explforce.normalized * bombpower, pushtime);
            hp.GetHurt(bombdamage);
        }
        gameObject.GetComponent<DestroyScript>().Destroyself();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!photonView.isMine || collision.gameObject != sender)
            return;
        GetComponent<Collider2D>().isTrigger = false;
    }
}
