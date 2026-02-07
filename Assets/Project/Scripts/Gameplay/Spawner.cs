using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
 [SerializeField] private GameObject _deadMarshmallowPrefab;
 [SerializeField] private float _spawnInterval = 3f;
 [SerializeField] private BoxCollider cupBounds;
 
 
 private Coroutine _spawnRoutine;
 
 public static Spawner Instance;

 private void Awake()
 {
     Instance = this;
 }
 
    void Start()
    {
        StartSpawning();
        
    }

    private void StartSpawning()
    {
        if (_spawnRoutine == null)
            _spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private void StopSpawning()
    {
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine); 
        _spawnRoutine = null;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnMarshmallow();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
    
    private void SpawnMarshmallow()
    {
        Vector3 spawnPosition = GetRandomPointInBounds(cupBounds.bounds);
     Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        Instantiate(_deadMarshmallowPrefab, spawnPosition, randomRotation);
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),bounds.max.y,Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public void SetSpawnInterval(float value)
    {
        _spawnInterval = value;
        
        StopSpawning();
        StartSpawning();
    }
    public float GetSpawnInterval()
    {
        return _spawnInterval;
    }
    
}
