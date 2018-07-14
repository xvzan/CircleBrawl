using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class LobbyPing : Photon.MonoBehaviour
{
    
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 5, 200, 30), PhotonNetwork.networkingPeer.RoundTripTime+"ms");
    }
}
