using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Steamworks;
using System.Text;

public class TestMenu00 : Photon.PunBehaviour
{
    //public Text NickName;
    public GameObject JoinButton;
    public GameObject Menu01;
    public GameObject Menu02;
    HAuthTicket hAuthTicket;

    public void SwtichToMenu01()
    {
        gameObject.SetActive(false);
        Menu02.SetActive(false);
        Menu01.SetActive(true);
    }
    
    // Use this for initialization
    void Start()
    {
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Steam;
        string SteamAuthSessionTicket = GetSteamAuthTicket(out hAuthTicket);
        PhotonNetwork.AuthValues.AddAuthParameter("ticket", SteamAuthSessionTicket);
        PhotonNetwork.ConnectUsingSettings("0.1.4s.0p");
    }

    public void ClickJoinLobbyButton()
    {
        //PlayerPrefs.SetString("nickname", PhotonNetwork.player.NickName);
        SwtichToMenu01();
    }

    public void ClickQuitButton()
    {
        SteamAPI.Shutdown();
        Application.Quit();
    }

    public string GetSteamAuthTicket(out HAuthTicket hAuthTicket)
    {
        byte[] ticketByteArray = new byte[1024];
        uint ticketSize;
        hAuthTicket = SteamUser.GetAuthSessionTicket(ticketByteArray, ticketByteArray.Length, out ticketSize);
        System.Array.Resize(ref ticketByteArray, (int)ticketSize);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < ticketSize; i++)
        {
            sb.AppendFormat("{0:x2}", ticketByteArray[i]);
        }
        return sb.ToString();
    }

    public override void OnConnectedToPhoton()
    {
        SteamUser.CancelAuthTicket(hAuthTicket);
        JoinButton.SetActive(true);
        PhotonNetwork.player.NickName = SteamFriends.GetPersonaName();
        ClickJoinLobbyButton();
    }
}