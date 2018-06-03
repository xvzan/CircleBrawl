using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class StarScript : Photon.MonoBehaviour
{
    public float powerpers = 2;

    private void FixedUpdate()
    {
        if (!photonView.isMine)
            return;
        perSkill();
    }

    public void perSkill()
    {
        float radius = transform.lossyScale.x / 2;
        Vector2 actionplace = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(actionplace, radius);
        foreach (Collider2D hit in colliders)
        {
            HPScript hp = hit.GetComponent<HPScript>();
            if (hp != null)
            {
                if (hp.gameObject.GetPhotonView().isMine)
                    hp.GetHurt(-powerpers * Time.fixedDeltaTime);
                else
                    hp.GetHurt(powerpers * Time.fixedDeltaTime);
            }
        }
    }
}
