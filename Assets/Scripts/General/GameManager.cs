using Better_FPS_Controller.Scripts.Player;
using UnityEngine;

namespace General
{
    public class GameManager : MonoBehaviour
    {
        public static GameObject PlayerGameObject;
        void Awake()
        {
            PlayerGameObject = FindObjectOfType<PlayerMovement>().gameObject;
        }
    }
}
