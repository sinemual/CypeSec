using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageManager : MonoBehaviour
{
    #region Singleton Init
    private static StorageManager _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static StorageManager Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<StorageManager>();
    }
    #endregion

    public TileData loadUnloadPoint;
    public TileData idlePoint;
    public TileData relicPoint;
    public GameObject[] p_resources;
    [HideInInspector] public TileData[] resourcesPlaces = new TileData[8];
    [HideInInspector] public TileData[] idlePlaces = new TileData[8];
    private bool[] freeIdlePlaces = new bool[8];

    public List<Relic> relics = new List<Relic>();

    public void Init(DungeonGenerator _dungeonGenerator)
    {
        loadUnloadPoint = DungeonGenerator.Instance.dungTilesData[(int)SceneController.Instance.cueen.locateTile.pos.x + 3, (int)SceneController.Instance.cueen.locateTile.pos.y];
        idlePoint = DungeonGenerator.Instance.dungTilesData[(int)SceneController.Instance.cueen.locateTile.pos.x - 3, (int)SceneController.Instance.cueen.locateTile.pos.y];
        loadUnloadPoint.SetTileType(TileData.TileType.Ground, true);
        idlePoint.SetTileType(TileData.TileType.Ground, true);
        for (int i = 0; i < loadUnloadPoint.neighbors.Length; i++)
        {
            loadUnloadPoint.neighbors[i].SetTileType(TileData.TileType.Ground, true);
            resourcesPlaces[i] = loadUnloadPoint.neighbors[i];
            idlePoint.neighbors[i].SetTileType(TileData.TileType.Ground, true);
            idlePlaces[i] = idlePoint.neighbors[i];
            freeIdlePlaces[i] = true;
        }
        relicPoint = TileDataTool.DetectLocation(resourcesPlaces[4].transform);
        SetupResources();
    }

    public TileData NeedPlace()
    {
        for (int i = 0; i < freeIdlePlaces.Length; i++)
            if (freeIdlePlaces[i])
            {
                freeIdlePlaces[i] = false;
                return idlePlaces[i];
            }
        return null;
    }

    private void SetupResources()
    {
        if (Data.Instance.Woody > 0)
            Instantiate(p_resources[0], new Vector3(resourcesPlaces[0].pos.x, resourcesPlaces[0].pos.y, 0), Quaternion.identity, transform);
        if (Data.Instance.Stoney > 0)
            Instantiate(p_resources[1], new Vector3(resourcesPlaces[1].pos.x, resourcesPlaces[1].pos.y, 0), Quaternion.identity, transform);
        if (Data.Instance.Metaly > 0)
            Instantiate(p_resources[2], new Vector3(resourcesPlaces[2].pos.x, resourcesPlaces[2].pos.y, 0), Quaternion.identity, transform);
        if (Data.Instance.Meat > 0)
            Instantiate(p_resources[3], new Vector3(resourcesPlaces[3].pos.x, resourcesPlaces[3].pos.y, 0), Quaternion.identity, transform);
    }

    public bool IsResourceAvaliable(Resource.Type _resource, int _amount)
    {
        if (_resource == Resource.Type.Woody)
            return Data.Instance.Woody >= _amount;
        if (_resource == Resource.Type.Stoney)
            return Data.Instance.Stoney >= _amount;
        if (_resource == Resource.Type.Metaly)
            return Data.Instance.Metaly >= _amount;
        if (_resource == Resource.Type.Meat)
            return Data.Instance.Meat >= _amount;
        if (_resource == Resource.Type.Mucus)
            return Data.Instance.Mucus >= _amount;
        return false;
    }

    public void PutResource(GameObject _resourceObject)
    {
        Resource _resource = _resourceObject.GetComponent<Resource>();
        Resource.Type _resourceType = _resource.currrentResType;
        int _amount = _resource.amount;
        Debug.Log($"PutResource: {_resourceType}, {_amount}");

        if (_resourceType == Resource.Type.Woody)
            Data.Instance.Woody += _amount;
        if (_resourceType == Resource.Type.Stoney)
            Data.Instance.Stoney += _amount;
        if (_resourceType == Resource.Type.Metaly)
            Data.Instance.Metaly += _amount;
        if (_resourceType == Resource.Type.Meat)
            Data.Instance.Meat += _amount;
        if (_resourceType == Resource.Type.Mucus)
            Data.Instance.Mucus += _amount;

        UIManager.Instance.RefreshInfo();
        _resourceObject.SetActive(false);
    }

    public GameObject GetResource(Resource.Type _resource, int _amount)
    {
        //Debug.Log($"GetResource: {_resource}, {_amount}");
        if (_resource == Resource.Type.Woody)
            Data.Instance.Woody -= _amount;
        if (_resource == Resource.Type.Stoney)
            Data.Instance.Stoney -= _amount;
        if (_resource == Resource.Type.Metaly)
            Data.Instance.Metaly -= _amount;
        if (_resource == Resource.Type.Meat)
            Data.Instance.Meat -= _amount;
        if (_resource == Resource.Type.Mucus)
            Data.Instance.Mucus -= _amount;

        UIManager.Instance.RefreshInfo();
        return p_resources[(int)_resource];
    }

    public void PutRelic(Relic _relic)
    {
        Data.Instance.DungInviting += _relic.cost;
        _relic.transform.parent = null;
        _relic.gameObject.transform.position = relicPoint.pos;
        relics.Add(_relic);
        UIManager.Instance.RefreshInfo();
    }
}
