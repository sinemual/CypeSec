using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileData : MonoBehaviour
{
    public Vector2 pos;
    public enum TileType { Wall, SWall, Ground };
    public enum Dirs { N = 0, NE, E, SE, S, SW, W, NW };
    public TileType tileType;
    public TileData[] neighbors = new TileData[8];
    public bool isResource;
    public bool isBuild;

    public bool isBusy;

    public void SetTileType(TileType _tileType, bool _isBusy = false)
    {
        if (tileType == TileType.Ground && _tileType == TileType.SWall)
        {
            DungeonGenerator.Instance.grounds.Remove(this);
            DungeonGenerator.Instance.sWalls.Add(this);
            GetComponent<SpriteRenderer>().sortingLayerName = "Walls";
        }
        else if ((tileType == TileType.SWall || tileType == TileType.Wall) && _tileType == TileType.Ground)
        {
            DungeonGenerator.Instance.grounds.Add(this);
            DungeonGenerator.Instance.sWalls.Remove(this);
            GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
        }
        isBusy = _isBusy;
        tileType = _tileType;
    }

    public void SetNeighbors(DungeonGenerator _dung, int _x, int _y)
    {
        neighbors[0] = _dung.dungTilesData[_x, _y + 1];
        neighbors[1] = _dung.dungTilesData[_x + 1, _y + 1];
        neighbors[2] = _dung.dungTilesData[_x + 1, _y];
        neighbors[3] = _dung.dungTilesData[_x + 1, _y - 1];
        neighbors[4] = _dung.dungTilesData[_x, _y - 1];
        neighbors[5] = _dung.dungTilesData[_x - 1, _y - 1];
        neighbors[6] = _dung.dungTilesData[_x - 1, _y];
        neighbors[7] = _dung.dungTilesData[_x - 1, _y + 1];
    }
}
