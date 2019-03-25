using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Map {
    public class MapSpriteManager : MonoBehaviour
    {
        public Sprite defaulMapSprite;

        [Serializable]
        public struct SpriteConfig
        {
            public string name;
            public DirecitonSprite[] direcitonSprites;
            public Sprite defaultSprite;
        }

        [Serializable]
        public struct DirecitonSprite
        {
            public Sprite sprite;
            public Vector4 left_right_up_down;
        }

        public SpriteConfig[] spriteConfigs;

        private Dictionary<string, Dictionary<int, Sprite>> spriteDic;

        private void Awake()
        {
            this.spriteDic = new Dictionary<string, Dictionary<int, Sprite>>();

            foreach (var sc in this.spriteConfigs)
            {
                var tempDirecitonDic = new Dictionary<int, Sprite>();

                foreach (var ds in sc.direcitonSprites)
                {
                    if (ds.sprite != null)
                    {
                        tempDirecitonDic[this.Vector4ToIndex(ds.left_right_up_down)] = ds.sprite;
                    }
                    else
                    {
                        Debug.LogErrorFormat("{0} _ {1} sprite not config!", sc.name, ds.left_right_up_down);
                    }
                }

                if (sc.defaultSprite != null)
                {
                    tempDirecitonDic[this.Vector4ToIndex(Vector4.one * -1)] = sc.defaultSprite;
                }
                else
                {
                    Debug.LogErrorFormat("{0} _ defaul sprite not config!", sc.name);
                }

                if (sc.name != null)
                {
                    this.spriteDic[sc.name] = tempDirecitonDic;
                }
                else
                {
                    Debug.LogError("sprite name not config!");
                }
            }
        }
    
        
        public Sprite GetMapSprite(string key, Vector4 left_right_up_down) {

            return this.spriteDic[key].ContainsKey(this.Vector4ToIndex(left_right_up_down)) ? this.spriteDic[key][this.Vector4ToIndex(left_right_up_down)] : this.spriteDic[key][this.Vector4ToIndex(Vector4.one * -1)];
        }

        private int Vector4ToIndex(Vector4 v4) {
            return (int)(v4.x + v4.y * 2 + v4.z * 4 + v4.w * 8);
        }
    }
}
