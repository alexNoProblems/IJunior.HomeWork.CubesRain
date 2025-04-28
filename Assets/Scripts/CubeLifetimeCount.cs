using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Cube))]
public class CubeLifetimeCount : MonoBehaviour
{
    private Cube _cube;
    private bool _isCountingLifetime = false;
    private CubeFactory _factory;
    private Coroutine _lifeTimeCoroutine;

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnEnable()
    {
        _isCountingLifetime = false;
        
        if (_lifeTimeCoroutine != null)
        {
            StopCoroutine(_lifeTimeCoroutine);
            _lifeTimeCoroutine = null;
        }
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

        _lifeTimeCoroutine = StartCoroutine(LifetimeCoroutine(lifeTime));
    }

    private IEnumerator LifetimeCoroutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        if (_factory != null)
            _factory.ReturnCube(_cube);
        else
            Destroy(gameObject);

        _lifeTimeCoroutine = null;
    }
}
