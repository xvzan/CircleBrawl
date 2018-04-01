using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class HPScript : Photon.MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    private GameObject safeground;
    float outhurt = 2;

    // Use this for initialization
    void Start () {
        currentHP = maxHP;
        safeground = GameObject.Find("GroundCircle");
	}

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (transform.position.magnitude > safeground.transform.lossyScale.x / 2)
        {
            currentHP -= outhurt * Time.fixedDeltaTime;
        }
    }

    void LateUpdate () {
		if(currentHP <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
	}

    private void OnDestroy()
    {
        GameObject[] PlayersLeft = GameObject.FindGameObjectsWithTag("Player");
        if (PlayersLeft.Length <= 1)
        {
            PhotonNetwork.LoadLevel("Sences/TestMenu");
        }
    }


    public void TransferTo(Vector2 destination)
    {
        photonView.RPC("DoTransferTo", PhotonTargets.All, destination);
    }

    [PunRPC]
    public void DoTransferTo(Vector2 destination)
    {
        GetComponent<Rigidbody2D>().position = destination;
    }

    public void GetHurt(float damage)
    {
        photonView.RPC("DoGetHurt", PhotonTargets.All, damage);
    }

    [PunRPC]
    void DoGetHurt(float damage)
    {
        currentHP -= damage;
    }

    public void GetKicked(Vector2 force)
    {
        photonView.RPC("DoGetKicked", PhotonTargets.All, force);
    }

    [PunRPC]
    void DoGetKicked(Vector2 force)
    {
        GetComponent<Rigidbody2D>().AddForce(force);
    }

}
