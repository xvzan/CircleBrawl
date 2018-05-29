using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SAScript : Photon.MonoBehaviour
{

    private float pasttime;
    public float maxtime = 3;
    public float BulletSpeed = 6;
    public GameObject fireball;
    public GameObject sender;
    public Rigidbody2D selfRB2D;

    void FixedUpdate()
    {
        pasttime += Time.fixedDeltaTime;
        if (pasttime >= maxtime)
        {
            gameObject.GetComponent<DestroyScript>().Destroyself();
        }
    }

    public void StartFire()
    {
        StartCoroutine(FFF(0.2f));
    }

    IEnumerator FFF(float waittime)
    {
        Vector2 direction = selfRB2D.velocity;
        while (true)
        {
            DoFire(direction.normalized * BulletSpeed);
            direction = Quaternion.AngleAxis(50, Vector3.forward) * direction;
            yield return new WaitForSeconds(waittime);
        }
    }

    //[PunRPC]
    void DoFire(Vector2 speed2d)
    {
        fireball.GetComponent<SABulletScript>().sender = sender;
        GameObject bullet = PhotonNetwork.Instantiate(fireball.name, selfRB2D.position, Quaternion.identity, 0);
        bullet.GetComponent<Rigidbody2D>().velocity = speed2d;
    }
}
