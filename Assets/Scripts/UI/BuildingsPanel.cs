using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsPanel : MonoBehaviour
{
    public List<BuildingUI> buildingUIs = new List<BuildingUI>();
    public bool isShow;

    private void Start()
    {
        buildingUIs.AddRange(GetComponentsInChildren<BuildingUI>());
    }

    public void ShowToggle()
    {
        isShow = !isShow;
        for (int i = 0; i < buildingUIs.Count; i++)
            buildingUIs[i].Init(BuildingsManager.Instance.p_buildings[i]);
    }
}
