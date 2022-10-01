using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    public Button buildingButton;
    public Text buildNameText;
    public Image buildImage;
    public Sprite[] resourcesSprites;
    public GameObject[] needResourcesPanels;
    private Image[] needResourcesImages = new Image[3];
    private Text[] needAmountResourcesTexts = new Text[3];

    private GameObject buildingGO;
    private Building building;

    public void Init(GameObject _building)
    {
        buildingGO = _building;
        building = buildingGO.GetComponent<Building>();

        for (int i = 0; i < needResourcesPanels.Length; i++)
        {
            needResourcesImages[i] = needResourcesPanels[i].transform.GetChild(0).GetComponent<Image>();
            needAmountResourcesTexts[i] = needResourcesPanels[i].GetComponentInChildren<Text>();
            needResourcesImages[i].sprite = resourcesSprites[(int)building.needResources[i]];
            needAmountResourcesTexts[i].text = building.needAmountResources[i].ToString();

            if (building.needAmountResources[i] == 0)
                needResourcesPanels[i].SetActive(false);
        }

        buildNameText.text = building.buildingName;
        buildImage.sprite = building.buildingSprites[0];

        buildingButton.onClick.RemoveAllListeners();
        buildingButton.onClick.AddListener(PickBuilding);
    }

    private void PickBuilding()
    {
        BuildingsManager.Instance.Pick(buildingGO);
    }
}
