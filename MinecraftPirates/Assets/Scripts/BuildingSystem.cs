using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : Singleton<BuildingSystem>
{
    [SerializeField] private GameObject[] placeableObjects;
    public enum BuildingType
    {
        HULL,
        WALL,
        CANNON_WALL
    }

    public bool IsBuilding { get; private set; }
    private Preview m_Preview;
    private GameObject m_ObjectToPreview;
    private Transform m_RayCastOrigin;

    // Update is called once per frame
    void Update()
    {
        if (IsBuilding)
        {
            // use ray cast
            CalculateRayCast();
        }
    }

    public void StartBuilding(Transform player, BuildingType buildingType)
    {
        m_ObjectToPreview = Instantiate(placeableObjects[buildingType.GetHashCode()], Vector3.zero, Quaternion.identity);
        m_Preview = m_ObjectToPreview.GetComponent<Preview>();
        m_RayCastOrigin = player;
        IsBuilding = true;

    }

    public void PlaceObject()
    {
        m_Preview.Place();
        IsBuilding = false;
    }

    public void CancelBuild()
    {
        Destroy(m_ObjectToPreview);
        IsBuilding = false;
    }

    private void CalculateRayCast()
    {
        Ray ray = new Ray(m_RayCastOrigin.transform.position, m_RayCastOrigin.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            m_ObjectToPreview.transform.position = hit.point;
        }
    }


    public void RotateObject()
    {
        m_Preview.Rotate();
    }
}
