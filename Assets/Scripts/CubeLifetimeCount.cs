using UnityEngine;
using System.Collections;
using System;

public class CubeLifetimeCount : MonoBehaviour
{
    private bool _isCountingLifetime = false;
    private Coroutine _lifeTimeCoroutine;
    public event Action LifetimeEnded;
    
    private void OnEnable()
    {
        _isCountingLifetime = false;
        
        if (_lifeTimeCoroutine != null)
        {
            StopCoroutine(_lifeTimeCoroutine);
            _lifeTimeCoroutine = null;
        }
    }

    public void StartLifetimeCountdown()
    {
        float minLifetime = 2f;
        float maxLifetime = 5f;
        float lifeTime = UnityEngine.Random.Range(minLifetime, maxLifetime);

        if (_isCountingLifetime)
            return;

        _isCountingLifetime = true;
        _lifeTimeCoroutine = StartCoroutine(LifetimeCoroutine(lifeTime));
    }

    private IEnumerator LifetimeCoroutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        LifetimeEnded?.Invoke();

        _isCountingLifetime = false;
        _lifeTimeCoroutine = null;
    }
}
