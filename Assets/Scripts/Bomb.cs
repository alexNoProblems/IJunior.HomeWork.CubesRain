using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _minFadeTime = 2f;
    [SerializeField] private float _maxFadeTime = 5f;
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 5f;

    private Material _materialInstance;
    private Renderer _renderer;
    private Color _originalColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        InitMaterial();
    }

    private void Start()
    {
        float fadeDuration = Random.Range(_minFadeTime, _maxFadeTime);

        StartCoroutine(FadeAndExplode(fadeDuration));
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
