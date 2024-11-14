using UnityEngine;

namespace Enemy
{
    public class EnemyStats : MonoBehaviour
    {

        [SerializeField] private float health = 10.0f;

        public void GotHit(float damage)
        {
            health -= damage;
            if (health <= 0.0f) Destroy(gameObject);
        }

    }
}
