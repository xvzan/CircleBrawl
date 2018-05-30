using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class RollScript : Photon.MonoBehaviour
{
    public Vector2 v;
    public Rigidbody2D selfRB;
    public float rolldamage = 2;

    private void FixedUpdate()
    {
        selfRB.velocity = v;
        rollSkill();
    }
    
    public void rollSkill()
    {
        float radius = transform.lossyScale.x / 2;
        Vector2 actionplace = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(actionplace, radius);
        foreach (Collider2D hit in colliders)
        {
            HPScript hp = hit.GetComponent<HPScript>();
            if (hp != null)
            {
                hp.GetHurt(rolldamage * Time.fixedDeltaTime);
            }
        }
    }
}
