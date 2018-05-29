using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class STScirpt : Photon.MonoBehaviour
{
    private float pasttime;
    public float maxtime = 2;
    public float BulletSpeed = 6;
    public GameObject fireball;
    //public Rigidbody2D selfRB2D;
    public GameObject sender;
    public Vector2 finalv;

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
        if (gameObject.GetComponent<DestroyScript>().selfprotect && collision.gameObject == sender)
            return;
        gameObject.GetComponent<DestroyScript>().Destroyself();
    }
    
    private void OnDestroy()
    {
        FFF(finalv);
    }

    void FFF(Vector2 direction)
    {
        direction = Quaternion.AngleAxis(17.5f, Vector3.forward) * direction;
        fireball.GetComponent<SABulletScript>().sender = null;
        int bnum = 0;
        while (bnum < 8)
        {
            DoFire(gameObject.GetComponent<Rigidbody2D>().position + 0.4f * direction.normalized, direction.normalized * BulletSpeed);
            direction = Quaternion.AngleAxis(-5, Vector3.forward) * direction;
            bnum++;
        }
    }

    void DoFire(Vector2 fireplace, Vector2 speed2d)
    {
        GameObject bullet = PhotonNetwork.Instantiate(fireball.name, fireplace, Quaternion.identity, 0);
        bullet.GetComponent<Rigidbody2D>().velocity = speed2d;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        gameObject.GetComponent<DestroyScript>().selfprotect = false;
    }
}
