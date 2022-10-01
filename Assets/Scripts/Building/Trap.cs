using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    public int damage;
    public bool isEye;
    public float rayDistance;
    public UnityAction onWork;
    private Stats needStats;
    private Animator animator;
    private List<GameObject> nearFogs = new List<GameObject>();

    public Unit currentTarget;
    public bool targetThroughTrap;
    public bool reloaded;
    public float timeReload = 5;

    private void Start()
    {

        needStats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
        reloaded = true;
        Raycating();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stranger") && !isEye)
        {
            currentTarget = collision.GetComponent<Unit>(); ;
            targetThroughTrap = IsThrough(currentTarget.GetComponent<Stats>());
            if (!targetThroughTrap && reloaded)
            {
                currentTarget.ModifyHealth(damage);
                onWork.Invoke();
                animator?.SetBool("IsWork", true);
                reloaded = false;
                CoroutineActions.ExecuteAction(timeReload, () => { reloaded = true; });
            }
        }
    }

    public bool IsThrough(Stats _strangerStats)
    {
        return false;
        //bool _isPassed = true;

        //if (_strangerStats.strong < needStats.strong)
        //    _isPassed = false;
        //if (_strangerStats.agility < needStats.agility)
        //    _isPassed = false;
        //if (_strangerStats.intellect < needStats.intellect)
        //    _isPassed = false;

        //return _isPassed;
    }

    public void EnableNearFogs()
    {
        for (int i = 0; i < nearFogs.Count; i++)
            nearFogs[i].SetActive(true);
    }

    private void Raycating()
    {
        RaycastHit2D[] hitsO = Physics2D.RaycastAll(transform.localPosition, transform.up, rayDistance);
        if (hitsO != null)
        {
            foreach (var item in hitsO)
            {
                if (item.collider.gameObject.CompareTag("Fog"))
                {
                    if (!nearFogs.Contains(item.collider.gameObject))
                        nearFogs.Add(item.collider.gameObject);
                    item.collider.gameObject.SetActive(false);
                }
            }
        }
    }
}
