using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class RockExplode : Photon.MonoBehaviour
{
    public float damage = 10;
    public float bombforce = 18;
    public float pushtime = 1;
    private float timetosing = 2;
    private float timesinged = 0;

    // Use this for initialization
    void Start ()
    {
        timesinged = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        timesinged += Time.fixedDeltaTime;
        if (timesinged >= timetosing)
            Skill();
    }

    public void Skill()
    {
        //photonView.RPC("RealSkill", PhotonTargets.All);
        GetComponent<DestroyScript>().Destroyself();
    }

    //[PunRPC]
    public void RealSkill()
    {
        //gameObject.GetComponent<MoveScript>().stopwalking();
        float radius = transform.lossyScale.x / 2;
        Vector2 actionplace = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(actionplace, radius);
        foreach (Collider2D hit in colliders)
        {
            HPScript hp = hit.GetComponent<HPScript>();
            if (hp != null)
            {
                hp.GetHurt(damage);
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 explforce;
                    explforce = rb.position - actionplace;
                    hit.GetComponent<RBScript>().GetPushed(explforce.normalized * bombforce, pushtime);
                }
            }
        }
    }

    private void OnDestroy()
    {
        RealSkill();
    }
}
