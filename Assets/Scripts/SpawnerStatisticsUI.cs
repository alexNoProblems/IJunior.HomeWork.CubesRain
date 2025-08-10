using UnityEngine;
using TMPro;

public class SpawnerStatisticsUI : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _poolBehavior;
    [SerializeField] private TMP_Text _spawnedEverText;
    [SerializeField] private TMP_Text _createdText;
    [SerializeField] private TMP_Text _activeText;

    private IPoolStats _pool;

    private void Awake()
    {
        _pool = _poolBehavior as IPoolStats;
    }

    private void OnEnable()
    {
        if (_pool == null)
            return;

        _pool.StatsChanged += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        if (_pool == null)
            return;

        _pool.StatsChanged -= Refresh;
    }

    private void Refresh()
    {
        if (_spawnedEverText)
            _spawnedEverText.text = _pool.SpawnedEverCount.ToString();
        if (_createdText)
            _createdText.text = _pool.CreatedCount.ToString();
        if (_activeText)
            _activeText.text = _pool.ActiveCount.ToString();
    }
}
