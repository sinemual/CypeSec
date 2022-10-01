using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wizard : MonoBehaviour
{
    [HideInInspector] public Moving moving;

    private void Start()
    {
        moving = GetComponent<Moving>();
        transform.position = SceneController.Instance.cueen.locateTile.neighbors[(int)TileData.Dirs.W].transform.position;
        UnityAction completeCallback = () =>
        {
            Destroy(gameObject);
        };
        //moving.GoTo(TileDataTool.GetNierNeighborGround(TileDataTool.DetectLocation(SceneController.Instance.enterDungeon.transform)), completeCallback);
    }
}
