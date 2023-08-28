using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    #region Unity Methods
    public GameObject ConnectOptionsPanelGameobject;
    public GameObject ConnectWithNamePanelGameobject;

    // Start is called before the first frame update
    void Start()
    {
        ConnectOptionsPanelGameobject.SetActive(true);
        ConnectWithNamePanelGameobject.SetActive(false);
    }

    #endregion
}
