using UnityEngine;

namespace CodeBase.Enemy.PickUpTest
{
    public class PickUpTest : MonoBehaviour, PickUpObject 
    {
        protected bool active = true;

        protected void OnTriggerEnter(Collider collision)
        {
            if (active)
            {
                active = false;
                DoAction();
            }
        }

        protected void OnTriggerStay(Collider collision)
        {
            if (active)
            {
                active = false;
                DoAction();
            }
        }

        protected void OnTriggerExit(Collider collision)
        {
            if (active)
            {
                active = false;
                DoAction();
            }
        }

        protected virtual void DoAction()
        {

        }
    }
}