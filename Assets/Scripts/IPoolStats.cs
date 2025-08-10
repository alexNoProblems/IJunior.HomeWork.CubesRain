using System;

public interface IPoolStats
{
    event Action StatsChanged;

    int CreatedCount { get; }
    int SpawnedEverCount { get; }
    int ActiveCount { get; }
}
