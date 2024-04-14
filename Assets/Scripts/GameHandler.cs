using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using GunSystem;
using UiSystem;
public class GameHandler : MonoBehaviour
{
   
    public static GameHandler Instance { get; private set; }

    private void Awake() 
    {
       Instance = this; 
    }
    void Start()
    {   
        if (PlayerController.Instance != null)
            PlayerController.Instance.ShootEvent += OnShoot; //me suscribo al evento shoot con el metodo OnShoot
        
    }
    public void SwitchGun(float scroll)
    {
        GunHandler.Instance.SwitchGun(scroll);

        UiHandler.Instance.UpdateAmmoText(GunHandler.Instance.GetCurrentAmmo());
    }

    private void OnShoot(Vector3 start, Vector3 dir)//tmb deberia pasar el recoil y otras cosas
    {
        //Check Ammo
        if(GunHandler.Instance.IsGunEmpty())
            return;
        // Bit shift the index of the layer (8) to get a bit mask and negation of it 
        
        // esto capaz deberia estar re implementado con cada arma y que el gun handler se lo pida a current gun
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        
        RaycastHit hit;

        if (Physics.Raycast(start, dir, out hit, Mathf.Infinity, layerMask))
        {
            hit.rigidbody?.AddForce(-hit.normal*GunHandler.Instance.GetForce());
        }
        //Gun visual stuff
        GunHandler.Instance.Shoot(start, dir);
        //Ui should always be last one
        UiHandler.Instance.UpdateAmmoText(GunHandler.Instance.GetCurrentAmmo());
    }

    public void PickAmmo(int gunIndex, int ammoAmount)
    {
        GunHandler.Instance.PickAmmo(gunIndex, ammoAmount);
        UiHandler.Instance.UpdateAmmoText(GunHandler.Instance.GetCurrentAmmo());
    }

    public void UpdatePlayerHeath(float deltaHealth)
    {
        float currentHealth = PlayerController.Instance.UpdateHealth(deltaHealth);
        UiHandler.Instance.UpdateHealthBar(currentHealth);
    }

}
