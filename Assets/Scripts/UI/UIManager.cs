using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton Init
    private static UIManager _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static UIManager Instance // Init not in order
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
        _instance = FindObjectOfType<UIManager>();
        _instance.Initialize();
    }
    #endregion

    [Header("Buttons")]
    public Button cameraMoveAtCueenButton;
    public Button alarmButton;
    public Button patrolButton;
    public Button addCeggButton;
    public Button openBuildingPanelButton;
    public Button openMiningPanelButton;
    public Button openTasksPanelButton;
    public Button pauseMenuButton;

    [Header("ResourcesTexts")]
    public Text woodyText;
    public Text stoneyText;
    public Text metalyText;
    public Text mucusText;
    public Text meatText;
    public Text dungInvitingText;

    [Header("Panels")]
    public PausePanel pausePanel;
    public BuildingsPanel buildingsPanel;
    public MiningPanel miningPanel;
    public TasksPanel tasksPanel;

    [Header("Animators")]
    private Animator pausePanelAnimator;
    private Animator buildingsPanelAnimator;
    private Animator miningPanelAnimator;
    private Animator taskPanelAnimator;

    private void Initialize()
    {
        ButtonsSetup();
        AnimatorsSetup();
        //gameObject.hideFlags = HideFlags.HideInHierarchy;
    }

    void Update()
    {

    }

    public void RefreshInfo()
    {
        woodyText.text = $"{Data.Instance.Woody}";
        stoneyText.text = $"{Data.Instance.Stoney}";
        metalyText.text = $"{Data.Instance.Metaly}";
        mucusText.text = $"{Data.Instance.Mucus}";
        meatText.text = $"{Data.Instance.Meat}";
        dungInvitingText.text = $"{Data.Instance.DungInviting}";
    }

    private void AnimatorsSetup()
    {
        pausePanelAnimator = pausePanel.GetComponent<Animator>();
        buildingsPanelAnimator = buildingsPanel.GetComponent<Animator>();
        miningPanelAnimator = miningPanel.GetComponent<Animator>();
        taskPanelAnimator = tasksPanel.GetComponent<Animator>();
    }

    private void ButtonsSetup()
    {
        cameraMoveAtCueenButton.onClick.RemoveAllListeners();
        alarmButton.onClick.RemoveAllListeners();
        patrolButton.onClick.RemoveAllListeners();
        addCeggButton.onClick.RemoveAllListeners();
        openBuildingPanelButton.onClick.RemoveAllListeners();
        openMiningPanelButton.onClick.RemoveAllListeners();
        openTasksPanelButton.onClick.RemoveAllListeners();
        pauseMenuButton.onClick.RemoveAllListeners();

        cameraMoveAtCueenButton.onClick.AddListener(() => { Camera.main.GetComponent<CameraController>().MoveAtCueen(); });
        alarmButton.onClick.AddListener(() => { SceneController.Instance.Alarm(); });
        patrolButton.onClick.AddListener(() => { TasksManager.Instance.AddTask(Task.TaskType.Wander); });
        addCeggButton.onClick.AddListener(() => { SceneController.Instance.cueen.ceggsManager.CreateCegg(); });
        openBuildingPanelButton.onClick.AddListener(() => { BuildingsPanelToggle(); });
        openMiningPanelButton.onClick.AddListener(() => { MiningPanelToggle(); });
        openTasksPanelButton.onClick.AddListener(() => { TaskPanelToggle(); });
        pauseMenuButton.onClick.AddListener(() => { PausePanelToggle(); });
    }

    public void BuildingsPanelToggle(bool _hide = false)
    {
        if (_hide)
        {
            buildingsPanelAnimator.SetBool("IsShow", false);
            if(buildingsPanel.isShow)
                buildingsPanel.ShowToggle();
            return;
        }
        buildingsPanelAnimator.SetBool("IsShow", !buildingsPanelAnimator.GetBool("IsShow"));
        buildingsPanel.ShowToggle();
    }

    public void MiningPanelToggle(bool _hide = false)
    {
        if (_hide)
        {
            miningPanelAnimator.SetBool("IsShow", false);
            return;
        }
        miningPanelAnimator.SetBool("IsShow", !miningPanelAnimator.GetBool("IsShow"));
    }

    public void TaskPanelToggle(bool _hide = false)
    {
        if (_hide)
        {
            taskPanelAnimator.SetBool("IsShow", false);
            return;
        }
        taskPanelAnimator.SetBool("IsShow", !taskPanelAnimator.GetBool("IsShow"));
    }

    public void PausePanelToggle(bool _hide = false)
    {
        if (_hide)
        {
            pausePanelAnimator.SetBool("IsShow", false);
            return;
        }
        pausePanelAnimator.SetBool("IsShow", !pausePanelAnimator.GetBool("IsShow"));
    }

    public void HideAll()
    {
        BuildingsPanelToggle(true);
        MiningPanelToggle(true);
        TaskPanelToggle(true);
    }
}
