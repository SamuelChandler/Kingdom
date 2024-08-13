using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour, IDataPersistance
{

    public static VirtualCameraController instance;

    CinemachineVirtualCamera Vcamera;
    CinemachineConfiner confider;

    [SerializeField] PolygonCollider2D _boundingShape;

    [SerializeField] List<Transform> CameraTargets;

    int TargetIndex;
    
    void Awake()
    {
        instance = this;
        Vcamera = GetComponent<CinemachineVirtualCamera>();
        confider = GetComponent<CinemachineConfiner>();
    }

    public void SetTarget(Transform target){

        if(target == null){
            Debug.Log("Targeting Player Now");
            Vcamera.Follow = Player.instance.transform;
            confider.m_BoundingShape2D = _boundingShape;
            TargetIndex = 0;
            return;

        }else if(CameraTargets.Contains(target) == false){
            //Not in the list of Valid Camera Targets
            Debug.Log("Camera Target Added: " + target.name);
            CameraTargets.Add(target);
        }
        
        TargetIndex = CameraTargets.IndexOf(target);
        Vcamera.Follow = target;
        confider.m_BoundingShape2D = null;
    }

    public void LoadData(PlayerData playerData)
    {
        SetTarget(CameraTargets[playerData.CameraTarget]);
    }

    public void SaveData(ref PlayerData playerData)
    {
        playerData.CameraTarget = TargetIndex;
    }
}
