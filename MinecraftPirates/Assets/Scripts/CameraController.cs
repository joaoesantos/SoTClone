using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private int height;

    [SerializeField] private float distance;

    [SerializeField] private float verticalCameraSpeed;
    [SerializeField] private float horizontalCameraSpeed;

    private float m_RotX;

    private float m_RotY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player)
        {
            return;
        }

        var eulerAngles = player.eulerAngles;

        m_RotX -= Input.GetAxis("Mouse Y") * verticalCameraSpeed;
        m_RotY += Input.GetAxis("Mouse X") * horizontalCameraSpeed;
        m_RotX = Mathf.Clamp(m_RotX, -10, 90);
        Quaternion rotation = Quaternion.Euler(m_RotX, m_RotY, 0);
        transform.rotation = rotation;
        player.rotation = Quaternion.Euler(eulerAngles.x, m_RotY, 0);
        
        transform.position = 
            player.position - 
            (rotation * Vector3.forward * distance + new Vector3(0, -height, 0));
    }
    
}
