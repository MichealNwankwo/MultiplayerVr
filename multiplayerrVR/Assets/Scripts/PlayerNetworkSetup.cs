using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    public GameObject LocalXRRigGameobject;
    public GameObject MainAvatarGameObject;


    public GameObject AvatarHeadGameobject;
    public GameObject AvatarBodyGameobject;


    public GameObject[] AvatarModelPrefabs;
    public TextMeshProUGUI PlayerName_Text;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            LocalXRRigGameobject.SetActive(true);

            //Getting avatar selection data so the right avatar can be instantiated.
            object avatarSelectionNumber;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER, out avatarSelectionNumber))
            {
                Debug.Log("Avatar Selection Number" + (int)avatarSelectionNumber);
                photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int)avatarSelectionNumber);
            }

            LocalXRRigGameobject.SetActive(true);
            SetLayerRecursively(AvatarHeadGameobject,6);
            SetLayerRecursively(AvatarBodyGameobject,7);

            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();
            if (teleportationAreas.Length>0)
            {
                Debug.Log("found" + teleportationAreas.Length + "Teleportation Area.");
                foreach (var item in teleportationAreas)
                {
                    item.teleportationProvider = LocalXRRigGameobject.GetComponent<TeleportationProvider>();
                }
            }
            MainAvatarGameObject.AddComponent<AudioListener>();
        }
        else
        {
            LocalXRRigGameobject.SetActive(false);
            SetLayerRecursively(AvatarHeadGameobject, 0);
            SetLayerRecursively(AvatarBodyGameobject, 0);
        }
        if (PlayerName_Text!= null)
        {
            PlayerName_Text.text = photonView.Owner.NickName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber)
    {
        GameObject selectedAvatarGameobject = Instantiate(AvatarModelPrefabs[avatarSelectionNumber], LocalXRRigGameobject.transform);

        AvatarInputConverter avatarInputConverter = LocalXRRigGameobject.GetComponent<AvatarInputConverter>();
        AvatarHolder avatarHolder = selectedAvatarGameobject.GetComponent<AvatarHolder>();
        SetUpAvatarGameobject(avatarHolder.HeadTransform, avatarInputConverter.AvatarHead);
        SetUpAvatarGameobject(avatarHolder.BodyTransform, avatarInputConverter.AvatarBody);
        SetUpAvatarGameobject(avatarHolder.HandLeftTransform, avatarInputConverter.AvatarHand_Left);
        SetUpAvatarGameobject(avatarHolder.HandRightTransform, avatarInputConverter.AvatarHand_Right);
    }

    void SetUpAvatarGameobject(Transform avatarModelTransform, Transform mainAvatarTransform)
    {
        avatarModelTransform.SetParent(mainAvatarTransform);
        avatarModelTransform.localPosition = Vector3.zero;
        avatarModelTransform.localRotation = Quaternion.identity;
    }
}
