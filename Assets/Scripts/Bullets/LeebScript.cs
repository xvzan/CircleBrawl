using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class LeebScript : Photon.MonoBehaviour
{
    public GameObject sender;
    GameObject Target;
    public float speed;
    public Rigidbody2D bulletRB2D;
    Rigidbody2D targetRB2D;
    public float Damage;
    float currenttime = 0;
    public float maxtime = 1;
    
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
            gameObject.GetComponent<DestroyScript>().Destroyself();
            return;
        }
        Target = other.gameObject;
        if (Target == sender)
        {
            targetRB2D = Target.GetComponent<Rigidbody2D>();
            bulletRB2D.position = targetRB2D.position;
            GetNextTarget();
        }
        else
        {
            if (Target.GetComponent<HPScript>() != null)
            {
                Target.GetComponent<HPScript>().GetHurt(Damage);
                sender.GetComponent<HPScript>().GetHurt(-Damage);
                gameObject.GetComponent<DestroyScript>().Destroyself();
            }
        }
    }

    private void GetNextTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
        GameObject nextTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        foreach (GameObject potentialTarget in enemies)
        {
            if (potentialTarget == Target || potentialTarget == sender)
                continue;
            Vector3 directionToTarget = potentialTarget.transform.position - Target.transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                nextTarget = potentialTarget;
            }
        }
        Target = nextTarget;
        if (Target == null)
            gameObject.GetComponent<DestroyScript>().Destroyself();
        targetRB2D = Target.GetComponent<Rigidbody2D>();
        bulletRB2D.velocity = (targetRB2D.position - bulletRB2D.position).normalized * speed;
    }
}
