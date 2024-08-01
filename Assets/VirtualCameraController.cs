using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{

    float priority;

    CinemachineVirtualCamera Vcamera;
    // Start is called before the first frame update
    void Start()
    {
        Vcamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        priority = transform.position.x - Player.instance.transform.position.x; 
        Vcamera.Priority = (int)priority;
    }
}
