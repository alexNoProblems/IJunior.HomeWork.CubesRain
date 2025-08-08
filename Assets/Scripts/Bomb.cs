using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _minFadeTime = 2f;
    [SerializeField] private float _maxFadeTime = 5f;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _returnDelay = 0.5f;

    private GenericPool<Bomb> _pool; 
    private Material _materialInstance;
    private Renderer _renderer;
    private Color _originalColor;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _waitForSeconds = new WaitForSeconds(_returnDelay);

        InitMaterial();
    }

    private void OnEnable()
    {
        ResetColorAlpha();

        float fadeDuration = Random.Range(_minFadeTime, _maxFadeTime);
        StartCoroutine(FadeAndExplode(fadeDuration));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Init(GenericPool<Bomb> pool)
    {
        _pool = pool;
    }

    private void ResetColorAlpha()
    {
        if (_materialInstance != null)
        {
            Color startColor = Color.black;
            startColor.a = 1f;
            _materialInstance.color = startColor;
            _originalColor = startColor;
        }
    }

    private IEnumerator FadeAndExplode(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            Color newColor = _originalColor;
            newColor.a = alpha;
            _materialInstance.color = newColor;

            yield return null;
        }

        Explode();

        yield return _waitForSeconds;

        if (_pool != null)
            _pool.Return(this);
        else
            Destroy(gameObject);
    }

    private void InitMaterial()
    {
        _materialInstance = _renderer.material;
        _originalColor = _materialInstance.color;
        _renderer.material = _materialInstance;
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Cube cube))
            {
                if (cube.TryGetComponent(out Rigidbody rigidbody))
                {
                    Vector3 direction = (collider.transform.position - transform.position).normalized;
                    rigidbody.AddForce(direction * _explosionForce, ForceMode.Impulse);
                }
            }
        }
    }
}
