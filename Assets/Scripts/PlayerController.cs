using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();
    private PhotonView view;

    private void Start()
    {
        mainCamera = Camera.main;
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            GetBodyMovement();
            GetTurretMovement();
            GetShootingInput();
        }
    }

    private void GetShootingInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnShoot?.Invoke();
        }
    }

    private void GetTurretMovement()
    {
        OnMoveTurret?.Invoke(GetMousePositon());
    }

    private Vector2 GetMousePositon()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition;
    }

    private void GetBodyMovement()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);

        // get current user id; get the tank gameobject and get the transform view of the tank
        var userid = PhotonNetwork.AuthValues.UserId;
        var tankObject = gameObject.transform.GetChild(0);
        var transformView = tankObject.GetComponent<PhotonTransformViewClassic>();
        var damageable = tankObject.GetComponent<Damagable>();

        // debug stuff ; safe to remove
        //Debug.Log($"{view.ViewID} at {transformView.transform.position.x}");
        //Debug.Log($"{view.ViewID} at {transformView.transform.position.y}");

        // get custom properties of room; tryAdd to add key if not existed; then set value for key
        var customProps = PhotonNetwork.CurrentRoom.CustomProperties;
        customProps.TryAdd(userid, new Dictionary<string, float>());
        customProps[userid] = new Dictionary<string, float>() {
            { "x", transformView.transform.position.x },
            { "y", transformView.transform.position.y },
            { "qx", transformView.transform.rotation.x },
            { "qy", transformView.transform.rotation.y },
            { "qz", transformView.transform.rotation.z },
            { "qw", transformView.transform.rotation.w },
            { "h", damageable.Health },
        };

        // update custom properties of room (will update to other clients)
        // (slight race condition, last client to move will be the one to update the custom properties)
        PhotonNetwork.CurrentRoom.SetCustomProperties(customProps);
    }
}