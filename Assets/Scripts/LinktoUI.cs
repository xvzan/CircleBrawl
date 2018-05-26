using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class LinktoUI : Photon.MonoBehaviour
{
    public GameObject MyUI;

    void ShutUp()
    {
        photonView.RPC("SSUP", PhotonTargets.All);
    }

    [PunRPC]
    public void SSUP()
    {
        if (!photonView.isMine)
            return;
        GetComponent<DoSkill>().FireReset();
        MyUI.GetComponent<SetSkillC>().AllCOff();
        MyUI.GetComponent<SetSkillD>().AllDOff();
        MyUI.GetComponent<SetSkillE>().AllEOff();
        //MyUI.GetComponent<SetSkillF>().AllFOff();
        MyUI.GetComponent<SetSkillG>().AllGOff();
        MyUI.GetComponent<SetSkillR>().AllROff();
        MyUI.GetComponent<SetSkillT>().AllTOff();
        MyUI.GetComponent<SetSkillY>().AllYOff();
    }

    IEnumerator Sback()
    {
        yield return new WaitForSeconds(1f);
        MyUI.GetComponent<SkillsLink>().alphaset();
    }
}
