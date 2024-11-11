using System;
using General;
using UnityEngine;
using UnityEngine.AI;

namespace Test
{
    public class TestDynamicEnemy : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        private Vector3 _pastLocation;
        private NavMeshAgent _agent;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _player = GameManager.PlayerGameObject.transform;
        }

        private void Update()
        {
            Vector3 position;
            
            if (!GlobalVariables.GamePaused)
            {
                position = _player.position;
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
            
        }
    }
}
