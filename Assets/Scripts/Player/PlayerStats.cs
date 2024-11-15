using System;
using System.Collections;
using General;
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
        [SerializeField] private TextMeshProUGUI waveDisplay;
        
        [Header("Internal Variables")]
        [SerializeField] private float immunityTime = 2.0f;
        [SerializeField] private float health = 10.0f;
        
        [Header("SFX")] [SerializeField] private AudioSource audioSource;
        
        [HideInInspector] public int money;
        private bool _immune;
        [HideInInspector] public bool interactable;
        
        public void _Update()
        {
            if (health <= 0.0f) PlayerDeath();

            healthDisplay.text = $"Health:{(int)health}";
            moneyDisplay.text = $"Money:{money}";
            interactionDisplay.gameObject.SetActive(interactable);
            waveDisplay.text = $"Wave:{GameManager.CurrentWaveIndex}";
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
                audioSource.Play();
                StartCoroutine(ImmunityTimer());
            }
        }

        public void PlayerDeath()
        {
            // TODO: could add sfx here and maybe a time delay
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
