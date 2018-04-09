using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class TestMenu02 : Photon.PunBehaviour
{
    public GameObject Startbutton;
    public GameObject Masterpanel;
    public GameObject Otherpanel;
    public Toggle Readytoggle;
    public Toggle AutostartToggle;

    public Text PlayersJoined;
    public Text Notready;
    public Text Roomname;

    public GameObject Menu00;
    public GameObject Menu01;

    bool isready = false;

    public override void OnJoinedRoom()
    {
        setreadystatusonline();
        showpanel();
        Roomname.text = PhotonNetwork.room.Name;
    }
    
    /*
    private void OnEnable()
    {
        //setreadystatusonline();
        //showpanel();
    }
    */

    public void SwtichToMenu01()
    {
        Menu00.SetActive(false);
        gameObject.SetActive(false);
        Menu01.SetActive(true);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Updatetext();
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Updatetext();
    }

    public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        showpanel();
    }

    public void showmasterpanel()
    {
        Otherpanel.SetActive(false);
        Masterpanel.SetActive(true);
        isready = true;
        setreadystatusonline();
    }

    public void showotherpanel()
    {
        Masterpanel.SetActive(false);
        Otherpanel.SetActive(true);
        isready = Readytoggle.isOn;
        setreadystatusonline();
    }

    public void OnReadyToggleChanged()
    {
        isready = Readytoggle.isOn;
        setreadystatusonline();
    }

    public void setreadystatusonline()
    {
        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {
        {"ready", isready }
        });
        photonView.RPC("Updatetext", PhotonTargets.All);
    }

    public void ClickStartButton()
    {
        photonView.RPC("EnterCircleSence", PhotonTargets.All);
    }

    public void ClickBackButton()
    {
        PhotonNetwork.LeaveRoom();
        SwtichToMenu01();
    }

    public void showpanel()
    {
        if (PhotonNetwork.isMasterClient)
            showmasterpanel();
        else
            showotherpanel();
    }

    [PunRPC]
    public void EnterCircleSence()
    {
        PhotonNetwork.LoadLevel("Sences/CircleSence");
    }

    [PunRPC]
    public void Updatetext()
    {
        PlayersJoined.text = PhotonNetwork.playerList.Length + " players joined";
        int i = 0;
        foreach (PhotonPlayer pp in PhotonNetwork.playerList)
        {
            if ((bool)pp.CustomProperties["ready"] == false)
                i++;
        }
        Notready.text = i + " not ready";
        if (PhotonNetwork.isMasterClient)
        {
            if (i == 0)
            {
                Startbutton.SetActive(true);
                if (AutostartToggle.isOn && PhotonNetwork.playerList.Length > 1)
                    ClickStartButton();
            }
            else
                Startbutton.SetActive(false);
        }
    }
}
