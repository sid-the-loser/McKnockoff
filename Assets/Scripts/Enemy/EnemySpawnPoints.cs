using Barricades;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawnPoints : MonoBehaviour
    {
        public bool active;
        [SerializeField] private Barricade[] barricades;
    }
}
