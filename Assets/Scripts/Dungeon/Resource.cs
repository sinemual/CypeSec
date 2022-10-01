using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour, ILookingFor
{
    public enum Type { Woody, Stoney, Metaly, Mucus, Meat, Null }
    public Type currrentResType;
    public Sprite s_mined;
    public Sprite[] s_dirs;
    public int amount;

    private WorkData workData;

    public string ObjectTag { get => currrentResType.ToString(); set { } }

    public void SetSprite(int _dirNum)
    {
        GetComponent<SpriteRenderer>().sprite = s_dirs[_dirNum];
    }

    public void Mine(Unit _miner, UnityAction _onWorkComplete)
    {
        Debug.Log("Mining!");
        workData = GetComponent<WorkData>();
        StartCoroutine(Mining(_miner, _onWorkComplete));
    }

    IEnumerator Mining(Unit _miner, UnityAction _onWorkComplete)
    {
        workData.OnWorkStart.Invoke();
        yield return new WaitForSeconds(workData.workingTime);
        GetComponentInParent<TileData>().isResource = false;
        GetComponent<SpriteRenderer>().sprite = s_mined;
        _miner.US.Hands.Take(gameObject);
        _onWorkComplete.Invoke();
        //_cim.WorkComplete(gameObject, amount);
    }
}
