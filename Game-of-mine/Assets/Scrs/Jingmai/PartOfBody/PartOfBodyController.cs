using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JingMai
{

    public class PartOfBodyController : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject PartOfBodyNodePrefab;

        public List<PartOfBodyNodeController> nodeControllers;

        public PartOfBodyModel model;

        void Start()
        {

        }


        public void InitPartOfBodyUI(PartOfBodyModel model, PartOfBodyController bodyController = null)
        {
            this.model = model;

            this.nodeControllers = new List<PartOfBodyNodeController>();

            // 初始化关节点
            for (int y = 0; y < model.height; y++)
            {
                for (int x = 0; x < model.weight; x++)
                {
                    PartOfBodyNodeController nodeController = Instantiate(this.PartOfBodyNodePrefab, this.transform).GetComponent<PartOfBodyNodeController>();

                    nodeController.model = model.GetPartOfBodyNodeModel(x, y);

                    nodeController.InitNodeUI();

                    this.nodeControllers.Add(nodeController);
                }
            }

            // 关节点连接
            if (model.type != PartOfBodyType.Body)
            {
                if (bodyController != null)
                {
                    this.transform.localPosition += this.CalculatePartOfBodyPosition(bodyController);
                }
                else
                {
                    Debug.LogError("InitPartOfBodyUI Error : bodyModel is null!");
                }
            }
        }



        private Vector3 CalculatePartOfBodyPosition(PartOfBodyController bodyController)
        {
            // 找到连接节点
            Vector2 bodyJoinPoint = bodyController.model.GetBodyJoinPoint(this.model.type);

            PartOfBodyNodeController bodyJoinNodeController = bodyController.GetNodeControllerWithJoin(bodyJoinPoint);
            PartOfBodyNodeController partJoinNodeController = this.GetNodeControllerWithJoin(this.model.joinPoint);

            // 判断连接方向
            PartOfBodyJoinDirection joinDirection = this.CalculateJoinDirection(this.model.joinPoint, bodyJoinPoint);

            // 得到部位节点世界坐标
            Vector3 joinNodeTargetLocalPosition = bodyJoinNodeController.transform.localPosition;

            switch (joinDirection)
            {
                case PartOfBodyJoinDirection.Left_Right:
                    joinNodeTargetLocalPosition += Vector3.right * PartOfBodyNodeController.NodeSize;
                    break;
                case PartOfBodyJoinDirection.Right_Left:
                    joinNodeTargetLocalPosition += Vector3.left * PartOfBodyNodeController.NodeSize;
                    break;
                case PartOfBodyJoinDirection.Up_Down:
                    joinNodeTargetLocalPosition += Vector3.down * PartOfBodyNodeController.NodeSize;
                    break;
                case PartOfBodyJoinDirection.Down_Up:
                    joinNodeTargetLocalPosition += Vector3.up * PartOfBodyNodeController.NodeSize;
                    break;
                default:
                    break;
            }

            Vector3 joinNodeTargetWorldPosition = bodyController.transform.TransformPoint(joinNodeTargetLocalPosition);

            joinNodeTargetLocalPosition = this.transform.InverseTransformPoint(joinNodeTargetWorldPosition);
            // 得到部位坐标
            Vector3 offset = joinNodeTargetLocalPosition - partJoinNodeController.transform.localPosition;

            return offset;
        }

        /// <summary>
        /// 获取连接节点
        /// </summary>
        /// <returns>The node controller with join.</returns>
        /// <param name="joinPoint">Join.</param>
        private PartOfBodyNodeController GetNodeControllerWithJoin(Vector2 joinPoint)
        {
            int joinX = (int)(joinPoint.x * (this.model.weight - 1));

            int joinY = (int)(joinPoint.y * (this.model.height - 1));

            //Debug.Log("GetNodeControllerWithJoin:" + joinX + "," + joinY);

            return this.nodeControllers[this.model.GetPartOfBodyNodeIndex(joinX, joinY)];
        }

        /// <summary>
        /// Calculates the join direction.
        /// </summary>
        /// <returns>The join direction.</returns>
        /// <param name="joinPoint">Join point.</param>
        /// <param name="bodyJoinPoint">Body join point.</param>
        private PartOfBodyJoinDirection CalculateJoinDirection(Vector2 joinPoint, Vector2 bodyJoinPoint)
        {
            if (joinPoint.x <= 0.0001 && bodyJoinPoint.x >= 0.9999f)
            {
                return PartOfBodyJoinDirection.Left_Right;
            }
            else if (joinPoint.x >= 0.9999f && bodyJoinPoint.x <= 0.0001)
            {
                return PartOfBodyJoinDirection.Right_Left;
            }
            else if (joinPoint.y >= 0.9999f && bodyJoinPoint.y <= 0.0001)
            {
                return PartOfBodyJoinDirection.Up_Down;
            }
            else if (joinPoint.y <= 0.0001 && bodyJoinPoint.y >= 0.9999f)
            {
                return PartOfBodyJoinDirection.Down_Up;
            }
            else
            {
                return PartOfBodyJoinDirection.Left_Right;
            }
        }

        /// <summary>
        /// Part of body join direction.
        /// </summary>
        public enum PartOfBodyJoinDirection
        {
            /// <summary>
            /// 身体在左，部位在右
            /// </summary>
            Left_Right,
            /// <summary>
            /// 身体在右，部位在左
            /// </summary>
            Right_Left,
            /// <summary>
            /// 身体在上，部位在下
            /// </summary>
            Up_Down,
            /// <summary>
            /// 身体在下，部位在上
            /// </summary>
            Down_Up
        }

    }
}
