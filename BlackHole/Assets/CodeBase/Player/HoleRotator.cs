using UnityEngine;

namespace CodeBase.Player
{
    public class HoleRotator : MonoBehaviour
    {
        [SerializeField] [Header("General Variables")] 
        private float _rotateSpeed;

        [SerializeField] [Header("References")] 
        private Transform _centerTransformToRotate;

        private void Update() => 
            _centerTransformToRotate.Rotate(new Vector3(0f, _rotateSpeed * Time.deltaTime, 0f));
    }
}