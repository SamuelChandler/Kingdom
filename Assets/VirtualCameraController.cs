using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{

    public static VirtualCameraController instance;

    CinemachineVirtualCamera Vcamera;
    CinemachineConfiner confider;

    [SerializeField] PolygonCollider2D _boundingShape;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Vcamera = GetComponent<CinemachineVirtualCamera>();
        confider = GetComponent<CinemachineConfiner>();
        
    }

    public void SetTarget(Transform target){

        if(target == null){
            Debug.Log("Targeting Player Now");
            Vcamera.Follow = Player.instance.transform;
            confider.m_BoundingShape2D = _boundingShape;
            return;
        }

        Vcamera.Follow = target;
        confider.m_BoundingShape2D = null;
    }
}
