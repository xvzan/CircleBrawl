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
    bool boost = false;
    public float boostnow = 0;
    public float boostmax = 25;

    // Use this for initialization
    void Start () {
        currentHP = maxHP;
        safeground = GameObject.Find("GroundCircle");
        boost = false;
	}

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (transform.position.magnitude > safeground.transform.lossyScale.x / 2 && photonView.isMine)
        {
            currentHP -= outhurt * Time.fixedDeltaTime;
        }
    }

    void LateUpdate () {
		if(currentHP <= 0 && photonView.isMine)
        {
            gameObject.GetComponent<DoSkill>().DoClearJob();
            gameObject.GetComponent<DoSkill>().DestroyClean();
            gameObject.GetComponent<DestroyScript>().Destroyself();
        }
        if(currentHP > maxHP && photonView.isMine)
        {
            currentHP = maxHP;
        }
	}

    private void OnDestroy()
    {
        if (!gameObject.CompareTag("Player"))
            return;
        GameObject[] PlayersLeft = GameObject.FindGameObjectsWithTag("Player");
        if (PhotonNetwork.isMasterClient && PlayersLeft.Length <= 1)
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
        if (boost)
        {
            float boostvalue = Mathf.Min(boostmax - boostnow, damage / 2);
            currentHP += boostvalue;
            boostnow += boostvalue;
            GetComponent<MoveScript>().movespeed += boostvalue / 50;
        }
    }

    public void boostend()
    {
        boost = false;
        GetComponent<MoveScript>().movespeed -= boostnow / 50;
        boostnow = 0;
    }

    public void booststart()
    {
        boostend();
        boost = true;
    }

    /*
    public void GetKicked(Vector2 force)
    {
        photonView.RPC("DoGetKicked", PhotonTargets.All, force);
    }

    [PunRPC]
    void DoGetKicked(Vector2 force)
    {
        GetComponent<Rigidbody2D>().AddForce(force);
    }*/

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(maxHP);
            stream.SendNext(currentHP);
        }
        else
        {
            maxHP = (float)stream.ReceiveNext();
            currentHP = (float)stream.ReceiveNext();
        }
    }
}
