using UnityEngine;
using System.Collections;

namespace Map
{
    public class TileIsoController : MonoBehaviour
    {
        public int x = 0;
        public int y = 0;
        public int z = 0;

        public float unityX
        {
            get
            {
                return this.x * 0.5f - this.y * 0.5f;
            }
        }
        public float unityY
        {
            get
            {
                return this.x * 0.25f + this.y * 0.25f + this.z * 0.5f;
            }
        }

        public int index = 0;
    }
}