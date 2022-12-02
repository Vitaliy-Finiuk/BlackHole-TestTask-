using System.Collections.Generic;
using CodeBase.Infrastructure;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerMove : MonoBehaviour
    {

        #region SerializedField

        [Header("Hole mesh")]
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshCollider _meshCollider;

        [Header("Hole vertices radius")]
        [SerializeField] Vector2 _moveLimits;
        [SerializeField] private float _radius;
        [SerializeField] private Transform _holeCenter;
        [SerializeField] private Transform _rotatingCenter;

        [Space]
        [SerializeField] private float _moveSpeed;

        #endregion

        #region PrivateFields

        private Mesh _mesh;
        private List<int> _holeVertices;
        private List<Vector3> _offSets;
        private int _holeVerticesCount;

        private float _horizontalAxis, _verticalAxis;
        private Vector3 _touch, _targetPosition;

        #endregion

        #region UnityFunctions

        private void Start()
        {
            RotationCircleAnim();

            Game.IsMoving = false;

            _holeVertices = new List<int>();
            _offSets = new List<Vector3>();

            _mesh = _meshFilter.mesh;

            FindHoleVectices();
        }

        private void Update()
        {
            Game.IsMoving = Input.GetMouseButton(0);

            if(Game.IsMoving)
            {
                MoveHole();

                UpdateVerticesHolePosition();
            }
        }

        #endregion

        #region Methods

        private void MoveHole()
        {
            _horizontalAxis = Input.GetAxis("Mouse X");
            _verticalAxis = Input.GetAxis("Mouse Y");

            _touch = Vector3.Lerp(_holeCenter.position, _holeCenter.position + new Vector3(_horizontalAxis, 0f, 
                _verticalAxis), _moveSpeed * Time.deltaTime);

            _targetPosition = new Vector3(
                Mathf.Clamp(_touch.x, -_moveLimits.x, _moveLimits.x), _touch.y,
                Mathf.Clamp(_touch.z, -_moveLimits.y, _moveLimits.y)
            );
            _holeCenter.position = _targetPosition;
        }

        private void UpdateVerticesHolePosition()
        {
            Vector3[] vertices = _mesh.vertices;
            for (int i = 0; i < _holeVerticesCount; i++)
            {
                vertices [_holeVertices [i]] = _holeCenter.position + _offSets[i];
            }

            _mesh.vertices = vertices;
            _meshFilter.mesh = _mesh;
            _meshCollider.sharedMesh = _mesh;
        }


        private void FindHoleVectices()
        {
            for (int i = 0; i < _mesh.vertices.Length; i++)
            {
                float distace = Vector3.Distance(_holeCenter.position, _mesh.vertices[i]);

                if(distace < _radius)
                {
                    _holeVertices.Add(i);
                    _offSets.Add(_mesh.vertices[i] - _holeCenter.position);
                }
            }

            _holeVerticesCount = _holeVertices.Count;
        }

        private void RotationCircleAnim()
        {
            _rotatingCenter
                .DORotate(new Vector3(90f, 0f, -90f), 0.2f)
                .SetEase(Ease.Linear)
                .From(new Vector3(90f, 0, 0f))
                .SetLoops(-1, LoopType.Incremental);
        }

        #endregion
        
        #region UnityEditor

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_holeCenter.position, _radius);
        }

        #endregion

    }
}
