using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cegg : MonoBehaviour
{
    [SerializeField]
    private GameObject p_cim = null;
    [SerializeField]
    private int incubationStages = 3;
    [SerializeField]
    private float stageTime = 10;
    [SerializeField]
    private Sprite[] s_incubationStagesSprites = new Sprite[4];
    private int stagesCounter;
    private bool alreadyBorn;

    void Start()
    {
        stagesCounter = 0;
        alreadyBorn = false;
        StartCoroutine(IncubationStageDelay());
    }

    IEnumerator IncubationStageDelay()
    {
        yield return new WaitForSeconds(stageTime);
        stagesCounter++;
        if (stagesCounter < s_incubationStagesSprites.Length)
            GetComponent<SpriteRenderer>().sprite = s_incubationStagesSprites[stagesCounter];
        if (stagesCounter >= incubationStages && !alreadyBorn)
        {
            AudioManager.Instance.Play("egg");
            GameObject _cim = Instantiate(p_cim, transform.position, Quaternion.identity);
            SceneController.Instance.cueen.cims.Add(_cim.GetComponent<Cim>());
            alreadyBorn = true;
        }
        if (stagesCounter >= incubationStages * 2)
        {
            SceneController.Instance.cueen.ceggsManager.DestroyCegg(this);
            yield return null;
        }
        StartCoroutine(IncubationStageDelay());
    }
}
