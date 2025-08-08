using UnityEngine;
using TMPro;

public class SpawnerStatisticsUI : MonoBehaviour
{
    [SerializeField] private GenericPool<Cube> _cubePool;
    [SerializeField] private GenericPool<Bomb> _bombPool;
    [SerializeField] private TMP_Text _cubeSpawnedEverText;
    [SerializeField] private TMP_Text _cubeCreatedText;
    [SerializeField] private TMP_Text _cubeActiveText;
    [SerializeField] private TMP_Text _bombSpawnedEverText;
    [SerializeField] private TMP_Text _bombCreatedText;
    [SerializeField] private TMP_Text _bombActiveText;
    [SerializeField] private float _updateInterval = 0.25f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _updateInterval)
            return;

        _timer = 0f;

        if (_cubePool != null)
        {
            _cubeSpawnedEverText.text = _cubePool.SpawnedEverCount.ToString();
            _cubeCreatedText.text = _cubePool.CreatedCount.ToString();
            _cubeActiveText.text = _cubePool.ActiveCount.ToString();
        }

        if (_bombPool != null)
        {
            _bombSpawnedEverText.text = _bombPool.SpawnedEverCount.ToString();
            _bombCreatedText.text = _bombPool.CreatedCount.ToString();
            _bombActiveText.text = _bombPool.ActiveCount.ToString();
        }        
    }
}
