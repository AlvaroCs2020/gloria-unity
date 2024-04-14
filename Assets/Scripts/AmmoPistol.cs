using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GunSystem;
public class AmmoPistol : AbsAmmo
{
    [SerializeField] private int _index;
    [SerializeField] private int _ammoAmount;

    public override void AmmoPicked()
    {
        GameHandler.Instance.PickAmmo(_index, _ammoAmount);
    }
}
