using System;
using Enemy;
using TMPro;
using UnityEngine;

namespace Barricades
{
    public class Barricade : MonoBehaviour
    {
        public int unlockMoney;
        [SerializeField] private TextMeshPro[] moneyDisplays;
        [SerializeField] private EnemySpawnPoints[] enemySpawnPointsArray; 

        private void Start()
        {
            foreach (var moneyDisplay in moneyDisplays)
            {
                moneyDisplay.text = $"${unlockMoney}";
            }
        }

        public bool TryBuy(int amount)
        {
            var x = unlockMoney - amount;
            if (x <= 0)
            {
                foreach (var spawnPoint in enemySpawnPointsArray)
                {
                    spawnPoint.Unlock();
                }
                return true;
            }
            
            return false;
        }

        public void RemoveMyself()
        {
            Destroy(gameObject);
            /*foreach (var moneyDisplay in moneyDisplays)
            {
                moneyDisplay.text = "";
            }
            gameObject.SetActive(false);*/
        }
    }
}
