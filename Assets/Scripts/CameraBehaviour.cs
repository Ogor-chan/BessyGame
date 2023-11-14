using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class CameraPoint
    {
        public GameObject GO;
        public Transform TargetLockOn;
        public float ZoomSpeed;
        public float SmoothTime;
        public float ZoomSize;
    }
    public enum CameraState
    {
        Following,
        LockedOn
    }

    public List<CameraPoint> CameraPointList = new List<CameraPoint>();
    public CameraState CurrentCameraState;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;
    private CameraPoint CurrentPoint;
    private GameObject Player;
    [SerializeField] int Zoom;
    [SerializeField] float ZoomSpeed;


    private void Awake()
    {
        mainCamera = Camera.main;
        Player = GameObject.Find("Player");
        CurrentCameraState = CameraState.Following;
    }


    public void PlayerCollision(Collider2D collision)
    {
        if (collision.tag == "CameraPoint")
        {
            print("IsCamera");
            CurrentCameraState = CameraState.LockedOn;
            CurrentPoint = WhichPoint(collision.gameObject);
        }
    }

    public void PlayerLeaveCollision(Collider2D collision)
    {
        if (collision.tag == "CameraPoint")
        {
            print("LeaveTrigger");
            CurrentCameraState = CameraState.Following;
        }
    }


    private void Update()
    {
        if(CurrentCameraState == CameraState.LockedOn)
        {
            Vector3 targetPosition = new Vector3(CurrentPoint.GO.transform.position.x, CurrentPoint.GO.transform.position.y,
    mainCamera.transform.position.z);
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition,
                ref velocity, CurrentPoint.SmoothTime);

            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize,
                CurrentPoint.ZoomSize, Time.deltaTime * CurrentPoint.ZoomSpeed);
        }
        else if (CurrentCameraState == CameraState.Following)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize,
                Zoom, Time.deltaTime * ZoomSpeed);
            Vector3 targetPosition = new Vector3(Player.transform.position.x, Player.transform.position.y,
mainCamera.transform.position.z);
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition,
    ref velocity, 0.5f);
        }
    }

    public CameraPoint WhichPoint(GameObject ThisPoint)
    {
        foreach (var item in CameraPointList)
        {
            if (item.GO == ThisPoint)
            {
                return item;
            }
        }
        return null;
    }



}
