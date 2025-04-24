using UnityEngine;

[RequireComponent(typeof(CubeColorizer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public CubeColorizer Colorizer {get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Colorizer = GetComponent<CubeColorizer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void SetColor(Color color)
    {
        Colorizer.SetColor(color);
    }
}
