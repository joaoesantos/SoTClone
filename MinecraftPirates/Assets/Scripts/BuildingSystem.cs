using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : Singleton<BuildingSystem>
{
    public enum BuildingType
    {
        HULL,
        WALL,
        CANNON_WALL
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class Building : MonoBehaviour
{
    public Transform Transform { get; set; }
    public BuildingSystem.BuildingType Type { get; set; }
}