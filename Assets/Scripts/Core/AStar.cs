using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public static bool isDebug;

    private static TileData current;

    public static Transform[] GetPath(TileData startTileData, TileData goalTileData, NullCatcher _currentUnitCatcher = null)
    {
        PriorityQueue<TileData> frontiers = new PriorityQueue<TileData>();
        Dictionary<TileData, TileData> came_from = new Dictionary<TileData, TileData>();
        Dictionary<TileData, int> costSoFar = new Dictionary<TileData, int>();
        List<TileData> path = new List<TileData>();

        frontiers.Enqueue(startTileData, 0);
        costSoFar.Add(startTileData, 0);

        while (frontiers.Count > 0)
        {
            current = frontiers.Dequeue();
            if (current == goalTileData)
                break;

            foreach (TileData next in current.neighbors)
            {
                if (next == null || goalTileData == null)
                {
                    _currentUnitCatcher.AStarCatcher();
                    return null;
                }
                int newCoast = costSoFar[current] + GetMoveCost(current, next);
                if (!costSoFar.ContainsKey(next) || newCoast < costSoFar[next])
                {
                    if (next.tileType != TileData.TileType.Wall && next.tileType != TileData.TileType.SWall)
                    {
                        costSoFar[next] = newCoast;
                        int priority = newCoast + TileDataTool.Heuristic(goalTileData, next);
                        frontiers.Enqueue(next, priority);
                        came_from[next] = current;

                        if (isDebug)
                        {
                            came_from[next].GetComponent<SpriteRenderer>().color = Color.magenta;
                            next.GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                    }
                }
            }
        }
        current = goalTileData;
        path.Add(current);

        while (current != startTileData)
        {
            current = came_from[current];
            path.Add(current);
            if (isDebug)
                current.GetComponent<SpriteRenderer>().color = Color.red;
        }
        path.Reverse();
        Transform[] pathTs = new Transform[path.Count];
        for (int i = 0; i < path.Count; i++)
            pathTs[i] = path[i].transform;
        return pathTs;
    }

    private static int GetMoveCost(TileData a, TileData b)
    {
        if (b == a.neighbors[(int)TileData.Dirs.NE] ||
            b == a.neighbors[(int)TileData.Dirs.NW] ||
            b == a.neighbors[(int)TileData.Dirs.SE] ||
            b == a.neighbors[(int)TileData.Dirs.SW] ||
            b == SceneController.Instance.cueen.locateTile) //diagonal
            return 10;
        return 1;
    }

    private class PriorityQueue<T>
    {
        private List<Tuple<T, int>> elements = new List<Tuple<T, int>>();

        public int Count
        {
            get { return elements.Count; }
        }

        public void Enqueue(T item, int priority)
        {
            elements.Add(Tuple.Create(item, priority));
        }

        public T Dequeue()
        {
            int bestIndex = 0;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Item2 < elements[bestIndex].Item2)
                {
                    bestIndex = i;
                }
            }

            T bestItem = elements[bestIndex].Item1;
            elements.RemoveAt(bestIndex);
            return bestItem;
        }
    }
}
