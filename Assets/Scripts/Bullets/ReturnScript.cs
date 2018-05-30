using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class ReturnScript : Photon.MonoBehaviour
{
    public Rigidbody2D Target;
    public Rigidbody2D selfrb2d;
    public float Speed;

    void FixedUpdate()
    {
        if (!photonView.isMine || Target == null)
            return;
        Vector2 direction = Target.position - selfrb2d.position;
        if (direction.sqrMagnitude <= 0.3)
            gethome();
        selfrb2d.velocity = direction.normalized * Speed;
    }

    void gethome()
    {
        Target.gameObject.GetComponent<SkillT3b>().ResetCD();
        GetComponent<DestroyScript>().Destroyself();
    }
}
