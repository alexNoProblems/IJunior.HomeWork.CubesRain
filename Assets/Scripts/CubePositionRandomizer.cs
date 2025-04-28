using UnityEngine;

public class CubePositionRandomizer : MonoBehaviour
{
    [SerializeField] private Transform _platform;
    [SerializeField] private float _spawnHeight = 11f;

    private Vector3 _platformSize;

    private void Awake()
    {
        Renderer renderer = _platform.GetComponent<Renderer>();

        if (renderer != null)
            _platformSize = renderer.bounds.size;
    }
    
    public Vector3 GetRandomPositionAbovePlatform()
    {
        Vector3 platformCenter = _platform.position;

        float halfOfPlatformSizeX = _platformSize.x / 2;
        float halfOfPlatformSizeZ = _platformSize.z / 2;

        float randomX = Random.Range(platformCenter.x - halfOfPlatformSizeX, platformCenter.x + halfOfPlatformSizeX);
        float randomZ = Random.Range(platformCenter.z - halfOfPlatformSizeZ, platformCenter.z + halfOfPlatformSizeZ);
        float y = platformCenter.y + _spawnHeight;

        return new Vector3(randomX, y, randomZ);
    }
}
