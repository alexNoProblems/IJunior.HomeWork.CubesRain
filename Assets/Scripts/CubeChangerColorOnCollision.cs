using UnityEngine;

[RequireComponent(typeof(Cube), typeof(CubeLifetimeCount))]
public class CubeChangerColorOnCollision : MonoBehaviour
{
    private Cube _cube;
    private CubeLifetimeCount _lifetimeCount;
    private bool _hasCollided = false;

    private string _platformTag = "Platform";

    private void Awake()
    {
        _cube = GetComponent<Cube>();
        _lifetimeCount = GetComponent<CubeLifetimeCount>();
    }

    private void OnEnable()
    {
        _hasCollided = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == true) 
            return;
        
        if (collision.gameObject.CompareTag(_platformTag))
        {
            _hasCollided = true;
            Color newColor = _cube.Colorizer.GenerateRandomColor();
            _cube.SetColor(newColor);
            _lifetimeCount.StartLifetimeCountdown();
        }
    }
}
