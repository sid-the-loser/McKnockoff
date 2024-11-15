using System;
using General;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyStats : MonoBehaviour
    {

        [SerializeField] private float health = 10.0f;
        [SerializeField] private int moneyDrop;
        [SerializeField] private float hitDamage = 1.0f;
        [SerializeField] private float minimumHitDistance = 2;
        [SerializeField] private AudioSource audioSource;

        private PlayerStats _player;
        
        private Transform _playerTransform;
        private Vector3 _pastLocation;
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _playerTransform = GameManager.PlayerGameObject.transform;
            _player = GameManager.PlayerGameObject.GetComponent<PlayerStats>();
            GameManager.EnemyCount++;
        }

        private void Update()
        {
            Vector3 position;

            if (!GlobalVariables.GamePaused)
            {
                position = _playerTransform.position;
            }
            else
            {
                position = transform.position;
            }

            if (_pastLocation != position)
            {
                _agent.SetDestination(position);
                _pastLocation = position;
            }
            
            if (!GlobalVariables.GamePaused)
            {
                if (GlobalVariables.FastDistanceCheck(_player.gameObject.transform.position, transform.position,
                        minimumHitDistance))
                {
                    _player.Damage(hitDamage);
                }
            }
        
        }

        public void GotHit(float damage)
        {
            health -= damage;
            if (health <= 0.0f)
            {
                _player.money += moneyDrop;
                audioSource.Play();
                GameManager.EnemyCount--;
                Destroy(gameObject);
            }
        }
    }
}
