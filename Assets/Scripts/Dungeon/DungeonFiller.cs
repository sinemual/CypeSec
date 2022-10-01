using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonFiller : MonoBehaviour
{
    public GameObject p_cueen;
    public GameObject p_enterTile;
    public GameObject p_wizard;
    public GameObject[] p_relics;

    private GameObject cueen;
    private GameObject enterDungeon;
    private GameObject wizard;

    private DungeonGenerator dungeonGenerator;

    public void Fill(DungeonGenerator _dungeonGenerator)
    {
        dungeonGenerator = _dungeonGenerator;
        CueenLocate();
        EnterLocate();
        //WizardInit();
        RelicsLocate();
    }

    private void WizardInit()
    {
        wizard = Instantiate(p_wizard);
    }

    private void RelicsLocate()
    {
        GameObject relicsParent = new GameObject();
        relicsParent.transform.SetParent(transform);
        relicsParent.name = $"Relic";
        for (int i = 0; i < 5; i++)
        {
            TileData _posTileData = dungeonGenerator.grounds[Random.Range(0, dungeonGenerator.grounds.Count)];
            if (!_posTileData.isBusy)
                Instantiate(p_relics[Random.Range(0, p_relics.Length)], _posTileData.transform.position, Quaternion.identity, relicsParent.transform);
        }
    }

    private void EnterLocate()
    {
        Vector2 locatePos = new Vector2(Random.Range(dungeonGenerator.spareSpaceRows / 2 - ((dungeonGenerator.dungRows / 2) - 2),
            dungeonGenerator.spareSpaceRows / 2 + ((dungeonGenerator.dungRows / 2) - 2)),
            dungeonGenerator.spareSpaceColumns - Random.Range(4, 7));
        TileData tempTileData = dungeonGenerator.dungTilesData[(int)locatePos.x, (int)locatePos.y];
        tempTileData.SetTileType(TileData.TileType.Ground);

        enterDungeon = Instantiate(p_enterTile, new Vector2(locatePos.x, locatePos.y + 1), Quaternion.identity, transform);
        SceneController.Instance.enterDungeon = enterDungeon.GetComponent<TileData>();

        while (tempTileData.neighbors[(int)TileData.Dirs.S].tileType != TileData.TileType.Ground)
        {
            tempTileData = tempTileData.neighbors[(int)TileData.Dirs.S];
            tempTileData.SetTileType(TileData.TileType.Ground);
        }
    }

    private void CueenLocate()
    {
        TileData cueenLocateTile = null;
        //if (cueen != null)
        //    Destroy(cueen);
        for (int i = 0; i < 50; i++)
        {
            cueenLocateTile = SearchCueenLocate();

            if (cueenLocateTile != null)
            {
                if (cueen == null)
                    cueen = Instantiate(p_cueen, cueenLocateTile.gameObject.transform.position, Quaternion.identity) as GameObject;
                else
                    cueen.transform.position = cueenLocateTile.gameObject.transform.position;

                SceneController.Instance.cueen = cueen.GetComponent<Cueen>();
                SceneController.Instance.cueen.LocateTile(cueenLocateTile);
                cueenLocateTile.SetTileType(TileData.TileType.Ground, true);
                for (int j = 0; j < cueenLocateTile.neighbors.Length; j++)
                {
                    cueenLocateTile.neighbors[j].SetTileType(TileData.TileType.Ground, true);
                    try { cueenLocateTile.neighbors[(int)TileData.Dirs.N].neighbors[i].SetTileType(TileData.TileType.Ground, true); }
                    catch { SceneManager.LoadScene(0); }


                    //if (dungeonGenerator.grounds.Contains(cueenLocateTile.neighbors[j]))
                    //{
                    //    dungeonGenerator.grounds.Remove(cueenLocateTile.neighbors[j]);
                    //    //grounds.Sort();
                    //}
                }
                break;
            }
        }
        if (cueenLocateTile == null)
            CueenLocate();
    }

    private TileData SearchCueenLocate()
    {

        TileData mbLocate = dungeonGenerator.dungTilesData[Random.Range(dungeonGenerator.spareSpaceRows / 2 - ((dungeonGenerator.dungRows / 2) - 2), dungeonGenerator.spareSpaceRows / 2 + ((dungeonGenerator.dungRows / 2) - 2)),
            Random.Range(dungeonGenerator.spareSpaceColumns / 2 - ((dungeonGenerator.dungColumns / 2) - 2), dungeonGenerator.spareSpaceColumns / 2)];
        bool mbFlag = false;

        if (mbLocate.neighbors[(int)TileData.Dirs.N].neighbors[(int)TileData.Dirs.N].tileType == TileData.TileType.Ground)
            mbFlag = true;

        if (mbFlag)
            return mbLocate;
        else
            return null;
    }
}
