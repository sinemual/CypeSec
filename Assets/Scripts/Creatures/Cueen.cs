using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cueen : MonoBehaviour, IHaveHealth
{
    [HideInInspector]
    public TileData locateTile;
    public List<Cim> cims = new List<Cim>(); // private
    [SerializeField]
    public Queue<Task> tasksInQueue  = new Queue<Task>();
    private int fill;
    [HideInInspector]
    public CeggsManager ceggsManager;
    public int Health { get; set; }
    public bool IsDead { get; set; }

    public int Fill {
        get => fill;
        set
        {
            if (fill + value <= 0)
            {
                fill = 0;
                Die();
            }
        }
    }

    void Start()
    {
        ceggsManager = GetComponent<CeggsManager>();
    }

    public void LocateTile(TileData _locateTile)
    {
        locateTile = _locateTile;
    }

    public void NewTask(Task _task)
    {
        tasksInQueue.Enqueue(_task);
        Cim searchedWorker = SearchWorker();
        if(searchedWorker != null)
        {
            searchedWorker.workProcessor.Work(_task);
            tasksInQueue.Dequeue();
        }
        else
        {
            Debug.LogWarning($"NEED A WORKER!");
        }
        TasksManager.Instance.tasksPanel.AddTask(_task);
    }

    public void AskTask(Cim _cim)
    {
        if(tasksInQueue.Count > 0)
        {
            Task _task = tasksInQueue.Peek();
            tasksInQueue.Dequeue();
            _cim.workProcessor.Work(_task);
            TasksManager.Instance.tasksPanel.RefreshInfo(_task);
        }
    }

    private Cim SearchWorker()
    {
        foreach (var cim in cims)
            if (cim.SM.CurrentState.GetType() == typeof(IdleState) && cim.workProcessor.currentTask == null)
                return cim;
        return null;
    }

    public void ModifyHealth(int _amount)
    {
        Health += _amount;
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        GetComponent<Animator>().SetBool("IsDie", true);
        IsDead = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }
}
