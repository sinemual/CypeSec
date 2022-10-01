using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    #region Singleton Init
    private static BuildingsManager _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static BuildingsManager Instance // Init not in order
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
        _instance = FindObjectOfType<BuildingsManager>();
        _instance.Initialize();
    }
    #endregion

    public List<GameObject> p_buildings = new List<GameObject>();

    public GameObject p_buildPlace;
    public Transform buildPlacesParent;
    public List<BuildPlace> buildPlaces = new List<BuildPlace>();
    private GameObject currentPickBuilding;

    private void Initialize()
    {
    }

    public void InitBuildPlaces(List<TileData> _grounds)
    {
        for (int i = 0; i < _grounds.Count; i++)
        {
            if (!_grounds[i].isBusy)
            {
                GameObject _temp = Instantiate(p_buildPlace, _grounds[i].transform.position, Quaternion.identity, buildPlacesParent);
                buildPlaces.Add(_temp.GetComponent<BuildPlace>());
            }
        }
        DisableBuildPlaces();
    }

    public void ActivateBuildPlaces()
    {
        for (int i = 0; i < buildPlaces.Count; i++)
        {
            buildPlaces[i].gameObject.SetActive(true);
            buildPlaces[i].Check();
        }
    }

    public void DisableBuildPlaces()
    {
        for (int i = 0; i < buildPlaces.Count; i++)
            buildPlaces[i].gameObject.SetActive(false);
    }

    public void Pick(GameObject _building)
    {
        SceneController.Instance.BuildModeToggle();
        currentPickBuilding = Instantiate(_building, transform);
    }

    public void RotatePick()
    {
        currentPickBuilding.GetComponent<Building>().Rotate();
    }
}
