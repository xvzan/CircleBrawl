using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class DoSkill : Photon.MonoBehaviour
{
    public static int singing;
    public delegate void PointSkill(Vector2 actionplace);
    public PointSkill Fire;

    // Use this for initialization
    void Start ()
    {
        Fire = donothing;
    }

    // Update is called once per frame
    void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			justdoit();
		}
        if (Input.GetButtonDown("Stop"))
        {
            gameObject.GetComponent<MoveScript>().stopwalking();
            singing = 0;
            FireReset();
        }
    }

    public void justdoit()
    {
        //photonView.RPC("realdoit", PhotonTargets.All);
        if (!photonView.isMine)
            return;
        Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        FireReset();
    }

    /*
    [PunRPC]
    public void realdoit()
    {
        Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    */

    public void FireReset()
    {
        Fire = null;
    }

    void donothing(Vector2 vector)
    {

    }
}
