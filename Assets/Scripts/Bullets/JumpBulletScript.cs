using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBulletScript : MonoBehaviour {

    GameObject sender;
    GameObject Target;
    public float speed;
    public Rigidbody2D bulletRB2D;
    Rigidbody2D targetRB2D;
    private int jumpcount;
    public int maxjumptime;
    bool beginning = true;

	// Use this for initialization
	void Start () {
        jumpcount = 0;
	}
	
	void FixedUpdate () {
        Vector2 distance = targetRB2D.position - bulletRB2D.position;
        if (!beginning)
            bulletRB2D.velocity = distance.normalized * speed;
	}

    private void hitTarget(GameObject victim)
    {
        victim.GetComponent<HPScript>().GetHurt(3);
        jumpcount++;
        if (jumpcount == maxjumptime)
            GameObject.Destroy(this);
        bulletRB2D.position = targetRB2D.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Target)
        {
            hitTarget(other.gameObject);
            GetNextTarget();
            return;
        }
        if (beginning)
        {
            hitTarget(other.gameObject);
            Target = other.gameObject;
            GetNextTarget();
            beginning = false;
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
        targetRB2D = Target.GetComponent<Rigidbody2D>();
    }
}
