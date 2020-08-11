using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : Singleton<World>
{
    
    [SerializeField] private GameObject chunk;
    [SerializeField] private byte[,,] worldData;
    
    [SerializeField] private int worldX = 32;

    [SerializeField] private int worldY = 16;

    [SerializeField] private int worldZ = 32;
    [SerializeField] private int chunkSize = 16;

    private List<Building> m_WorldBuldings;

    // Start is called before the first frame update
    void Start()
    {
        var xChunks = worldX / chunkSize;
        var zChunks = worldZ / chunkSize;

        for (int x = 0; x < xChunks; x++)
        {
            for (int z = 0; z < zChunks; z++)
            {
                GameObject newChunk = Instantiate(
                    chunk,
                    new Vector3(0, 0, 0),
                    new Quaternion(0, 0, 0, 0)
                );

                Chunk chunkComponent = newChunk.GetComponent<Chunk>();
                chunkComponent.Frequency = 0.5f;
                chunkComponent.Scale = 0.3f;
                chunkComponent.ChunkSize = chunkSize;
                chunkComponent.InitialX = x * chunkSize;
                chunkComponent.InitialZ = z * chunkSize;
            }
        }
    }

    public void AddBuilding(Building building)
    {
        m_WorldBuldings.Add(building);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
