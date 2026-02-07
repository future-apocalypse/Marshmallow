using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 public int jumpCount;
 public float survivalTime;
 public static GameManager Instance;

 [SerializeField] private float baseSinkTime = 5f;
 [SerializeField] private float baseSpawnInterval = 3f;
 private int _difficultyTier;
 
 private void Awake()
 {
  Instance = this;
 }

 private void Update()
 {
  survivalTime -= Time.deltaTime;
 }
 public void RegisterJump()
 {
  jumpCount++;
  
  int newTier = jumpCount / 10;
  if (newTier > _difficultyTier)
  {
   _difficultyTier = newTier;
   ApplyDifficulty();
  }
 }

 private void ApplyDifficulty()
 {
  float sinkTime = Mathf.Max(1f, baseSinkTime - _difficultyTier * 1f);
  float spawnInterval = Mathf.Max(0.5f, baseSpawnInterval - _difficultyTier * 0.3f);
  
  PlatformManager.Instance.SetSinkTime(sinkTime);
  Spawner.Instance.SetSpawnInterval(spawnInterval);
 }
}
