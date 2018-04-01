using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class CentrallyConstentField : Photon.MonoBehaviour
{
    Rigidbody2D center;
    public float speed;

    public void AddConstentCentrallyVelocity(Rigidbody2D victim,MoveScript worker)
    {
        Vector2 vector = center.position - victim.position;
        if (vector.magnitude < 0.1)
            return;
        worker.VelotoAdd += (center.position - victim.position).normalized * speed;
    }
    
    // Use this for initialization
    void Start () {
        center = GetComponent<Rigidbody2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MoveScript MS = collision.GetComponent<MoveScript>();
        MS.cook += AddConstentCentrallyVelocity;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MoveScript MS = collision.GetComponent<MoveScript>();
        MS.cook -= AddConstentCentrallyVelocity;
    }

    // Update is called once per frame
    void Update ()
    {

	}
}
