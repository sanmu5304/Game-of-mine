using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JingMai
{
    public class PartOfBodyNodeController : MonoBehaviour
    {
        public static float NodeSize = 1.1f;

        public PartOfBodyNodeModel model;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        void Start()
        {
        }

        public void InitNodeUI()
        {
            // 位置
            this.transform.localPosition = new Vector3(this.model.x * PartOfBodyNodeController.NodeSize, this.model.y * PartOfBodyNodeController.NodeSize);

            // 贴图
            switch (this.model.state)
            {
                case PartOfBodyNodeState.Unuseful:

                    this.spriteRenderer.color = Color.clear;
                    break;
                case PartOfBodyNodeState.Lock:

                    this.spriteRenderer.color = Color.yellow;
                    break;
                case PartOfBodyNodeState.Unlock:

                    this.spriteRenderer.color = Color.white;
                    break;
                default:
                    break;
            }
        }

        public void DebugSprite()
        {
            this.spriteRenderer.color = Color.red;
        }
    }
}