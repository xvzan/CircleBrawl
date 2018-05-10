using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class RBScript : Photon.MonoBehaviour
{
    Rigidbody2D PlayerRB2D;
    MoveScript MS;
    float timetostop;
    float timepassed;

	// Use this for initialization
	void Start () {
        PlayerRB2D = GetComponent<Rigidbody2D>();
        MS = GetComponent<MoveScript>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void GetKicked(Vector2 force)
    {
        photonView.RPC("DoGetKicked", PhotonTargets.All, force);
    }

    [PunRPC]
    void DoGetKicked(Vector2 force)
    {
        PlayerRB2D.AddForce(force);
    }

    public void GetPushed(Vector2 velo,float time)
    {
        photonView.RPC("DoGetPushed", PhotonTargets.All, velo, time);
    }

    [PunRPC]
    void DoGetPushed(Vector2 velo, float time)
    {
        MS.controllable = false;
        MS.Givenvelocity = velo;
        timepassed = 0;
        timetostop = time;
    }
    
    private void FixedUpdate()
    {
        if (MS.controllable)
            return;
        if (timepassed >= timetostop)
        {
            MS.Givenvelocity = Vector2.zero;
            MS.controllable = true;
        }
        else
            timepassed += Time.fixedDeltaTime;
    }

    public float GetRemainTime()
    {
        return timetostop - timepassed;
    }

    public void PushEnd()
    {
        MS.Givenvelocity = Vector2.zero;
        MS.controllable = true;
        //timepassed = timetostop;
    }
}
