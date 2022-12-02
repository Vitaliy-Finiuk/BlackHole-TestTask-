using UnityEngine;

namespace CodeBase.PickUp
{
    public abstract class PickUp : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
    
        protected virtual void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }
    
        protected abstract void TriggerEnter(Collider obj);

        protected abstract void TriggerExit(Collider obj);
    }
}
