using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {

        // manager
        private MapSpriteManager spriteManager;

        public int map_max_x = 10;
        public int map_max_y = 10;
        public int map_max_z = 1;

        public string key = "default";

        public GameObject mapRoot;
        public GameObject TilePrefab;

        private TileManager[] tiles;

        void Start()
        {
            spriteManager = GameObject.FindWithTag("MapSpriteManager").GetComponent<MapSpriteManager>();
            this.GenerateMap();
        }

        public void GenerateMap()
        {
            if (this.tiles != null)
            {
                foreach (var tile in this.tiles)
                {
                    Destroy(tile.gameObject);
                }
            }
            this.tiles = new TileManager[this.map_max_x * this.map_max_y * this.map_max_z];

            for (int map_x = this.map_max_x - 1; map_x >= 0; map_x--)
            {
                for (int map_y = this.map_max_y - 1; map_y >= 0; map_y--)
                {

                    for (int map_z = 0; map_z < this.map_max_z; map_z++)
                    {

                        TileManager tile = Instantiate(this.TilePrefab, mapRoot.transform).GetComponent<TileManager>();
                        tile.transform.name = string.Format("tile_{0}_{1}_{2}", map_x, map_y, map_z);

                        // index
                        tile.tileIsoController.index = this.GetTileIndex(map_x, map_y, map_z);

                        // sortingOrder
                        tile.sortingOrder = tile.tileIsoController.index;

                        // tiles
                        this.tiles[tile.tileIsoController.index] = tile;


                        // sprite
                        tile.sprite = spriteManager.GetMapSprite(this.key, new Vector4(1, 10, 0, 1));

                        // transform
                        tile.tileIsoController.x = map_x;
                        tile.tileIsoController.y = map_y;
                        tile.tileIsoController.z = map_z;

                        tile.transform.localPosition = new Vector3(tile.tileIsoController.unityX, tile.tileIsoController.unityY);
                    }
                }
            }
        }

        // index 按照 sortingOrder 生成， 无需再次生成 sortingOrder
        private int GetTileIndex(int x, int y, int z)
        {
            return (this.map_max_y - y - 1) * this.map_max_x * this.map_max_z + (this.map_max_x - x - 1) * this.map_max_z + z;
        }

    }

}
