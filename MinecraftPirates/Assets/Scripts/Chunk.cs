using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using Noise;

[RequireComponent(typeof(MeshFilter))]
public class Chunk : MonoBehaviour
{
    private MeshCollider m_ChunkCollider;
    private int m_ChunkSize;
    private float m_Frequency;
    private float m_Scale;
    private Vector3[] m_Vertices;
    private int[] m_Triangles;
    private Mesh m_Mesh;
    private int m_InitialX;
    private int m_InitialZ;

    public int ChunkSize
    {
        get => m_ChunkSize;
        set => m_ChunkSize = value;
    }

    public float Frequency
    {
        get => m_Frequency;
        set => m_Frequency = value;
    }

    public float Scale
    {
        get => m_Scale;
        set => m_Scale = value;
    }

    public int InitialX
    {
        get => m_InitialX;
        set => m_InitialX = value;
    }

    public int InitialZ
    {
        get => m_InitialZ;
        set => m_InitialZ = value;
    }


    void Start()
    {
        m_Mesh = new Mesh();
        m_ChunkCollider = GetComponent<MeshCollider>();
        GetComponent<MeshFilter>().mesh = m_Mesh;
        CreateShape();
        UpdateMesh();
    }
    
    private void CreateShape()
    {
       m_Vertices = new Vector3[(int) Math.Pow(m_ChunkSize + 1, 2)];
       for (int i = 0, z = 0; z <= m_ChunkSize; z++)
       {
           for (int x = 0; x <= m_ChunkSize; x++)
           {
               float y = Mathf.PerlinNoise((x + m_InitialX) * m_Scale + m_Frequency, (z + m_InitialZ) * m_Scale + m_Frequency);
               //y += Mathf.PerlinNoise(x * m_Scale + 0.5f, z * m_Scale + 0.5f);
               //float y = Noise.Noise.GetNoise((double)x / m_Scale, 0.0, (double) z / m_Scale);
               m_Vertices[i] = new Vector3(x + m_InitialX, y, z + m_InitialZ);
               i++;
           }
       }

       List<int> tempTri = new List<int>();
       for (int i = 0; i < m_Vertices.Length - m_ChunkSize - 1; i++)
       {
           int vertX = (int) m_Vertices[i].x;
           if (vertX < m_InitialX + m_ChunkSize)
           {
               tempTri.AddRange(new List<int>()
               {
                   i , i + m_ChunkSize + 1, i + 1
               });
           }

           if (vertX > m_InitialX)
           {
               tempTri.AddRange(new List<int>()
               {
                   i , i + m_ChunkSize, i + m_ChunkSize + 1
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
        
        m_ChunkCollider.sharedMesh = m_Mesh;
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
