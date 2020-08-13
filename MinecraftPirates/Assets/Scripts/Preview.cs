using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Preview : MonoBehaviour
{
    [SerializeField] private Material goodPositionMaterial;
    [SerializeField] private Material badPositionMaterial;
    [SerializeField] private List<String> placeableTags;
    private MeshRenderer _Mesh;
    
    // Start is called before the first frame update
    void Start()
    {
        _Mesh = GetComponent<MeshRenderer>();
        _Mesh.material = badPositionMaterial;
    }
    
    public void Rotate()
    {
        transform.Rotate(0f, 45f,0f, Space.Self);
    }

    public void Place()
    {
        GameObject newGameObject = Instantiate(gameObject, transform.position, transform.rotation);
        newGameObject.GetComponent<Collider>().isTrigger = false;
        Destroy(gameObject);
    }

    private void ChangeColor()
    {
        
    }
}
