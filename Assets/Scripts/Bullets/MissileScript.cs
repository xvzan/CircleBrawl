using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class MissileScript : Photon.MonoBehaviour
{
    public Rigidbody2D Target;
    Rigidbody2D selfrb2d;
    public float Speed;

    // Use this for initialization
    void Start()
    {
        selfrb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Target == null)
            return;
        Vector2 direction = Target.position - selfrb2d.position;
        selfrb2d.velocity = direction.normalized * Speed;
    }
}
