using UnityEngine;
using System;

[RequireComponent(typeof(CubeColorizer), typeof(Rigidbody), typeof (CubeLifetimeCount))]
public class Cube : MonoBehaviour
{
    public CubeColorizer Colorizer {get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public CubeLifetimeCount LifetimeCount { get; private set; }

    private GenericPool<Cube> _factory;
    private bool _hasCollided = false;

    public event Action<Vector3> CubeDestroyed;

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
        CubeDestroyed = null;
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

    public void Initialize(GenericPool<Cube> factory, Color color)
    {
        _factory = factory;
        SetColor(color);
    }

    public void SetColor(Color color)
    {
        Colorizer.SetColor(color);
    }

    private void HandleLifetimeEnded()
    {
        CubeDestroyed?.Invoke(transform.position);

        if (_factory != null)
            _factory.Return(this);
        else
            throw new System.InvalidOperationException("Cube: Factory не инициализирована. Невозможно вернуть cube в пул!!!");
    }
}
