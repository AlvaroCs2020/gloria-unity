using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UiSystem;

public class UiHealth : UiHealthBar 
{
    protected override void Start()
    {
        UiHandler.Instance.AssignHealthBar(this);
    }
}
