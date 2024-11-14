using System;
using System.Collections.Generic;
using Enemy;
using Player;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace General
{
    public class GameManager : MonoBehaviour
    {
        [Header("Wave data and Spawn points")]
        [SerializeField] private Vector3[] waveData;
        [SerializeField] private EnemySpawnPoints[] spawnPoints;

        private int _currentWave;
        
        public static GameObject PlayerGameObject;
        void Awake()
        {
            PlayerGameObject = FindObjectOfType<PlayerMovement>().gameObject;
            if (PlayerGameObject is null) throw new SystemException("No player found!");
        }
    }
}
