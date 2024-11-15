using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trashcans
{
    public class TrashcanLogic : MonoBehaviour
    {
        public int moneyToOpen;
        [SerializeField] private TextMeshPro moneyDisplay;

        private void Start()
        {
            moneyDisplay.text = $"${moneyToOpen}";
        }

        public int TryBuy(int amount)
        {
            var x = moneyToOpen - amount;
            if (x <= 0)
            {
                return Random.Range(1, 3);
            }
            
            return -1;
        }
        
        public void RemoveMyself()
        { 
            Destroy(gameObject);
        }
    }
}
