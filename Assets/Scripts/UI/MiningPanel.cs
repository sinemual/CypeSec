using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningPanel : MonoBehaviour
{
    public Button woodyMiningButton;
    public Button stoneyMiningButton;
    public Button metalyMiningButton;
    public Button meatyMiningButton;

    private void Start()
    {
        ButtonsSetup();
    }

    private void ButtonsSetup()
    {
        woodyMiningButton.onClick.RemoveAllListeners();
        stoneyMiningButton.onClick.RemoveAllListeners();
        metalyMiningButton.onClick.RemoveAllListeners();
        meatyMiningButton.onClick.RemoveAllListeners();

        woodyMiningButton.onClick.AddListener(() => { TasksManager.Instance.AddTask(Task.TaskType.Mining, Resource.Type.Woody); });
        stoneyMiningButton.onClick.AddListener(() => { TasksManager.Instance.AddTask(Task.TaskType.Mining, Resource.Type.Stoney); });
        metalyMiningButton.onClick.AddListener(() => { TasksManager.Instance.AddTask(Task.TaskType.Mining, Resource.Type.Metaly); });
        meatyMiningButton.onClick.AddListener(() => { TasksManager.Instance.AddTask(Task.TaskType.Mining, Resource.Type.Meat); });
    }
}
