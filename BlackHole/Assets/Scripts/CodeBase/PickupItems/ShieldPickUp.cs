﻿using UnityEngine;

namespace CodeBase.Enemy
{
    public class ShieldPickUp : PickUp
    {
        protected override void TriggerEnter(Collider obj) => 
            Debug.Log("ShieldTriggerEnter");

        protected override void TriggerExit(Collider obj) => 
            Debug.Log("ShieldTriggerExit");
    }
}