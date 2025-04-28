using UnityEngine;
using System.Collections.Generic;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolCapacity = 10;

    private Queue<Cube> _availableCubes = new Queue<Cube>();

    private void Awake()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            Cube cube = Instantiate(_cubePrefab);
            cube.gameObject.SetActive(false);
            _availableCubes.Enqueue(cube);
        }
    }

    public Cube GetCube()
    {
        Cube cube;

        if(_availableCubes.Count > 0)
            cube = _availableCubes.Dequeue();
        else
            cube = Instantiate(_cubePrefab);

        cube.gameObject.SetActive(true);

        return cube;
    }

    public void ReturnCube(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _availableCubes.Enqueue(cube);
    }
}
