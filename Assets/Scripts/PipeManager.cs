using System;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private PipeSpawnConfig m_pipeSpawnConfig;

    private float timer;
    private bool isActive;

    private void Start()
    {
        EventBus.OnGameStart += HandleGameStart;
        EventBus.OnGameOver += HandleGameOver;
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
    }

    private void SpawnPipes(PipeContainer pipePrefab)
    {
        Instantiate(pipePrefab, transform.position, Quaternion.identity);
    }
}
