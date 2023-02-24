using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    
    [SerializeField] private Tilemap tileMap;

    //[SerializeField] private List<TileBase> tiles;
    [SerializeField] private List<TileBase> dirtVariations;
    [SerializeField] private List<TileBase> grassVariations;
    [SerializeField] private List<TileBase> grassBorders;
    
    [SerializeField] private TileBase notHandledTile;

    [Header("Configuration")]
    [SerializeField] private int size = 40;
    [Range(1, 100)] [SerializeField] private float scale;
    [Range(1, 5)] [SerializeField] private int octaves;
    [Range(0, 1)] [SerializeField] private float persistence;
    [Range(1, 10)] [SerializeField] private float lacunarity;

    private static System.Random rdn = new System.Random();
    void Start()
    {
        var seed = UnityEngine.Random.Range(-10000, 10000);
        var offset = size / 2;
        var noiseMap = Noise.GenerateNoiseMap(size, size, seed, scale, octaves, persistence, lacunarity, new Vector2(0,0));
        var grassMap = BooleanMap(noiseMap);
        grassMap = FillGaps(grassMap);
        for (int i = 1; i < size - 1; i++) {
            for (int j = 1; j < size - 1; j++) {
                var tile = ChooseTile(grassMap, i, j);
                tileMap.SetTile(new Vector3Int(i-offset,j-offset,0), tile);
            }
        }
        tileMap.gameObject.SetActive(true);
    }

    private TileBase ChooseTile(bool[,] grassMap, int i, int j)
    {
        // 001  002  004
        // 128  xxx  008  -> + i     ^ + j
        // 064  032  016
        if (grassMap[i,j]) return grassVariations[rdn.Next(0, grassVariations.Count)];
        var code4 = TileCode4Connected(grassMap, i, j);

        if (code4 == 2) return grassBorders[1];
        if (code4 == 8) return grassBorders[3];
        if (code4 == 32) return grassBorders[5];
        if (code4 == 128) return grassBorders[7];
        
        if (code4 == 130) return grassBorders[11];
        if (code4 == 10) return grassBorders[10];
        if (code4 == 160) return grassBorders[9];
        if (code4 == 40) return grassBorders[8];

        var code8 = TileCode8Connected(grassMap, i, j, code4);

        if (code8 == 0) return dirtVariations[rdn.Next(0, dirtVariations.Count)];
        if (code8 == 1) return grassBorders[0];
        if (code8 == 4) return grassBorders[2];
        if (code8 == 16) return grassBorders[4];
        if (code8 == 64) return grassBorders[6];

        if (code8 == 238) return grassBorders[12];
        if (code8 == 187) return grassBorders[13];
        
        if (code8 == 5) return grassBorders[1];
        if (code8 == 20) return grassBorders[3];
        if (code8 == 80) return grassBorders[5];
        if (code8 == 65) return grassBorders[7];
        
        // Dirt block with only 2 corner grass tiles
        if (code8 == 68) return grassBorders[12];
        if (code8 == 17) return grassBorders[13];

        if (code4 != 0) {
            if (TileSurrounded4Connected(grassMap, i, j) >= 3) return grassVariations[rdn.Next(0, grassVariations.Count)];
        }

        return notHandledTile;
    }

    private bool[,] BooleanMap(float[,] map)
    {
        var booleanMap = new bool[map.GetLength(0),map.GetLength(1)];
        for (int i = 0; i < map.GetLength(0); i++){
            for (int j = 0; j < map.GetLength(1); j++){
                booleanMap[i,j] = map[i,j] > 0.5;
            }
        }
        return booleanMap;
    }
    private bool[,] FillGaps(bool[,] map)
    {
        for (int i = 1; i < map.GetLength(0) - 1; i++) {
            for (int j = 1; j < map.GetLength(1) - 1; j++) {
                if (map[i,j] == true) continue;
                if (map[i-1,j] && map[i+1,j]) map[i,j] = true;
                else if (map[i,j-1] && map[i,j+1]) map[i,j] = true;
            }
        }
        return map;
    }

    private int TileCode4Connected(bool[,] map, int i, int j){
        var code = 0;

                                       if (map[i,j-1]) code += 2;
        if (map[i+1,j])  code += 128;                              if (map[i-1,j])   code += 8;
                                       if (map[i,j+1]) code += 32;

        return code;
    }
    private int TileCode8Connected(bool[,] map, int i, int j, int connected4){
        var code = connected4;

        if (map[i+1,j-1]) code += 1;                               if (map[i-1,j-1]) code += 4;
        
        if (map[i+1,j+1]) code += 64;                              if (map[i-1,j+1]) code += 16;

        return code;
    }
    private int TileSurrounded4Connected(bool[,] map, int i, int j){
        var count = 0;

                                    if (map[i,j-1]) count++;
        if (map[i+1,j])  count++;                                if (map[i-1,j])   count++;
                                    if (map[i,j+1]) count++;

        return count;
    }
}
