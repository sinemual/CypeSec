using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    public enum State { Planned, Resourcing, Building, Completed };
    public State currentState;
    public SpriteRenderer spriteRenderer;
    public bool isTrap;
    public bool isExcavations;
    public bool isRotateable;

    public string buildingName;
    public Sprite[] buildingSprites;
    public Resource.Type[] needResources = new Resource.Type[3];
    public int[] needAmountResources = new int[3];
    public bool[] haveNeedResources = new bool[3];

    private Trap trap;
    private int currentDir;
    private WorkData workData;

    private void Awake()
    {
        trap = GetComponent<Trap>();
        workData = GetComponent<WorkData>();
        isTrap = (trap != null);
        SetState(State.Planned);
    }

    private void Update()
    {
        if (currentState == State.Planned)
            Planned();
    }

    public void AddResource(Resource.Type _resource, int _amount)
    {
        for (int i = 0; i < needResources.Length; i++)
            if (_resource == needResources[i] && needAmountResources[i] != 0 && !haveNeedResources[i])
            {
                haveNeedResources[i] = true;
                needAmountResources[i] = 0;
                if (IsHaveNeedResources())
                    SetState(State.Building);
                return;
            }
    }

    private void PlannedInit()
    {
        for (int i = 0; i < haveNeedResources.Length; i++)
            if (needAmountResources[i] == 0)
                haveNeedResources[i] = true;
        TileDataTool.DetectLocation(transform).isBusy = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0.5f, 0.0f, 0.0f, 0.5f);
    }

    private void Planned()
    {
        bool _mayBuild = false;
        Vector3 _point = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        transform.position = _point;
        spriteRenderer.color = new Color(0.5f, 0.0f, 0.0f, 0.5f);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.up, 0.1f);
        foreach (var item in hits)
        {
            if (item.collider != null && item.collider.CompareTag("BuildPlace"))
            {
                transform.position = item.transform.position;
                spriteRenderer.color = new Color(0.0f, 0.5f, 0.0f, 0.5f);
                _mayBuild = true;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var isFog in hits)
                if (isFog.collider != null && isFog.collider.CompareTag("Fog") && isFog.collider.GetComponent<SpriteRenderer>().color.a > 0.8f)
                    _mayBuild = false;

            if (_mayBuild)
                SetState(State.Resourcing);
            else
            {
                SceneController.Instance.BuildModeToggle();
                Destroy(gameObject);
            }
        }
    }

    public void SetState(State _state)
    {
        currentState = _state;
        if (currentState == State.Planned)
            PlannedInit();
        else if (currentState == State.Resourcing)
            BuildingOnPlace();
        else if (currentState == State.Building)
            Debug.Log("");
        else if (currentState == State.Completed)
        {
            spriteRenderer.color = Utility.enabledColor;
            if (isTrap)
                GetComponent<Trap>().enabled = true;
            else if (isExcavations)
                GetComponent<Excavations>().enabled = true;
        }
    }

    private void BuildingOnPlace()
    {
        spriteRenderer.color = Utility.halfAlpha;
        SceneController.Instance.BuildModeToggle();
        TasksManager.Instance.AddTask(Task.TaskType.Building, this);
    }

    public bool IsHaveNeedResources()
    {
        bool _isHave = true;
        for (int i = 0; i < haveNeedResources.Length; i++)
            if (!haveNeedResources[i])
                _isHave = false;
        return _isHave;
    }

    public (Resource.Type, int) NeedResource()
    {
        for (int i = 0; i < needAmountResources.Length; i++)
        {
            if (needAmountResources[i] > 0)
                return (needResources[i], needAmountResources[i]);
        }
        return (Resource.Type.Null, 0);
    }

    public void BuildThis(Cim _cim)
    {
        Debug.Log("Building!");
        SetState(State.Building);
        StartCoroutine(BuildingThis(_cim));
    }

    IEnumerator BuildingThis(Cim _cim)
    {
        workData.OnWorkStart.Invoke();
        yield return new WaitForSeconds(workData.workingTime);
        SetState(State.Completed);
        //if (!isExcavations)
        //    _cim.WorkComplete();
        //else if (GetComponent<Excavations>() != null)
        //    GetComponent<Excavations>().DigUpThis(_cim);
    }

    public void Rotate()
    {
        if (isRotateable)
        {
            currentDir += 1;
            transform.Rotate(new Vector3(0, 0, transform.rotation.z - 90));
            transform.GetChild(0).Rotate(new Vector3(0, 0, transform.rotation.z + 90));
            if (currentDir > 3)
            {
                currentDir = 0;
                transform.Rotate(Vector3.zero);
                transform.GetChild(0).Rotate(Vector3.zero);
            }
            spriteRenderer.sprite = buildingSprites[currentDir];
        }
    }
}
