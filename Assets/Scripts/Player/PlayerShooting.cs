using System;
using System.Collections.Generic;
using Enemy;
using General;
using TMPro;
using UnityEngine;

// todo: rework required

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject headObject;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private TextMeshProUGUI crossHair;

        [SerializeField] private GameObject meleeModel;
        [SerializeField] private GameObject pistolModel;
        [SerializeField] private GameObject slugModel;
        [SerializeField] private GameObject smgModel;
        [SerializeField] private int weaponIndex;

        private List<GameObject> _weaponsModel = new List<GameObject>();

        private void Start()
        {
            weaponIndex = 0; // starting with melee weapon
            _weaponsModel.Add(meleeModel);
            _weaponsModel.Add(pistolModel);
            _weaponsModel.Add(slugModel);
            _weaponsModel.Add(smgModel);
            UpdateWeaponsModel();
        }

        private void Update()
        {
            if  (!GlobalVariables.GamePaused)
            {
                RaycastHit hit;
                var bro = Physics.Raycast(headObject.transform.position,
                    headObject.transform.TransformDirection(Vector3.forward),
                    out hit, Mathf.Infinity, layerMask);
                if (bro)
                {
                    crossHair.color = Color.red;
                    crossHair.fontStyle = FontStyles.Underline;
                    if (InputManager.RangedAttackPressed) 
                        hit.collider.gameObject.GetComponent<EnemyStats>().GotHit(1); // TODO: remove this once-
                                                                                            // proper weapons are added
                }
                else
                {
                    crossHair.color = Color.grey;
                    crossHair.fontStyle = FontStyles.Normal;
                }
            }
        }

        private void UpdateWeaponsModel()
        {
            foreach (var obj in _weaponsModel)
            {
                obj.SetActive(false);
            }
            _weaponsModel[weaponIndex].SetActive(true);
        }
    }
}