using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class TestPhoton : Photon.PunBehaviour {

    public GameObject Menu00;
    public GameObject Menu01;
    public GameObject Menu02;

    // Use this for initialization
    void Start ()
    {
        PhotonNetwork.automaticallySyncScene = true;
        if (!PhotonNetwork.connected)
        {
            Menu01.SetActive(false);
            Menu02.SetActive(false);
            Menu00.SetActive(true);
        }
        else if (PhotonNetwork.room.Name != null)
        {
            Menu00.SetActive(false);
            Menu01.SetActive(false);
            Menu02.SetActive(true);
        }
        else
        {
            Menu00.SetActive(false);
            Menu01.SetActive(true);
            Menu02.SetActive(false);
        }
    }
}