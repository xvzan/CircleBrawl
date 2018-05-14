using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TestBorn : Photon.MonoBehaviour
{

    float waittime = 10f;
    float diameter;
    float speed = 1f;
    GameObject safeground;
    public GameObject MyUI;

	// Use this for initialization
	void Start ()
    {
        //PhotonNetwork.automaticallySyncScene = false;
        GameObject localPlayer = PhotonNetwork.Instantiate("PlayerCircle", Random.insideUnitCircle * 7, Quaternion.identity, 0);
        MyUI.GetComponent<SkillsLink>().mySoldier = localPlayer;
        MyUI.GetComponent<SkillsLink>().linktome();
        localPlayer.GetComponent<DoSkill>().enabled = true;
        localPlayer.GetComponent<MoveScript>().enabled = true;
        MyUI.GetComponent<SkillsLink>().alphaset();
        localPlayer.GetComponent<SpriteRenderer>().color = new Color32(0, 191, 0, 255);
        safeground = GameObject.Find("GroundCircle");
        diameter = safeground.transform.lossyScale.x;
        StartCoroutine(Changeradius());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Changeradius()
    {
        while (diameter > 1.5 * speed)
        {
            diameter -= speed;
            Vector3 nextscale = new Vector3(diameter, diameter, 1);
            yield return new WaitForSeconds(waittime);
            ShrinkGround(nextscale);
        }
    }

    void ShrinkGround(Vector3 scale)
    {
        if (!PhotonNetwork.isMasterClient)
            return;
        photonView.RPC("DoShrink", PhotonTargets.All, scale);
    }

    [PunRPC]
    void DoShrink(Vector3 scale)
    {
        safeground.transform.localScale = scale;
    }
}
