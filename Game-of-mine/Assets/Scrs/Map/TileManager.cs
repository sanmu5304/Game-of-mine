using UnityEngine;
using System.Collections;

namespace Map
{
    public class TileManager : MonoBehaviour
    {
        private TileIsoController _tileIsoController;
        public TileIsoController tileIsoController
        {
            get
            {
                if (!_tileIsoController)
                {
                    _tileIsoController = this.GetComponent<TileIsoController>();
                }
                return _tileIsoController;
            }
        }

        private SpriteRenderer _spriteRenderer;
        private SpriteRenderer spriteRenderer
        {
            get
            {
                if (!_spriteRenderer)
                {
                    _spriteRenderer = this.GetComponent<SpriteRenderer>();
                }
                return _spriteRenderer;
            }
        }

        public Sprite sprite
        {
            get
            {
                return this.spriteRenderer.sprite;
            }
            set
            {
                this.spriteRenderer.sprite = value;
            }
        }

        public int sortingOrder
        {
            set
            {
                this.spriteRenderer.sortingOrder = value;
            }
        }

        private void Start()
        {

        }

    }
}
