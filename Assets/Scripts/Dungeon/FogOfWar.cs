using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public GameObject p_fogOfWar;

    private List<Transform> fogs = new List<Transform>();
    private SpriteRenderer[] fogsRenders;

    private bool isInit;
    private GameObject fogParent;

    private DungeonGenerator dungeonGenerator;

    public void FogOfWarOn(DungeonGenerator _dungeonGenerator)
    {
        dungeonGenerator = _dungeonGenerator;
        FogOfWarFill();
        Init();
    }

    private void Init()
    {
        fogs.AddRange(fogParent.GetComponentsInChildren<Transform>());
        fogsRenders = new SpriteRenderer[fogs.Count - 1];
        for (int i = 0; i < fogsRenders.Length; i++)
            fogsRenders[i] = fogs[i + 1].GetComponent<SpriteRenderer>();
        isInit = true;
    }

    private void FixedUpdate()
    {
        if (isInit)
        {
            for (int i = 0; i < fogsRenders.Length; i++)
                if (fogsRenders[i].color.a < 1.0f)
                    fogsRenders[i].color = Vector4.Lerp(fogsRenders[i].color, Utility.exploredAlpha, Time.deltaTime);
        }
    }

    private void FogOfWarFill()
    {
        fogParent = new GameObject();
        fogParent.transform.SetParent(transform);
        fogParent.name = $"FogOfWar";

        for (int i = 0; i < dungeonGenerator.spareSpaceRows; i++)
            for (int j = 0; j < dungeonGenerator.spareSpaceColumns; j++)
            {
                if (!TileDataTool.ContainsInNeighbors(SceneController.Instance.cueen.locateTile, dungeonGenerator.dungTilesData[i, j]))
                    if (!TileDataTool.ContainsInNeighbors(SceneController.Instance.cueen.locateTile.neighbors[(int)TileData.Dirs.N], dungeonGenerator.dungTilesData[i, j]))
                        if (!TileDataTool.ContainsInNeighbors(StorageManager.Instance.idlePoint, dungeonGenerator.dungTilesData[i, j]))
                            if (!TileDataTool.ContainsInNeighbors(StorageManager.Instance.loadUnloadPoint, dungeonGenerator.dungTilesData[i, j]))
                                if (dungeonGenerator.dungTilesData[i, j] != StorageManager.Instance.idlePoint)
                                    if (dungeonGenerator.dungTilesData[i, j] != StorageManager.Instance.loadUnloadPoint)
                                    {
                                        GameObject _instance = Instantiate(p_fogOfWar, new Vector3(i, j, 0f), Quaternion.identity, fogParent.transform) as GameObject;
                                    }
            }

        fogParent.AddComponent<FogOfWar>();
    }
}
