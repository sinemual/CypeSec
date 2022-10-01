using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDataTool : MonoBehaviour
{
    public static TileData DetectLocation(Transform _transform)
    {
        return DungeonGenerator.Instance.dungTilesData[(int)_transform.position.x, (int)_transform.position.y];
    }

    public static int Heuristic(TileData a, TileData b)
    {
        return (int)(System.Math.Abs(a.pos.x - b.pos.x) + System.Math.Abs(a.pos.y - b.pos.y));
    }

    public static int Heuristic(GameObject a, GameObject b)
    {
        return (int)(System.Math.Abs(a.transform.position.x - b.transform.position.x) + System.Math.Abs(a.transform.position.y - b.transform.position.y));
    }

    public static bool ContainsInNeighbors(TileData _tileData, TileData _tileDataCheck, bool _isDiagonal = true)
    {
        List<TileData> _neighbors = new List<TileData>();
        if (_isDiagonal)
            _neighbors.AddRange(_tileData.neighbors);
        else
        {
            _neighbors.Add(_tileData.neighbors[0]);
            for (int i = 0; i < 3; i++)
                if (_tileData.neighbors[i] & 2 == 0)
                    _neighbors.Add(_tileData.neighbors[i]);
        }
        return _neighbors.Contains(_tileDataCheck);
    }

    public static bool ContainsInNeighbors(TileData _tileData, TileData.TileType _tileType, bool _isDiagonal = true)
    {
        List<TileData.TileType> _neighborsTypes = new List<TileData.TileType>();
        if (_isDiagonal)
            for (int i = 0; i < _tileData.neighbors.Length; i++)
                _neighborsTypes.Add(_tileData.neighbors[i].tileType);
        else
        {
            for (int i = 0; i < _tileData.neighbors.Length; i++)
                if (i == 0 || i % 2 == 0)
                    _neighborsTypes.Add(_tileData.neighbors[i].tileType);
        }
        return _neighborsTypes.Contains(_tileType);
    }

    public static TileData GetNierNeighborGround(TileData _tileData)
    {
        for (int i = 0; i < _tileData.neighbors.Length; i++)
            if (_tileData.neighbors[i].tileType == TileData.TileType.Ground)
                if (i == 0 || i % 2 == 0)
                    return _tileData.neighbors[i];
        return null;
    }

    public static TileData GetNierNeighborGround(TileData _tileData, bool _isRandom = false, bool _isDiagonal = false)
    {
        List<TileData> _grounds = new List<TileData>();

        for (int i = 0; i < _tileData.neighbors.Length; i++)
            if (_tileData.neighbors[i].tileType == TileData.TileType.Ground)
            {
                if (_isDiagonal)
                    _grounds.Add(_tileData.neighbors[i]);
                else if (i == 0 || i % 2 == 0)
                    _grounds.Add(_tileData.neighbors[i]);
            }

        if (_grounds.Count > 0)
        {
            if (_isRandom)
                return _grounds[Random.Range(0, _grounds.Count)];
            else
                return _grounds[0];
        }

        return null;
    }

    public static TileData GetNierNeighborGround(TileData _tileData, TileData _location)
    {
        int[] distances = new int[4];
        int temp = 100;
        int k = 0;
        int counter = 0;
        for (int i = 0; i < _tileData.neighbors.Length; i++)
            if (_tileData.neighbors[i].tileType == TileData.TileType.Ground)
                if (i == 0 || i % 2 == 0)
                {
                    distances[counter] = TileDataTool.Heuristic(_location, _tileData.neighbors[i]);
                    counter++;
                }

        for (int i = 0; i < distances.Length; i++)
            if (distances[i] < temp)
            {
                temp = distances[i];
                k = i * 2;
            }
        return _tileData.neighbors[k];
    }
}
