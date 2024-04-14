using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace GunSystem
{
    public class GunHandler : MonoBehaviour
    {
        
        //Gun System
        public static GunHandler Instance { get; private set; }
        private bool IsSystemActive = false;
        private AbsGun[] _gunsList;
        private AbsGun _currentGun;
        private int _currentGunI = 0;
        void Awake()
        {
            Instance = this;
        }
        void Start()
        {
            _gunsList = GetComponentsInChildren<AbsGun>();
            //_currentGun = _gunsList[0];
            for (int i = 0; i < _gunsList.Length; i++)
            {
                _gunsList[i].SetActive(false);
            }

        }
        public void Shoot(Vector3 start, Vector3 dir)
        {
            //aca llamamos a los metodos del arma
            _currentGun?.Shoot();
            _currentGun.ChangeAmmoAmount(-1);
        }

        public void SwitchGun(float scroll)
        {
            _currentGunI = (int) Mathf.Abs(_currentGunI + scroll) % _gunsList.Length;
            if (!_currentGun.IsGunActive() || !_gunsList[_currentGunI].IsGunActive()) return;
            _currentGun.SetActive(false);
            _gunsList[_currentGunI].SetActive(true);
            _currentGun = _gunsList[_currentGunI];
        }
        public bool IsGunEmpty()
        {
            return _currentGun.GetAmmo() == 0 ? true : false;
        }
        public int GetCurrentAmmo()
        {
            return _currentGun.GetAmmo();
        }
        public void PickAmmo(int index, int ammoAmount)
        {
            if (!_gunsList[index].IsGunActive()) //si todavia no la teniamos la activamos
            {
                _gunsList[index].ActivateGun();
            }
            if (!IsSystemActive) //si es la primera que levanto activo el sistema y activo el arma
            {
                Debug.Log("Se activa el sistema");
                IsSystemActive = true;
                PlayerController.Instance.ActivateGunSystem();
                _currentGun = _gunsList[index];
                _currentGunI = index;
                _currentGun.SetActive(true);
            }
            _gunsList[index].ChangeAmmoAmount(ammoAmount);
        }
        public float GetForce()
        {
            return _currentGun.GetForce();
        }
    }

    public abstract class AbsGun : MonoBehaviour
    {
        [SerializeField] private float _force;
        private const string _animTrigger = "Shoot";
        private Animator _anim;
        private AudioSource _audio;
        private bool _isActive = false;
        private int _ammo;
        public virtual void Start()
        {
            this.SetComponents(GetComponent<Animator>(), GetComponent<AudioSource>());
        }
        public void SetComponents(Animator anim, AudioSource audio)
        {
            _anim = anim;
            _audio = audio; 
        }
        //Shoot
        public void Shoot()
        {
            this.Anim();
            this.Audio();
        }
        protected void Anim()
        {
            _anim.SetTrigger(_animTrigger);
        }
        protected void Audio()
        {
            _audio.Play();
        }
        public float GetForce()
        {
            return _force;
        }
        //Ammo System
        public int GetAmmo()
        {
            return _ammo;
        }

        public void ChangeAmmoAmount(int delta)
        {
            _ammo += delta;
        }

        //Switch System
        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }
        public bool IsGunActive(){ return _isActive; }
        public void ActivateGun(){ _isActive = true; }
    }

    public abstract class AbsAmmo : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _offSet;

        private Transform _tr;
        private int i = 0;
        void Awake()
        {
            _tr = GetComponentsInChildren<Transform>()[1];
        }
        void FixedUpdate()
        {
            MoveAndRotate();
        }

        private void MoveAndRotate()
        {
            _tr.Rotate(_rotationSpeed * Time.deltaTime * Vector3.up);
            _tr.localPosition = _offSet * Mathf.Sin(0.1f*i) * Time.deltaTime * Vector3.up;
            i++;
            i %= 361;
        }
        private void OnTriggerEnter(Collider other) 
        {
            Destroy(gameObject);
            this.AmmoPicked();    
        }

        public abstract void AmmoPicked();
    }
}