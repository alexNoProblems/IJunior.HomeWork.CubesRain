using UnityEngine;

[RequireComponent(typeof(CubeColorizer), typeof(Rigidbody), typeof (CubeLifetimeCount))]
public class Cube : MonoBehaviour
{
    public CubeColorizer Colorizer {get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public CubeLifetimeCount LifetimeCount { get; private set; }

    private bool _hasCollided = false;

    private void Awake()
    {
        Colorizer = GetComponent<CubeColorizer>();
        Rigidbody = GetComponent<Rigidbody>();
        LifetimeCount = GetComponent<CubeLifetimeCount>();
    }

    private void OnEnable()
    {
        _hasCollided = false;
    }

    public void SetColor(Color color)
    {
        Colorizer.SetColor(color);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided == true) 
            return;
        
        if (collision.gameObject.TryGetComponent<PlatformTag>(out PlatformTag platformTag))
        {
            _hasCollided = true;
            Color newColor = Colorizer.GenerateRandomColor();
            SetColor(newColor);
            LifetimeCount.StartLifetimeCountdown();
        }
    }
}
