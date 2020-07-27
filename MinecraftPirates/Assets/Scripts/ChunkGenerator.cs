using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

[RequireComponent(typeof(MeshFilter))]
public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private int chunkSize;
    [SerializeField] private float frequency;
    [SerializeField] private float scale;
    private Vector3[] m_Vertices;
    private int[] m_Triangles;
    private Mesh m_Mesh;

    private void Awake()
    {
        Assert.IsFalse(chunkSize == 0);
        Assert.IsFalse(frequency == 0);
        Assert.IsFalse(scale == 0);
    }

    void Start()
    {
        m_Mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = m_Mesh;
        CreateShape();
        UpdateMesh();
    }

    private void Update()
    {
        /*CreateShape();
        UpdateMesh();*/
    }


    private void CreateShape()
    {
       m_Vertices = new Vector3[(int) Math.Pow(chunkSize + 1, 2)];

       for (int i = 0, z = 0; z <= chunkSize; z++)
       {
           for (int x = 0; x <= chunkSize; x++)
           {
               float y = Mathf.PerlinNoise(x * scale + frequency, z * scale + frequency);
               m_Vertices[i] = new Vector3(x, y, z);
               i++;
           }
       }

       List<int> tempTri = new List<int>();
       for (int i = 0; i < m_Vertices.Length - chunkSize - 1; i++)
       {
           int vertX = (int) m_Vertices[i].x;
           if (vertX < chunkSize)
           {
               tempTri.AddRange(new List<int>()
               {
                   i , i + chunkSize + 1, i + 1
               });
           }

           if (vertX > 0)
           {
               tempTri.AddRange(new List<int>()
               {
                   i , i + chunkSize, i + chunkSize + 1
               });
           }
       }
       m_Triangles = tempTri.ToArray();
    }

    private void UpdateMesh()
    {
        m_Mesh.Clear();
        m_Mesh.vertices = m_Vertices;
        m_Mesh.triangles = m_Triangles;
        
        m_Mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (m_Vertices != null)
        {
            for (int i = 0; i < m_Vertices.Length; i++)
            {
                Gizmos.DrawSphere(m_Vertices[i], 0.1f);
            }
        }
        
    }
}
