using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : MonoBehaviour
{
    public List<GameObject> objectsInMemory;
    public GameObject memoryImprint;
    public GameObject foundObject;
    public string lookingForTag;
    public TileData destinationPoint;

    public TileData meatyPoint;
    public TileData woodyPoint;
    public TileData stoneyPoint;
    public TileData metalyPoint;
    public TileData mucusPoint;

    private void Start()
    {
        objectsInMemory.Clear();
    }

    public void ClearMemory()
    {
        objectsInMemory.Clear();
    }

    public void Handle(GameObject _go)
    {
        memoryImprint = _go;
    }

    public void AddObject(GameObject _go)
    {
        objectsInMemory.Add(_go);
        Handle(_go);
        if (_go.GetComponent<Resource>() != null)
        {
            if (_go.GetComponent<Resource>().currrentResType == Resource.Type.Meat)
                meatyPoint = TileDataTool.DetectLocation(_go.transform);
            if (_go.GetComponent<Resource>().currrentResType == Resource.Type.Woody)
                woodyPoint = TileDataTool.DetectLocation(_go.transform);
            if (_go.GetComponent<Resource>().currrentResType == Resource.Type.Stoney)
                stoneyPoint = TileDataTool.DetectLocation(_go.transform);
            if (_go.GetComponent<Resource>().currrentResType == Resource.Type.Metaly)
                metalyPoint = TileDataTool.DetectLocation(_go.transform);
            if (_go.GetComponent<Resource>().currrentResType == Resource.Type.Mucus)
                mucusPoint = TileDataTool.DetectLocation(_go.transform);
        }
    }
}
