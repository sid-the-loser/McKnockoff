using System;
using Barricades;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemySpawnPoints : MonoBehaviour
    {
        public bool locked = true;

        public void SpawnEnemies()
        {
            if (!locked) Debug.Log("Yay!");
        }

        public void Unlock()
        {
            locked = false;
            // Debug.Log("Spawn point Unlocked!");
        }
    }
}
