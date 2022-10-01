using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cueen))]
public class CeggsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject p_cegg = null;

    private bool[] freeCeggPlaces = new bool[3];
    private TileData[] ceggPlaces = new TileData[3];
    private List<Cegg> ceggs = new List<Cegg>();
    private Cueen cueen;

    private void Start()
    {
        cueen = GetComponent<Cueen>();
        Instalization();
    }

    private void Instalization()
    {
        ceggPlaces[0] = cueen.locateTile.neighbors[(int)TileData.Dirs.SW];
        ceggPlaces[1] = cueen.locateTile.neighbors[(int)TileData.Dirs.S];
        ceggPlaces[2] = cueen.locateTile.neighbors[(int)TileData.Dirs.SE];

        for (int i = 0; i < freeCeggPlaces.Length; i++)
            freeCeggPlaces[i] = true;
    }

    public void CreateCegg()
    {
        if (Data.Instance.Meat >= 2)
        {
            for (int i = 0; i < freeCeggPlaces.Length; i++)
                if (freeCeggPlaces[i])
                {
                    GameObject _cegg = Instantiate(p_cegg, new Vector3(ceggPlaces[i].pos.x, ceggPlaces[i].pos.y, 0.0f), Quaternion.identity, transform);
                    ceggs.Add(_cegg.GetComponent<Cegg>());
                    freeCeggPlaces[i] = false;
                    StorageManager.Instance.GetResource(Resource.Type.Meat, 2);
                    return;
                }
        }
    }

    public void DestroyCegg(Cegg _cegg)
    {
        for (int i = 0; i < freeCeggPlaces.Length; i++)
            if (!freeCeggPlaces[i])
            {
                freeCeggPlaces[i] = true;
                //ceggs.Remove(ceggs[i]);
                Destroy(_cegg.gameObject);
                return;
            }
    }
}
