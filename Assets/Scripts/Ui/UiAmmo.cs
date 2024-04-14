using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UiSystem;
public class UiAmmo : UiText
{
    protected override void Start()
    {
        UiHandler.Instance.AssignAmmoText(this);
    }
}
