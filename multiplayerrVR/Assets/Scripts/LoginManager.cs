using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName_InputName;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region UI Callback Methods
    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        
        if (PlayerName_InputName !=null)
        {
           PhotonNetwork.NickName= PlayerName_InputName.text;
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Server is called, Nickname is available!");
        }
    }
    #endregion

    #region Photon Callbacks Method
    public override void OnConnected()
    {
        Debug.Log("Server is called, Photon is available!");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Masters Server: " + PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel("HomeScene");
    
    }
    #endregion
}
