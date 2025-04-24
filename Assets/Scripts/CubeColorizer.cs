using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeColorizer : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = new Material(_renderer.material);       
    }

    public Color GenerateRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}
