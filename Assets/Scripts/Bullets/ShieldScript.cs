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
    /*
    void Start ()
    {
        if (sender != null)
        {
            //sender.GetComponent<Collider2D>().enabled = false;
            sender.GetComponent<TestSkillLightning>().SelfR = 1.01f;
        }
	}

    void OnDestroy()
    {
        if (sender != null)
        {
            //sender.GetComponent<Collider2D>().enabled = true;
            sender.GetComponent<TestSkillLightning>().SelfR = 0.51f;
        }
    }*/
	
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
        if ((collision.transform.position - transform.position).sqrMagnitude < 0.8)
            return;
        if (collision.GetComponent<MoveScript>() != null)
        {
            Vector2 sp = collision.GetComponent<MoveScript>().Givenvelocity;
            Vector2 vp = transform.position - collision.transform.position;
            float angel12 = Vector2.Angle(sp, vp);
            collision.GetComponent<MoveScript>().Givenvelocity = Quaternion.AngleAxis(180 - angel12 * 2, Vector3.Cross(vp, sp)) * sp;
            return;
        }
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            Vector2 sp = collision.GetComponent<Rigidbody2D>().velocity;
            Vector2 vp = transform.position - collision.transform.position;
            float angel12 = Vector2.Angle(sp, vp);
            collision.GetComponent<Rigidbody2D>().velocity = Quaternion.AngleAxis(180 - angel12 * 2, Vector3.Cross(vp, sp)) * sp;
        }
    }
}
