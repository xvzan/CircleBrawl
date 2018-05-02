using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class SelfExplodeScript : Photon.MonoBehaviour
{
    
    private float currentcooldown;
    public float cooldowntime = 3;
    public bool skillavaliable;

    // Use this for initialization
    void Start()
    {
        currentcooldown = cooldowntime;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (skillavaliable)
            return;
        if (currentcooldown >= cooldowntime)
        {
            skillavaliable = true;
        }
        else
            currentcooldown += Time.fixedDeltaTime;
    }

    public void Skill()
    {
        //photonView.RPC("RealSkill", PhotonTargets.All);
        gameObject.GetComponent<MoveScript>().stopwalking();
        currentcooldown = 0;
        skillavaliable = false;
        float radius = 1.5f;
        Vector2 actionplace = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(actionplace, radius);
        foreach (Collider2D hit in colliders)
        {
            HPScript hp = hit.GetComponent<HPScript>();
            if (hp != null)
            {
                if (hit == gameObject.GetComponent<Collider2D>())
                {
                    hp.GetHurt(Mathf.Min(10, hp.currentHP - 1));
                }
                else
                {
                    hp.GetHurt(10);
                    Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 explforce;
                        explforce = rb.position - actionplace;
                        hit.GetComponent<RBScript>().GetPushed(explforce.normalized * 15, 1f);
                    }
                }
            }
        }
    }

    /*[PunRPC]
    public void RealSkill()
    {
    }*/
}
