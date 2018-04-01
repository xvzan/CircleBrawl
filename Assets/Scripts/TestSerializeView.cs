using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TestSerializeView : Photon.MonoBehaviour
{
    //Rigidbody2D SelfRb2d;
    Vector3 correctPlayerPos;

    // Use this for initialization
    void Start () {
        //SelfRb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.fixedDeltaTime * 10);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            correctPlayerPos = (Vector3)stream.ReceiveNext();
        }
    }
}
