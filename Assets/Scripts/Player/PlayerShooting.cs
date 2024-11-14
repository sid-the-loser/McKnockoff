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
        [Header("General")]
        [SerializeField] private GameObject headObject;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask barricadeLayer;
        [SerializeField] private TextMeshProUGUI crossHair;
        [SerializeField] private int weaponIndex;
        
        [Header("Melee")]
        [SerializeField] private GameObject meleeModel;
        [SerializeField] private float meleeDistance = 10.0f;
        [SerializeField] private float meleeDamage = 1.0f;
        
        [Header("Pistol")]
        [SerializeField] private GameObject pistolModel;
        [SerializeField] private float pistolDistance = 100.0f;
        [SerializeField] private float pistolDamage = 5.0f;
        
        [Header("Slug")]
        [SerializeField] private GameObject slugModel;
        [SerializeField] private float slugDistance = 50.0f;
        [SerializeField] private float slugDamage = 10.0f;
        
        [Header("Smg")]
        [SerializeField] private GameObject smgModel;
        [SerializeField] private float smgDistance = 100.0f;
        [SerializeField] private float smgDamage = 5.0f;

        private Animator _meleeAnimator;
        private Animator _pistolAnimator;

        private List<GameObject> _weaponsModel = new List<GameObject>();
        private PlayerStats _playerStats;

        private void Start()
        {
            if (!Application.isEditor)
                weaponIndex = 0; // starting with melee weapon
            _weaponsModel.Add(meleeModel);
            _weaponsModel.Add(pistolModel);
            _weaponsModel.Add(slugModel);
            _weaponsModel.Add(smgModel);
            UpdateWeaponsModel();

            _meleeAnimator = meleeModel.GetComponent<Animator>();
            _pistolAnimator = pistolModel.GetComponent<Animator>();
            
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
                    /*crossHair.color = Color.red;
                    crossHair.fontStyle = FontStyles.Underline;
                    if (InputManager.RangedAttackPressed) 
                        data.collider.gameObject.GetComponent<EnemyStats>().GotHit(1); */

                    switch (weaponIndex)
                    {
                        // MELEE
                        case 0:
                        {
                            data = new RaycastHit();
                            hit = Physics.Raycast(headObject.transform.position, 
                                headObject.transform.TransformDirection(Vector3.forward), 
                                out data, meleeDistance, enemyLayer);

                            if (hit)
                            {
                                crossHair.color = Color.red;
                                crossHair.fontStyle = FontStyles.Underline;
                            
                                if (InputManager.RangedAttackPressed)
                                {
                                    data.collider.gameObject.GetComponent<EnemyStats>().GotHit(meleeDamage);
                                }
                            }

                            break;
                        }
                        // PISTOL
                        case 1:
                        {
                            data = new RaycastHit();
                            hit = Physics.Raycast(headObject.transform.position, 
                                headObject.transform.TransformDirection(Vector3.forward), 
                                out data, pistolDistance, enemyLayer);

                            if (hit)
                            {
                                crossHair.color = Color.red;
                                crossHair.fontStyle = FontStyles.Underline;
                                if (InputManager.RangedAttackPressed)
                                {
                                    data.collider.gameObject.GetComponent<EnemyStats>().GotHit(pistolDamage);
                                }
                            }

                            break;
                        }
                        // SLUG
                        case 2:
                        {
                            data = new RaycastHit();
                            hit = Physics.Raycast(headObject.transform.position, 
                                headObject.transform.TransformDirection(Vector3.forward), 
                                out data, slugDistance, enemyLayer);

                            if (hit)
                            {
                                crossHair.color = Color.red;
                                crossHair.fontStyle = FontStyles.Underline;
                                if (InputManager.RangedAttackPressed)
                                {
                                    data.collider.gameObject.GetComponent<EnemyStats>().GotHit(slugDamage);
                                }
                            }

                            break;
                        }
                        // SMG
                        case 3:
                        {
                            data = new RaycastHit();
                            hit = Physics.Raycast(headObject.transform.position, 
                                headObject.transform.TransformDirection(Vector3.forward), 
                                out data, smgDistance, enemyLayer);

                            if (hit)
                            {
                                crossHair.color = Color.red;
                                crossHair.fontStyle = FontStyles.Underline;
                                if (InputManager.RangedAttackPressedDown)
                                {
                                    // change it to make it so it shoots continuously with a delay
                                    data.collider.gameObject.GetComponent<EnemyStats>().GotHit(smgDamage);
                                }
                            }

                            break;
                        }
                    }
                }
                else
                {
                    data = new RaycastHit();
                    hit = Physics.Raycast(headObject.transform.position, 
                        headObject.transform.TransformDirection(Vector3.forward),
                        out data, meleeDistance, barricadeLayer);

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

                #region Animations & Wasting Ammo

                if (InputManager.RangedAttackPressed)
                {
                    switch (weaponIndex)
                    {
                        case 0:
                            _meleeAnimator.SetTrigger("attack");
                            break;
                        case 1:
                            _pistolAnimator.SetTrigger("attack");
                            break;
                    }
                }

                #endregion
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