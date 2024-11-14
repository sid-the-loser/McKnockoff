using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Displaying Details")] 
        [SerializeField] private TextMeshProUGUI healthDisplay;
        [SerializeField] private TextMeshProUGUI moneyDisplay;
        [SerializeField] private TextMeshProUGUI interactionDisplay;
        
        [Header("Internal Variables")]
        [SerializeField] private float immunityTime = 2.0f;
        [SerializeField] private float health = 100.0f;
        private int _money;
        private bool _immune;
        private bool _interactable;
        
        private void Update()
        {
            if (health <= 0.0f) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            healthDisplay.text = $"Health:{health}";
            moneyDisplay.text = $"Money:{_money}";
            interactionDisplay.gameObject.SetActive(_interactable);
        }

        private IEnumerator ImmunityTimer()
        {
            _immune = true;
            yield return new WaitForSeconds(immunityTime);
            _immune = false;
        }
        
        public void Damage(float amount)
        {
            if (!_immune)
            {
                health -= amount;
                StartCoroutine(ImmunityTimer());
            }
        }
        
        public void AddMoney(int amount)
        {
            _money += amount;
        }

        public int GetMoney()
        {
            return _money;
        }
    }
}
