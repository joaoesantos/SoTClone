using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private int height;

    [SerializeField] private float distance;
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
        float x = eulerAngles.x;
        float y = eulerAngles.y;
        
        Quaternion rotation = Quaternion.Euler(x, y, 0);
        
        transform.rotation = rotation;
        transform.position = 
            player.position - 
            (rotation * Vector3.forward * distance + new Vector3(0, -height, 0));
    }
}
