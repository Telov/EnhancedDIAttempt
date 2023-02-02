using UnityEngine;
using UnityEngine.Tilemaps;

namespace EnhancedDIAttempt
{
    [CreateAssetMenu(menuName = "2D/Tiles/QuestionMarkTileSO")]
    public class QuestionMarkTileSO : Tile
    {
        [field: SerializeField] public TileBase TileAfterTrigger { get; set; }
        [field: SerializeField] public float JumpHeight { get; set; }
        [field: SerializeField] public float JumpDuration { get; set; }

        private Tilemap _tilemap;

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            if (go == null) return base.StartUp(position, tilemap, go);

            if (_tilemap == null) _tilemap = tilemap.GetComponent<Tilemap>();
            go.GetComponent<QuestionMarkTileMB>().Construct(position, _tilemap, this);

            return base.StartUp(position, tilemap, go);
        }
    }
}