using System;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private PipeSpawnConfig m_pipeSpawnConfig;

    private float timer;
    private bool isActive;

    private readonly List<PipeContainer> pipeContainers = new();

    private void Start()
    {
        EventBus.OnRestart += HandleGameRestart;
        EventBus.OnGameStart += HandleGameStart;
        EventBus.OnGameOver += HandleGameOver;
    }

    private void HandleGameRestart()
    {
        timer = 0f;
        isActive = false;

        foreach (PipeContainer pc in pipeContainers)
        {
            Destroy(pc.gameObject);
        }

        pipeContainers.Clear();
    }

    private void HandleGameStart()
    {
        timer = 0f;
        isActive = true;
    }

    private void HandleGameOver()
    {
        isActive = false;
    }

    private void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;

        if (timer >= m_pipeSpawnConfig.SpawnInterval)
        {
            timer = 0f;
            SpawnPipes(m_pipeSpawnConfig.PipeContainerPrefab);
        }

        for (int i = 0; i < pipeContainers.Count; i++)
        {
            var pc = pipeContainers[i];
            if (Utils.IsReachedBoundaryX(pc.TopPipe.GetBounds().max.x, true))
            {
                // TODO: IMPLEMENT POOLING SYSTEM
                Destroy(pc.gameObject);
                pipeContainers.Remove(pc);
            }
        }
    }

    private void SpawnPipes(PipeContainer pipePrefab)
    {
        var pc = Instantiate(pipePrefab, transform.position, Quaternion.identity);
        pipeContainers.Add(pc);
    }

    private void OnDestroy()
    {
        EventBus.OnRestart -= HandleGameRestart;
        EventBus.OnGameStart -= HandleGameStart;
        EventBus.OnGameOver -= HandleGameOver;
    }
}
