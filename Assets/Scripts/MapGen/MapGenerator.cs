using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileBase tile1;
    [SerializeField] private TileBase tile2;
    [SerializeField] private int size = 40;


    [SerializeField] private int seed;
    [SerializeField] private float scale;
    [SerializeField] private int octaves;
    [SerializeField] private float persistence;
    [SerializeField] private float lacunarity;
    void Start()
    {
        var offset = size / 2;
        var noiseMap = Noise.GenerateNoiseMap(size, size, seed, scale, octaves, persistence, lacunarity, new Vector2(0,0));
        for (int i = -offset; i < size - offset; i++) {
            for (int j = -offset; j < size - offset; j++) {
                var tile = noiseMap[i+offset,j+offset] > 0.5 ? tile1 : tile2;
                tileMap.SetTile(new Vector3Int(i,j,0), tile);
            }
        }
    }

    void Update()
    {
        
    }
}
