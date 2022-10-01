using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesSpritesManager : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite[] s_sWallTiles;
    public Sprite[] s_groundTiles;

    private Sprite[] s_tempTiles;
    private TileData.TileType compareTileType;
    private TileData.TileType compareTileType2;

    public void SetSpritesForTiles(DungeonGenerator _dungeonGenerator)
    {
        s_tempTiles = new Sprite[s_sWallTiles.Length];
        for (int i = 1; i < _dungeonGenerator.spareSpaceRows - 1; i++)
            for (int j = 1; j < _dungeonGenerator.spareSpaceColumns - 1; j++)
                SetSprite(_dungeonGenerator.dungTilesData[i, j]);
    }

    public void SetSprite(TileData _tileData) // DependingOnNeighbors
    {
        //if (_tileData.tileType == TileData.TileType.SWall) // simple
        //    if (_tileData.neighbors[(int)TileData.Dirs.S].tileType == TileData.TileType.Ground)
        //    {
        //        _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.S];
        //    }
        //    else
        //        _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[8];

        //if (_tileData.tileType == TileData.TileType.Ground)
        //    _tileData.GetComponent<SpriteRenderer>().sprite = s_groundTiles[8];

        if (_tileData.tileType == TileData.TileType.Wall)
        {
            if (_tileData.neighbors[(int)TileData.Dirs.E].tileType == TileData.TileType.SWall &&
                _tileData.neighbors[(int)TileData.Dirs.SE].tileType == TileData.TileType.Ground)
            {
                _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.E];
                _tileData.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            else if (_tileData.neighbors[(int)TileData.Dirs.W].tileType == TileData.TileType.SWall &&
                _tileData.neighbors[(int)TileData.Dirs.SW].tileType == TileData.TileType.Ground)
            {
                _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.W];
            }
            return;
        }

        if (_tileData.tileType == TileData.TileType.SWall)
        {
            s_tempTiles = s_sWallTiles;
            compareTileType = TileData.TileType.Ground;
        }

        if (_tileData.tileType == TileData.TileType.Ground)
        {
            s_tempTiles = s_groundTiles;
            compareTileType = TileData.TileType.SWall;
            compareTileType2 = TileData.TileType.Wall;
        }

        if (_tileData.neighbors[(int)TileData.Dirs.S].tileType == compareTileType)
        {
            _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[(int)TileData.Dirs.S];

            if ((_tileData.neighbors[(int)TileData.Dirs.SE].tileType == compareTileType ||
                _tileData.neighbors[(int)TileData.Dirs.SE].tileType == compareTileType2) &&
            _tileData.neighbors[(int)TileData.Dirs.E].tileType == compareTileType)
                _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[(int)TileData.Dirs.SE];
            else if ((_tileData.neighbors[(int)TileData.Dirs.SW].tileType == compareTileType ||
                _tileData.neighbors[(int)TileData.Dirs.SW].tileType == compareTileType2) &&
            _tileData.neighbors[(int)TileData.Dirs.W].tileType == compareTileType)
                _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[(int)TileData.Dirs.SW];
            else if (_tileData.neighbors[(int)TileData.Dirs.N].tileType == compareTileType)
                _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[9];
        }
        else if (_tileData.neighbors[(int)TileData.Dirs.N].tileType == compareTileType)
        {
            _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[(int)TileData.Dirs.N];

            if ((_tileData.neighbors[(int)TileData.Dirs.NE].tileType == compareTileType ||
                _tileData.neighbors[(int)TileData.Dirs.NE].tileType == compareTileType2) &&
            _tileData.neighbors[(int)TileData.Dirs.E].tileType == compareTileType)
                _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[(int)TileData.Dirs.NE];
            else if ((_tileData.neighbors[(int)TileData.Dirs.NW].tileType == compareTileType ||
                _tileData.neighbors[(int)TileData.Dirs.NW].tileType == compareTileType2) &&
            _tileData.neighbors[(int)TileData.Dirs.W].tileType == compareTileType)
                _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[(int)TileData.Dirs.NW];
        }
        else if (_tileData.neighbors[(int)TileData.Dirs.E].tileType == compareTileType)
        {
            _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[(int)TileData.Dirs.E];
            if (_tileData.neighbors[(int)TileData.Dirs.W].tileType == compareTileType)
                _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[10];
        }
        else if (_tileData.neighbors[(int)TileData.Dirs.W].tileType == compareTileType)
        {
            _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[(int)TileData.Dirs.W];
        }
        else
            _tileData.GetComponent<SpriteRenderer>().sprite = s_tempTiles[8];

        //if (_tileData.tileType == TileData.TileType.SWall)
        //{
        //    if (_tileData.neighbors[(int)TileData.Dirs.S].tileType == TileData.TileType.Ground)
        //    {
        //        _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.S];

        //        if (_tileData.neighbors[(int)TileData.Dirs.SE].tileType == TileData.TileType.Ground)
        //            _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.SE];
        //        else if (_tileData.neighbors[(int)TileData.Dirs.SW].tileType == TileData.TileType.Ground)
        //            _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.SW];
        //    }
        //    else if (_tileData.neighbors[(int)TileData.Dirs.N].tileType == TileData.TileType.Ground)
        //    {
        //        _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.N];

        //        if (_tileData.neighbors[(int)TileData.Dirs.NE].tileType == TileData.TileType.Ground)
        //            _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.NE];
        //        else if (_tileData.neighbors[(int)TileData.Dirs.NW].tileType == TileData.TileType.Ground)
        //            _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.NW];
        //    }
        //    else if (_tileData.neighbors[(int)TileData.Dirs.E].tileType == TileData.TileType.Ground)
        //        _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.E];
        //    else if (_tileData.neighbors[(int)TileData.Dirs.W].tileType == TileData.TileType.Ground)
        //        _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[(int)TileData.Dirs.W];
        //    else
        //        _tileData.GetComponent<SpriteRenderer>().sprite = s_sWallTiles[8];
        //}
    }
}
