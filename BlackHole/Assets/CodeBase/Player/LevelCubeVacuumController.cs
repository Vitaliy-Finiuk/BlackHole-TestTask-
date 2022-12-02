using DG.Tweening;
using UnityEngine;

namespace CodeBase.Player
{
    public enum LevelCubeStates
    {
        OnHold,
        InVacuum,
        OnRise
    }
    
    [RequireComponent(typeof(Rigidbody))]
    public class LevelCubeVacuumController : MonoBehaviour
    {
        #region SerializedField

        [Header("General Variables")]
        [Range(0, 10f)] [SerializeField] private float _shrinkSpeed; 
        [Range(0.05f, 100f)] [SerializeField] private float _risingSpeed; 
        [Range(0, 1000f)] [SerializeField] private float _risingRotationSpeed; 
        [Range(0, 1000f)] [SerializeField] private float _vacuumSpeed; 
        
        #endregion 

        #region Fields

        public LevelCubeStates CurrentState;

        [Header("References")]
        private Rigidbody _rigidbody;

        #endregion
        
        #region UnityFunctions

        private void Start() => 
            _rigidbody = GetComponent<Rigidbody>();

        private void Update() => 
            ApplyState();

        #endregion
        
        #region Methods

        private void ApplyState()
        {
            switch (CurrentState)
            {
                case LevelCubeStates.OnHold:
                    break;
                case LevelCubeStates.InVacuum:
                    HandleInVacuum();
                    break;
                case LevelCubeStates.OnRise:
                    HandleRising();
                    break;
            }
        }


        #endregion

        #region ObjectStates

        void HandleInVacuum() =>
            transform.Rotate(new Vector3(0f, (_risingRotationSpeed / 2f) * Time.deltaTime, 
                (_risingRotationSpeed / 2f ) * Time.deltaTime), Space.Self);

        private void HandleRising()
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y + (_risingSpeed * Time.deltaTime), transform.position.z);
            transform.Rotate(new Vector3(0f, _risingRotationSpeed * Time.deltaTime, _risingRotationSpeed * Time.deltaTime), Space.Self);
        } 

        private void TriggerShrink()
        {
            transform.DOScale(0.03f, _shrinkSpeed).SetDelay(0.1f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        #endregion

        #region TriggerAndCollisionFunctions

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("HoleInnerTrigger"))
            {
                if (CurrentState != LevelCubeStates.OnRise)
                {
                    CurrentState = LevelCubeStates.OnRise;
                    transform.parent = other.transform;
                    _rigidbody.isKinematic = true;
                    TriggerShrink();
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("HoleVacuumColl"))
            {
                if (CurrentState != LevelCubeStates.OnRise)
                {
                    CurrentState = LevelCubeStates.InVacuum;

                    Vector3 dir = (other.GetComponent<HoleVacuumCollController>().CenterHoleTransform.position - transform.position).normalized;
                    _rigidbody.AddForce(-dir * _vacuumSpeed * Time.deltaTime);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("HoleVacuumColl"))
            {
                if (CurrentState != LevelCubeStates.OnRise)
                {
                    CurrentState = LevelCubeStates.OnHold;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Killzone"))
            {
                Destroy(gameObject);
            }
        }

        #endregion

    }
}