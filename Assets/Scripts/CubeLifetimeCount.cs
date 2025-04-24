using UnityEngine;


[RequireComponent(typeof(Cube))]
public class CubeLifetimeCount : MonoBehaviour
{
    private Cube _cube;
    private bool _isCountingLifetime = false;
    private CubeFactory _factory;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnEnable()
    {
        _isCountingLifetime = false;
        CancelInvoke();
    }

    public void Initialize(CubeFactory factory)
    {
        _factory = factory;
    }

    public void StartLifetimeCountdown()
    {
        float minLifetime = 2f;
        float maxLifetime = 5f;
        float lifeTime = Random.Range(minLifetime, maxLifetime);

        if (_isCountingLifetime == true)
            return;
        
        _isCountingLifetime = true;

        Invoke(nameof(ReturnToPool), lifeTime);
    }

    private void ReturnToPool()
    {
        if (_factory != null)
            _factory.ReturnCube(_cube);
        else
            Destroy(gameObject);
    }
}
