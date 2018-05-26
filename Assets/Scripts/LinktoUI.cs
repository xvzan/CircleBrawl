using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class LinktoUI : Photon.MonoBehaviour
{
    public GameObject MyUI;

    public void ShutUp()
    {
        photonView.RPC("SSUP", PhotonTargets.All);
    }

    [PunRPC]
    void SSUP()
    {
        if (!photonView.isMine)
            return;
        GetComponent<DoSkill>().FireReset();
        MyUI.GetComponent<MainSkillMenu>().enabled = false;
        MyUI.GetComponent<MainSkillMenu>().CloseMainSkillMenu();
        MyUI.GetComponent<SetSkillC>().AllCOff();
        MyUI.GetComponent<SetSkillD>().AllDOff();
        MyUI.GetComponent<SetSkillE>().AllEOff();
        //MyUI.GetComponent<SetSkillF>().AllFOff();
        MyUI.GetComponent<SetSkillG>().AllGOff();
        MyUI.GetComponent<SetSkillR>().AllROff();
        MyUI.GetComponent<SetSkillT>().AllTOff();
        MyUI.GetComponent<SetSkillY>().AllYOff();
        StartCoroutine(Sback());
    }

    IEnumerator Sback()
    {
        yield return new WaitForSeconds(1f);
        MyUI.GetComponent<SkillsLink>().alphaset();
        MyUI.GetComponent<MainSkillMenu>().enabled = true;
    }
}
