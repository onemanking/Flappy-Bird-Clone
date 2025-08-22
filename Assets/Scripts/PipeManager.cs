using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private PipeSpawnConfig m_pipeSpawnConfig;

    private float timer;
    private bool isActive;

    private readonly List<PipeContainer> activePipeContainers = new();
    private IObjectPool<PipeContainer> pipePool;

    private void Start()
    {
        pipePool = PoolManager.Instance.GetPool(m_pipeSpawnConfig.PipeContainerPrefab);

        EventBus.OnRestart += HandleGameRestart;
        EventBus.OnGameStart += HandleGameStart;
        EventBus.OnGameOver += HandleGameOver;
    }

    private void HandleGameRestart()
    {
        timer = 0f;
        isActive = false;

        foreach (var pc in activePipeContainers)
        {
            pipePool.Release(pc);
        }

        activePipeContainers.Clear();
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
            SpawnPipes();
        }

        for (int i = activePipeContainers.Count - 1; i >= 0; i--)
        {
            var pc = activePipeContainers[i];
            if (Utils.IsReachedBoundaryX(pc.TopPipe.GetBounds().max.x, true))
            {
                activePipeContainers.RemoveAt(i);
                pipePool.Release(pc);
            }
        }
    }

    private void SpawnPipes()
    {
        var pc = pipePool.Get();
        pc.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        activePipeContainers.Add(pc);
    }

    private void OnDestroy()
    {
        EventBus.OnRestart -= HandleGameRestart;
        EventBus.OnGameStart -= HandleGameStart;
        EventBus.OnGameOver -= HandleGameOver;
    }
}
