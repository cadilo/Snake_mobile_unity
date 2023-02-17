using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public GameObject LoadIcon;
    float smooth = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        LoadIcon.transform.Rotate(0.0f, 0.0f, smooth);
    }
    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("MultiplayerMenu");
    }
}
