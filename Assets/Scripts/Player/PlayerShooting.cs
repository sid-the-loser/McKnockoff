using System;
using System.Collections.Generic;
using Barricades;
using Enemy;
using General;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

// todo: rework required

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GameObject headObject;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask barricadeLayer;
        [SerializeField] private TextMeshProUGUI crossHair;

        [SerializeField] private GameObject meleeModel;
        [SerializeField] private GameObject pistolModel;
        [SerializeField] private GameObject slugModel;
        [SerializeField] private GameObject smgModel;
        [SerializeField] private int weaponIndex;

        private List<GameObject> _weaponsModel = new List<GameObject>();
        private PlayerStats _playerStats;

        private void Start()
        {
            weaponIndex = 0; // starting with melee weapon
            _weaponsModel.Add(meleeModel);
            _weaponsModel.Add(pistolModel);
            _weaponsModel.Add(slugModel);
            _weaponsModel.Add(smgModel);
            UpdateWeaponsModel();

            _playerStats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            if  (!GlobalVariables.GamePaused)
            {
                RaycastHit data;
                var hit = Physics.Raycast(headObject.transform.position,
                    headObject.transform.TransformDirection(Vector3.forward),
                    out data, Mathf.Infinity, enemyLayer);
                if (hit)
                {
                    crossHair.color = Color.red;
                    crossHair.fontStyle = FontStyles.Underline;
                    if (InputManager.RangedAttackPressed) 
                        data.collider.gameObject.GetComponent<EnemyStats>().GotHit(1); // TODO: remove this once-
                                                                                            // proper weapons are added
                }
                else
                {
                    data = new RaycastHit();
                    hit = Physics.Raycast(headObject.transform.position, 
                        headObject.transform.TransformDirection(Vector3.forward),
                        out data, 10, barricadeLayer);

                    if (hit)
                    {
                        crossHair.color = Color.yellow;
                        crossHair.fontStyle = FontStyles.Bold;
                        _playerStats.interactable = true;
                        if (InputManager.UseButtonPressed)
                        {
                            var bar = data.collider.gameObject.GetComponent<Barricade>();
                            var a = bar.TryBuy(_playerStats.money);
                            if (a)
                            {
                                _playerStats.money -= bar.unlockMoney;
                                bar.RemoveMyself();
                            }
                        }
                    }
                    else
                    {
                        crossHair.color = Color.grey;
                        crossHair.fontStyle = FontStyles.Normal;
                        _playerStats.interactable = false;
                    }
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