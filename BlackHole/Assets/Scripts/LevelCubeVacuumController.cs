using UnityEngine;
using DG.Tweening;

public class LevelCubeVacuumController : MonoBehaviour
{
    [Header("General Variables")]
    [Range(0, 10f)] [SerializeField] private float _shrinkSpeed; 
    public float risingSpeed; 
    public float risingRotationSpeed; 
    public float vacuumSpeed; 
    public LevelCubeStates currentState;

    [Header("References")]
    private Rigidbody _rbCube;

    public enum LevelCubeStates
    {
        OnHold,
        InVacuum,
        OnRise
    }

    private void Start()
    {
        _rbCube = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        switch (currentState)
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

    void HandleInVacuum()
    {
        transform.Rotate(new Vector3(0f, (risingRotationSpeed / 2f) * Time.deltaTime, (risingRotationSpeed / 2f ) * Time.deltaTime), Space.Self);

    }

    private void HandleRising()
    {
        transform.position = new Vector3(transform.position.x, -transform.position.y + (risingSpeed * Time.deltaTime), transform.position.z);
        transform.Rotate(new Vector3(0f, risingRotationSpeed * Time.deltaTime, risingRotationSpeed * Time.deltaTime), Space.Self);

    } // HandleRising()

    private void TriggerShrink()
    {
        transform.DOScale(0.03f, _shrinkSpeed).SetDelay(0.1f).OnComplete(() =>
        {
            Destroy(gameObject);
        });

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HoleInnerTrigger"))
        {
            if (currentState != LevelCubeStates.OnRise)
            {
                currentState = LevelCubeStates.OnRise;
                transform.parent = other.transform;
                _rbCube.isKinematic = true;
                TriggerShrink();
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HoleVacuumColl"))
        {
            if (currentState != LevelCubeStates.OnRise)
            {
                currentState = LevelCubeStates.InVacuum;

                Vector3 dir = (other.GetComponent<HoleVacuumCollController>().CenterTornadoTransform.position - transform.position).normalized;
                _rbCube.AddForce(-dir * vacuumSpeed * Time.deltaTime);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HoleVacuumColl"))
        {
            if (currentState != LevelCubeStates.OnRise)
            {
                currentState = LevelCubeStates.OnHold;
            }
        }

    } // OnTriggerExit()

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Killzone"))
        {
            Destroy(gameObject);
        }

    } // OnCollisionEnter()
    
} // class
