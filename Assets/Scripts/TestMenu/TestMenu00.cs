using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class TestMenu00 : Photon.PunBehaviour
{

    public Text NickName;
    public Text WelcomeMessage;
    public GameObject Menu01;
    public GameObject Menu02;
    
    public void SwtichToMenu01()
    {
        gameObject.SetActive(false);
        Menu02.SetActive(false);
        Menu01.SetActive(true);
    }
    
    // Use this for initialization
    void Start()
    {
        //PhotonNetwork.automaticallySyncScene = true;
        /*
        PhotonNetwork.ConnectUsingSettings("0.1.1");
        string userId = "u" + Random.Range(1, 9999);
        PhotonNetwork.AuthValues = new AuthenticationValues(userId);
        */
        //NickName.text = PlayerPrefs.GetString("nickname");
    }

    public void ClickJoinLobbyButton()
    {
        PhotonNetwork.ConnectUsingSettings("0.1.3t");
        if (NickName.text == "")
            PhotonNetwork.player.NickName = "Player" + Random.Range(1, 9999);
        else
            PhotonNetwork.player.NickName = NickName.text;
        //PlayerPrefs.SetString("nickname", PhotonNetwork.player.NickName);
        WelcomeMessage.text = "Welcome," + PhotonNetwork.player.NickName;
        SwtichToMenu01();
    }
}