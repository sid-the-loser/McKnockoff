using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace General
{
    public class GameManager : MonoBehaviour
    {
        [Header("Wave data")]
        [SerializeField] private Vector3[] waveData;
        
        [Header("Enemy Types")]
        [SerializeField] private GameObject smallEnemyPrefab;
        [SerializeField] private float smallEnemyYOffset;
        [SerializeField] private GameObject mediumEnemyPrefab;
        [SerializeField] private float mediumEnemyYOffset;
        [SerializeField] private GameObject largeEnemyPrefab;
        [SerializeField] private float largeEnemyYOffset;
        
        public static int CurrentWaveIndex;
        
        public static GameObject PlayerGameObject;
        public static int EnemyCount;

        private EnemySpawnPoints[] _spawnPoints;

        private readonly float _waveCheckCooldownSeconds = 5.0f;
        private bool _waveCheckCooldown;
        
        private void Awake()
        {
            GlobalVariables.GamePaused = false;
            PlayerGameObject = FindObjectOfType<PlayerMovement>().gameObject;
            EnemyCount = 0;
            CurrentWaveIndex = 0;

            _spawnPoints = FindObjectsOfType<EnemySpawnPoints>();
            
            if (PlayerGameObject is null) throw new SystemException("No player found!");
        }

        private void Update()
        {
            if (!_waveCheckCooldown && !GlobalVariables.GamePaused)
            {
                if (EnemyCount <= 0)
                {
                    if (CurrentWaveIndex >= waveData.Length)
                    {
                        InputManager.ToggleMouseCapture(false);
                        SceneManager.LoadScene("Main menu");
                    }
                    else
                    {
                        SpawnEnemies(CurrentWaveIndex);
                        CurrentWaveIndex++;
                    }
                }
                StartCoroutine(StartWaveCoolDown());
            }
        }

        private IEnumerator StartWaveCoolDown()
        {
            _waveCheckCooldown = true;
            yield return new WaitForSeconds(_waveCheckCooldownSeconds);
            _waveCheckCooldown = false;
        }

        private void SpawnEnemies(int waveIndex)
        {
            // print("Spawing");
            
            var activeSpawnPoints = new List<EnemySpawnPoints>();
            foreach (var spawnPoint in _spawnPoints)
            {
                if (!spawnPoint.locked) activeSpawnPoints.Add(spawnPoint);
            }
            
            var currWaveData = waveData[waveIndex];

            if (activeSpawnPoints.Count > 0)
            {
                #region small enemies

                for (int i = 0; i < currWaveData.x; i++)
                {
                    var chosenSpawnPoint = activeSpawnPoints[Random.Range(0, activeSpawnPoints.Count)];

                    Instantiate(smallEnemyPrefab,
                        chosenSpawnPoint.transform.position + (Vector3.up * smallEnemyYOffset),
                        chosenSpawnPoint.transform.rotation);
                }

                #endregion
                
                #region medium enemies

                for (int i = 0; i < currWaveData.y; i++)
                {
                    var chosenSpawnPoint = activeSpawnPoints[Random.Range(0, activeSpawnPoints.Count)];

                    Instantiate(mediumEnemyPrefab,
                        chosenSpawnPoint.transform.position + (Vector3.up * mediumEnemyYOffset),
                        chosenSpawnPoint.transform.rotation);
                }

                #endregion
                
                #region large enemies

                for (int i = 0; i < currWaveData.z; i++)
                {
                    var chosenSpawnPoint = activeSpawnPoints[Random.Range(0, activeSpawnPoints.Count)];

                    Instantiate(largeEnemyPrefab,
                        chosenSpawnPoint.transform.position + (Vector3.up * largeEnemyYOffset),
                        chosenSpawnPoint.transform.rotation);
                }

                #endregion
            }
        }
    }
}
