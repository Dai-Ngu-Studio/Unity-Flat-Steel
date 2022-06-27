using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class StaticScript : MonoBehaviourPun
{
    public static StaticScript Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    public int playerDeadCount = 0;
    public int MyRank;
    public bool isDead = false;
    
    public Canvas gameResultCanvas;

    private PhotonView view;

    
    
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        view = GetComponent<PhotonView>();
        
        // HideOrShowGameOverCanvas(false);
    }

    // Update is called once per frame
    public void HideOrShowGameOverCanvas(bool visible)
    {
        
        gameResultCanvas.gameObject.SetActive(visible);
        if (visible && isDead == false)
        {
            TextMeshProUGUI totalPlayer = gameResultCanvas.transform.Find("PanelMain/PanelMyPosition/TextTotalPlayer").GetComponent<TextMeshProUGUI>();
            totalPlayer.SetText("/" + PhotonNetwork.CurrentRoom.PlayerCount);
            
            TextMeshProUGUI myPosition = gameResultCanvas.transform.Find("PanelMain/PanelMyPosition/TextMyPosition").GetComponent<TextMeshProUGUI>();
            myPosition.SetText("#"+MyRank);
        }          

    }
    public int IncreaseDeadCount()
    {
        view.RPC("IncreaseDeadCountRPC", RpcTarget.AllBuffered);
        return PhotonNetwork.CurrentRoom.PlayerCount - playerDeadCount;
    }
    [PunRPC]
    public void IncreaseDeadCountRPC()
    {
        playerDeadCount++;
        if (IsWonTop1())
        {
            MyRank = 1;
            HideOrShowGameOverCanvas(true);
        }
    }
    public void CameraFollower(Transform playerTransform)
    {
        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if (brain == null)
        {
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }

        cinemachineVirtualCamera.Follow = playerTransform;
        cinemachineVirtualCamera.LookAt = playerTransform;
    }

    public bool IsWonTop1()
    {
        return PhotonNetwork.CurrentRoom.PlayerCount - playerDeadCount == 1;
    }
    public void ChangeOrthoSize(float orthographicSize)
    {
        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }
}