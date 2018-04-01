using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class DoSkill : Photon.MonoBehaviour
{
    public static int singing;
    public delegate void PointSkill(Vector2 actionplace);
    public PointSkill Fire;

    public void justdoit()
    {
        photonView.RPC("realdoit", PhotonTargets.All);
    }

    [PunRPC]
    public void realdoit()
    {
        if (Fire == null)
            return;
        else
        {
            Fire(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    // Use this for initialization
    void Start ()
    {
        Fire = null;
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
            Fire = null;
        }
        if (Input.GetButtonDown("Fire1") && gameObject.GetComponent<TestSkill01>().skillavaliable)
        {
            singing = 0;
            Fire = gameObject.GetComponent<TestSkill01>().Skill;
        }
        if (Input.GetButtonDown("Fire2") && gameObject.GetComponent<TestSkill02>().skillavaliable)
        {
            singing = 0;
            Fire = gameObject.GetComponent<TestSkill02>().Skill;
        }
        if (Input.GetButtonDown("Fire3") && gameObject.GetComponent<SelfExplodeScript>().skillavaliable)
        {
            gameObject.GetComponent<TestSkill03>().Skill();
        }
        if (Input.GetButtonDown("Fire4") && gameObject.GetComponent<TestSkillLeech>().skillavaliable)
        {
            singing = 0;
            Fire = gameObject.GetComponent<TestSkillLeech>().Skill;
        }
        if (Input.GetButtonDown("Fire5") && gameObject.GetComponent<TestSkillLightning>().skillavaliable)
        {
            singing = 0;
            Fire = gameObject.GetComponent<TestSkillLightning>().Skill;
        }
    }
    
}
