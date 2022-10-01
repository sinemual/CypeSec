using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangersManager : MonoBehaviour
{
    private TileData enterDungeon;
    public GameObject[] p_strangers;
    public List<Stranger> lifeStrangers = new List<Stranger>();

    private void Start()
    {
        StartCoroutine(CreateStrangerDelay());
    }

    IEnumerator CreateStrangerDelay()
    {
        yield return new WaitForSeconds(130.0f);
        CreateStranger();
        StartCoroutine(CreateStrangerDelay());
    }

    [NaughtyAttributes.Button]
    public void CreateStranger()
    {
        if (enterDungeon == null)
            enterDungeon = SceneController.Instance.enterDungeon;
        GameObject _stranger = Instantiate(p_strangers[Data.Instance.level - 1], enterDungeon.transform.position, Quaternion.identity, transform);
    }
}
