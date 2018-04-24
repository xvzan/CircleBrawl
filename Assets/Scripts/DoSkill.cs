﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class DoSkill : Photon.MonoBehaviour
{
    public static int singing;
    public delegate void PointSkill(Vector2 actionplace);
    public PointSkill Fire;
    public delegate void NoPointSkill();
    public NoPointSkill ClearDebuff;

    // Use this for initialization
    void Start ()
    {
        Fire = null;
        ClearDebuff = null;
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
        if (!photonView.isMine || Fire == null)
            return;
        Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        FireReset();
    }

    public void FireReset()
    {
        Fire = null;
    }

    public void DoClearJob()
    {
        ClearDebuff();
        ClearDebuff = null;
    }
}
