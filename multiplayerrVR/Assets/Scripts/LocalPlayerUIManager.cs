using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalPlayerUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject GoHomeButton;
    // Start is called before the first frame update
    void Start()
    {
        GoHomeButton.GetComponent<Button>().onClick.AddListener(VirtualWorldManager.Instance.leaveRoomAndLoadHomeScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
