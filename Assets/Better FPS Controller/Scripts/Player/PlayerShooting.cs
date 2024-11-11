using Better_FPS_Controller.Scripts.General;
using TMPro;
using UnityEngine;

// todo: rework required

namespace Better_FPS_Controller.Scripts.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject headObject;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private TextMeshProUGUI crossHair;

        private void Update()
        {
            RaycastHit hit;
            var bro = Physics.Raycast(headObject.transform.position,
                headObject.transform.TransformDirection(Vector3.forward),
                out hit, Mathf.Infinity, layerMask);
            if (bro)
            {
                crossHair.color = Color.red;
                crossHair.fontStyle = FontStyles.Underline;
                if (InputManager.RangedAttackPressed) Destroy(hit.collider.gameObject);
            }
            else
            {
                crossHair.color = Color.grey;
                crossHair.fontStyle = FontStyles.Normal;
            }
        }
    }
}