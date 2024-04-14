using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private  float _damage;
    private void OnTriggerEnter(Collider other) 
    {
        GameHandler.Instance.UpdatePlayerHeath(_damage);
    }
    private void OnTriggerStay(Collider other) 
    {
        GameHandler.Instance.UpdatePlayerHeath(_damage*0.1f);
    }
 
}
