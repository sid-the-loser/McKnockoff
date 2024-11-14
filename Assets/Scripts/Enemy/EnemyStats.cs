using System;
using General;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats : MonoBehaviour
    {

        [SerializeField] private float health = 10.0f;
        [SerializeField] private int moneyDrop;
        [SerializeField] private float hitDamage = 1.0f;
        [SerializeField] private float minimumHitDistance = 2;

        private PlayerStats _player;

        private void Start()
        {
            _player = GameManager.PlayerGameObject.GetComponent<PlayerStats>();
        }

        private void Update()
        {
            if (!GlobalVariables.GamePaused && 
                GlobalVariables.FastDistanceCheck(_player.gameObject.transform.position, transform.position, 
                    minimumHitDistance))
            {
                _player.Damage(hitDamage);
            }
        }

        public void GotHit(float damage)
        {
            health -= damage;
            if (health <= 0.0f)
            {
                _player.AddMoney(moneyDrop);
                Destroy(gameObject);
            }
        }
    }
}
