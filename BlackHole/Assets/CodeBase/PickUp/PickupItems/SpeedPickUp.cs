using UnityEngine;

namespace CodeBase.PickUp.PickupItems
{
    public class SpeedPickUp : PickUp
    {
        protected override void TriggerEnter(Collider obj) => 
            Debug.Log("SpeedTriggerEnter");

        protected override void TriggerExit(Collider obj) => 
            Debug.Log("SpeedTriggerExit");
    }
}