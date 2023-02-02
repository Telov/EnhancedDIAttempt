using System;
using UnityEngine;

namespace EnhancedDIAttempt
{
    public class QuestionMarkMB : MonoBehaviour
    {
        private int _layer;
        private void Awake()
        {
            _layer = LayerMask.NameToLayer("Player");
            Debug.Log($"player layer number: {_layer}");
            gameObject.AddComponent<BoxCollider2D>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.gameObject.layer == _layer)
            {
                
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(transform.position, Vector3.one);
        }
    }
}