using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class TestBorn : Photon.MonoBehaviour
{

    float waittime = 15f;
    float diameter;
    float speed = 1f;
    GameObject safeground;

	// Use this for initialization
	void Start ()
    {
        GameObject localPlayer = PhotonNetwork.Instantiate("PlayerCircle", Random.insideUnitCircle * 7, Quaternion.identity, 0);
        localPlayer.GetComponent<DoSkill>().enabled = true;
        localPlayer.GetComponent<MoveScript>().enabled = true;
        //localPlayer.GetComponent<LeftButtonSkill>().enabled = true;
        localPlayer.transform.Find("Markicon").GetComponent<SpriteRenderer>().color = Color.red;
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
