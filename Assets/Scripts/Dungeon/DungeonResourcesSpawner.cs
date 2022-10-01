using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonResourcesSpawner : MonoBehaviour
{
    [HideInInspector] public List<Resource> resources = new List<Resource>();

    [Header("Prefabs")]
    public GameObject p_woody;
    public GameObject p_stoney;
    public GameObject p_metaly;

    private int woodyCounter;
    private int stoneyCounter;
    private int metalyCounter;

    public void Spawn(DungeonGenerator _dungeonGenerator, int _woodyCount, int _stoneyCount, int _metalyCount)
    {
        ResetSpawner();

        for (int i = 0; i < 10; i++)
        {
            if (woodyCounter < _woodyCount)
            {
                int random = Random.Range(0, _dungeonGenerator.sWalls.Count);
                if (!_dungeonGenerator.sWalls[random].isResource)
                {
                    GameObject instance = Instantiate(p_woody, _dungeonGenerator.sWalls[random].gameObject.transform.position, Quaternion.identity, _dungeonGenerator.sWalls[random].gameObject.transform) as GameObject;
                    _dungeonGenerator.sWalls[random].isResource = true;
                    resources.Add(instance.gameObject.GetComponent<Resource>());
                    woodyCounter++;
                }
            }
        }

        for (int j = 0; j < 10; j++)
        {
            if (stoneyCounter < _stoneyCount)
            {
                int random = Random.Range(0, _dungeonGenerator.sWalls.Count);
                if (!_dungeonGenerator.sWalls[random].isResource)
                {
                    GameObject instance = Instantiate(p_stoney, _dungeonGenerator.sWalls[random].gameObject.transform.position, Quaternion.identity, _dungeonGenerator.sWalls[random].gameObject.transform) as GameObject;
                    _dungeonGenerator.sWalls[random].isResource = true;
                    resources.Add(instance.gameObject.GetComponent<Resource>());
                    stoneyCounter++;
                }
            }
        }

        for (int k = 0; k < 10; k++)
        {
            if (metalyCounter < _metalyCount)
            {
                int random = Random.Range(0, _dungeonGenerator.sWalls.Count);
                if (!_dungeonGenerator.sWalls[random].isResource)
                {
                    GameObject instance = Instantiate(p_metaly, _dungeonGenerator.sWalls[random].gameObject.transform.position, Quaternion.identity, _dungeonGenerator.sWalls[random].gameObject.transform) as GameObject;
                    _dungeonGenerator.sWalls[random].isResource = true;
                    resources.Add(instance.gameObject.GetComponent<Resource>());
                    metalyCounter++;
                }
            }
        }
    }

    public void SetSprites()
    {
        foreach (var item in resources)
        {
            if(item.currrentResType != Resource.Type.Meat && item.currrentResType != Resource.Type.Mucus)
            {
                if (item.GetComponentInParent<TileData>().neighbors[(int)TileData.Dirs.N].tileType == TileData.TileType.Ground)
                    item.SetSprite(0);
                if (item.GetComponentInParent<TileData>().neighbors[(int)TileData.Dirs.E].tileType == TileData.TileType.Ground)
                    item.SetSprite(1);
                if (item.GetComponentInParent<TileData>().neighbors[(int)TileData.Dirs.N].tileType == TileData.TileType.Ground)
                    item.SetSprite(2);
                if (item.GetComponentInParent<TileData>().neighbors[(int)TileData.Dirs.W].tileType == TileData.TileType.Ground)
                    item.SetSprite(3);
            }
        }
    }

    private void ResetSpawner()
    {
        woodyCounter = 0;
        stoneyCounter = 0;
        metalyCounter = 0;

        resources.Clear();
    }
}
