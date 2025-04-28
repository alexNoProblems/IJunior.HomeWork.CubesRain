using UnityEngine;

[RequireComponent(typeof(CubeColorizer), typeof(Rigidbody), typeof (CubeLifetimeCount))]
public class Cube : MonoBehaviour
{
    public CubeColorizer Colorizer {get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public CubeLifetimeCount LifetimeCount { get; private set; }

    private CubePool _factory;
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

        LifetimeCount.LifetimeEnded += HandleLifetimeEnded;
    }

    private void OnDisable()
    {
        LifetimeCount.LifetimeEnded -= HandleLifetimeEnded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCollided) 
            return;
        
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            _hasCollided = true;
            Color newColor = Colorizer.GenerateRandomColor();
            SetColor(newColor);
            LifetimeCount.StartLifetimeCountdown();
        }
    }

    public void InitializeFactory(CubePool factory)
    {
        _factory = factory;
    }

    public void SetColor(Color color)
    {
        Colorizer.SetColor(color);
    }

    private void HandleLifetimeEnded()
    {
        if (_factory != null)
            _factory.ReturnCube(this);
        else
            Destroy(gameObject);
    }
}
