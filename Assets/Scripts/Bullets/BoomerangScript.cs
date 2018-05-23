using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class BoomerangScript : Photon.MonoBehaviour
{
    public float a = 0.2f;
    public Rigidbody2D senderRB;
    public Rigidbody2D selfRB;
	
	void FixedUpdate() {
        if (!photonView.isMine)
            return;
        selfRB.velocity += (senderRB.position - selfRB.position) * a * Time.fixedDeltaTime;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<DestroyScript>().selfprotect)
            return;
        Vector2 sp = selfRB.velocity;
        Vector2 vp = collision.transform.position - transform.position;
        float angel12 = Vector2.Angle(sp, vp);
        selfRB.velocity = Quaternion.AngleAxis(180 - angel12 * 2, Vector3.Cross(vp, sp)) * sp;
        //selfRB.velocity = -selfRB.velocity;
    }
}
