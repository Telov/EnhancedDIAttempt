using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EnhancedDIAttempt
{
    public class QuestionMarkTileMB : MonoBehaviour
    {
        private Vector3Int _pos;
        private Tilemap _tilemap;
        private QuestionMarkTileSO _so;

        public void Construct(Vector3Int pos, Tilemap tilemap, QuestionMarkTileSO so)
        {
            _pos = pos;
            _tilemap = tilemap;
            _so = so;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Work());
            }
        }

        private IEnumerator Work()
        {
            float timePassed = 0f;
            
            while (timePassed < _so.JumpDuration)
            {
                yield return new WaitForEndOfFrame();
                timePassed += Time.deltaTime;
                
                float distance = Mathf.PingPong(timePassed * (_so.JumpHeight / _so.JumpDuration) * 2, _so.JumpHeight);
                _tilemap.SetTransformMatrix(_pos, Matrix4x4.Translate(distance * Vector3.up));
            }
            _tilemap.SetTransformMatrix(_pos, Matrix4x4.Translate(Vector3.zero));
            _tilemap.SetTile(_pos, _so.TileAfterTrigger);
        }
    }
}