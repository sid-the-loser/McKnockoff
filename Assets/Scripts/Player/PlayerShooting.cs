using System;
using System.Collections;
using System.Collections.Generic;
using Barricades;
using Enemy;
using General;
using TMPro;
using Trashcans;
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
        [SerializeField] private LayerMask trashcanLayer;
        [SerializeField] private TextMeshProUGUI crossHair;
        [SerializeField] private int weaponIndex;
        
        [Header("Melee")]
        [SerializeField] private GameObject meleeModel;
        [SerializeField] private float meleeDistance = 10.0f;
        [SerializeField] private float meleeDamage = 1.0f;
        [SerializeField] private float meleeFireDelay = 0.5f;
        
        [Header("Pistol")]
        [SerializeField] private GameObject pistolModel;
        [SerializeField] private float pistolDistance = 100.0f;
        [SerializeField] private float pistolDamage = 5.0f;
        [SerializeField] private float pistolFireDelay = 0.5f;
        [SerializeField] private int maxPistolAmmo = 10;
        
        [Header("Slug")]
        [SerializeField] private GameObject slugModel;
        [SerializeField] private float slugDistance = 50.0f;
        [SerializeField] private float slugDamage = 10.0f;
        [SerializeField] private float slugFireDelay = 0.6f;
        [SerializeField] private int maxSlugAmmo = 2;
        
        [Header("Smg")]
        [SerializeField] private GameObject smgModel;
        [SerializeField] private float smgDistance = 100.0f;
        [SerializeField] private float smgDamage = 2.0f;
        [SerializeField] private float smgFireDelay = 0.1f;
        [SerializeField] private int maxSmgAmmo = 100;

        [Header("Misc")] [SerializeField] private TextMeshProUGUI ammoDisplay;

        private Animator _meleeAnimator;
        private Animator _pistolAnimator;
        private Animator _slugAnimator;
        private Animator _smgAnimator;
        
        private bool _smgInCooldown;
        private bool _slugInCooldown;
        private bool _meleeInCooldown;
        private bool _pistolInCooldown;

        private List<GameObject> _weaponsModel = new List<GameObject>();
        private int _inHandAmmo;
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
            _slugAnimator = slugModel.GetComponent<Animator>();
            _smgAnimator = smgModel.GetComponent<Animator>();
            
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
                            
                                if (InputManager.RangedAttackPressed && !_meleeInCooldown)
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
                                if (InputManager.RangedAttackPressed && !_pistolInCooldown && _inHandAmmo > 0)
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
                                if (InputManager.RangedAttackPressed && !_slugInCooldown && _inHandAmmo > 0)
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
                                if (InputManager.RangedAttackPressedDown && !_smgInCooldown && _inHandAmmo > 0)
                                {
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
                        data = new RaycastHit();
                        hit = Physics.Raycast(headObject.transform.position, 
                            headObject.transform.TransformDirection(Vector3.forward),
                            out data, meleeDistance, trashcanLayer);

                        if (hit)
                        {
                            crossHair.color = Color.yellow;
                            crossHair.fontStyle = FontStyles.Bold;
                            _playerStats.interactable = true;
                            if (InputManager.UseButtonPressed)
                            {
                                var tra = data.collider.gameObject.GetComponent<TrashcanLogic>();
                                var a = tra.TryBuy(_playerStats.money);

                                if (a > -1)
                                {
                                    _playerStats.money -= tra.moneyToOpen;
                                    weaponIndex = a;
                                    tra.RemoveMyself();
                                    UpdateWeaponsModel();
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

                #region Animations & Wasting Ammo

                if (InputManager.RangedAttackPressed && weaponIndex != 3)
                {
                    switch (weaponIndex)
                    {
                        case 0:
                            if (!_meleeInCooldown )
                            {
                                _meleeAnimator.SetTrigger("attack");
                                StartCoroutine(MeleeCooldown());
                            }
                            break;
                        case 1:
                            if (!_pistolInCooldown  && _inHandAmmo > 0)
                            {
                                _pistolAnimator.SetTrigger("attack");
                                _inHandAmmo--;
                                StartCoroutine(PistolCooldown());
                            }
                            break;
                        case 2:
                            if (!_slugInCooldown  && _inHandAmmo > 0)
                            {
                                _slugAnimator.SetTrigger("attack");
                                _inHandAmmo--;
                                StartCoroutine(SlugCooldown());
                            }
                            break;
                    }
                }
                else if (InputManager.RangedAttackPressedDown && weaponIndex == 3 && !_smgInCooldown && _inHandAmmo > 0)
                {
                    print("huh?");
                    _smgAnimator.SetTrigger("attack");
                    _inHandAmmo--;
                    StartCoroutine(SmgCooldown());
                }
                
                if (_inHandAmmo <= 0)
                {
                    weaponIndex = 0;
                    UpdateWeaponsModel();
                }

                #endregion

                #region Ammo Display Stuff

                if (weaponIndex == 0)
                {
                    ammoDisplay.text = "";
                }
                else
                {
                    ammoDisplay.text = $"Ammo:{_inHandAmmo}";
                }

                #endregion
            }
        }

        private IEnumerator MeleeCooldown()
        {
            _meleeInCooldown = true;
            yield return new WaitForSeconds(meleeFireDelay);
            _meleeInCooldown = false;
        }
        
        private IEnumerator PistolCooldown()
        {
            _pistolInCooldown = true;
            yield return new WaitForSeconds(pistolFireDelay);
            _pistolInCooldown = false;
        }
        
        private IEnumerator SlugCooldown()
        {
            _slugInCooldown = true;
            yield return new WaitForSeconds(slugFireDelay);
            _slugInCooldown = false;
        }
        
        private IEnumerator SmgCooldown()
        {
            _smgInCooldown = true;
            yield return new WaitForSeconds(smgFireDelay);
            _smgInCooldown = false;
        }

        private void UpdateWeaponsModel()
        {
            if (weaponIndex == 1)
            {
                _inHandAmmo = maxPistolAmmo;
            }
            if (weaponIndex == 2)
            {
                _inHandAmmo = maxSlugAmmo;
            }
            if (weaponIndex == 3)
            {
                _inHandAmmo = maxSmgAmmo;
            }
            
            foreach (var obj in _weaponsModel)
            {
                obj.SetActive(false);
            }
            _weaponsModel[weaponIndex].SetActive(true);
        }
    }
}