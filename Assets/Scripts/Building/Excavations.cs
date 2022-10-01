using UnityEngine;
using System.Collections;

public class Excavations : MonoBehaviour
{
    public GameObject relic;
    public WorkData workData;

    private void Start()
    {
        Raycating();
    }

    private void Raycating()
    {
        RaycastHit2D[] hitsO = Physics2D.RaycastAll(transform.localPosition, transform.up, 0.1f);
        if (hitsO != null)
            foreach (var item in hitsO)
                if (item.collider.gameObject.CompareTag("Relic"))
                    relic = item.collider.gameObject;
    }

    public void DigUpThis(Cim _cim)
    {
        Debug.Log("DigUping!");
        StartCoroutine(DigUpingThis(_cim));
    }

    IEnumerator DigUpingThis(Cim _cim)
    {
        workData = GetComponent<WorkData>();
        workData.OnWorkStart.Invoke();
        yield return new WaitForSeconds(workData.workingTime);
        relic.transform.GetChild(0).gameObject.SetActive(false);
        //_cim.WorkComplete(relic);
        Destroy(gameObject);
    }
}
