using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private int jumpHeight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        
        transform.Translate(horizontalMovement,0, verticalMovement);
        if (BuildingSystem.Instance.IsBuilding)
        {
            if (Input.GetKeyDown(KeyCode.R)) //rotate
            {
                BuildingSystem.Instance.RotateObject();
            }

            if (Input.GetKeyDown(KeyCode.G)) // cancel button
            {
                BuildingSystem.Instance.CancelBuild();
            }

            if (Input.GetMouseButtonDown(0))
            {
                BuildingSystem.Instance.PlaceObject();
            }
        }
        
        if (Input.GetMouseButtonDown(0)) //start build
        {
            BuildingSystem.Instance.StartBuilding(transform, BuildingSystem.BuildingType.HULL);
        }
    }
}
