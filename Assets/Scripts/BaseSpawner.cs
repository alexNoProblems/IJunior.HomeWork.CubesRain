using UnityEngine;

public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    protected abstract void Spawn();    
}
