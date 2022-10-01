using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    private GameObject[] fillers = new GameObject[5];
    private float timer;
    private bool processing;
    private float processTime;
    private float process;
    private SpriteRenderer spriteRenderer;
    private string sound;

    private WorkData workData;

    private void Awake()
    {
        workData = transform.parent.GetComponent<WorkData>();
        workData.OnWorkStart += ToggleOn;
        sound = workData.sound;
        processTime = workData.workingTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (processing)
        {
            timer += Time.deltaTime;
            process = timer / processTime;
            if (process >= 0.15f) Add(0);
            if (process >= 0.35f) Add(1);
            if (process >= 0.55f) Add(2);
            if (process >= 0.75f) Add(3);
            if (process >= 0.95f) Add(4);
            if (timer >= processTime)
            {
                processing = false;
                for (int i = 0; i < fillers.Length; i++)
                    fillers[i].SetActive(false);
                timer = 0;
                spriteRenderer.enabled = false;
            }
        }
    }

    public void ToggleOn()
    {
        timer = 0;
        spriteRenderer.enabled = true;
        for (int i = 0; i < fillers.Length; i++)
            fillers[i] = transform.GetChild(i).gameObject;
        processing = true;
    }

    private void Add(int _num)
    {
        if (!fillers[_num].activeSelf)
        {
            fillers[_num].SetActive(true);
            AudioManager.Instance.Play(sound);
        }
    }
}
