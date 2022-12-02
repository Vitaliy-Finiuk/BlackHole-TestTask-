using CodeBase.Enemy;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] private TriggerObserver _triggerObserver;
    
    protected virtual void Start()
    {
        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;
    }

    protected virtual void Update() {}

    protected abstract void TriggerEnter(Collider obj);

    protected abstract void TriggerExit(Collider obj);
}
