using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JingMai
{
    /// <summary>
    /// 身体部位
    /// </summary>
    public class PartOfBodyModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public PartOfBodyType type;



        /// <summary>
        /// 部位品级
        /// </summary>
        public PartOfBodyGrade grade;

        /// <summary>
        /// 部位等级
        /// </summary>
        public int level;



        /// <summary>
        /// 最大精血值
        /// </summary>
        public int maxHP;
        /// <summary>
        /// 当前精血值
        /// </summary>
        public int curHP;

        /// <summary>
        /// 部位健康状态
        /// </summary>
        public PartOfBodyState state
        {
            get
            {
                if (this.maxHP == 0)
                {
                    return PartOfBodyState.NoInit;
                }
                else if (this.curHP == this.maxHP)
                {
                    return PartOfBodyState.Perfect;
                }
                else if (this.curHP >= this.maxHP * 0.3f)
                {
                    return PartOfBodyState.Nomal;
                }
                else if (this.curHP > 0)
                {
                    return PartOfBodyState.Damaged;
                }
                else
                {
                    return PartOfBodyState.Necrosis;
                }
            }
        }



        /// <summary>
        /// 部位区域宽
        /// </summary>
        public int weight;

        /// <summary>
        /// 部位区域高
        /// </summary>
        public int height;

        /// <summary>
        /// 节点 列表
        /// </summary>
        private List<PartOfBodyNodeModel> nodeModels;

        /// <summary>
        /// 通过index获取节点数据
        /// </summary>
        /// <returns>The node model.</returns>
        public PartOfBodyNodeModel GetPartOfBodyNodeModel(int x, int y)
        {
            return this.nodeModels[this.GetPartOfBodyNodeIndex(x, y)];
        }

        /// <summary>
        /// 初始化节点数据模型
        /// </summary>
        public void InitPartOfBodyNodeModels()
        {
            this.nodeModels = new List<PartOfBodyNodeModel>();
            float unusefulP = this.GetNodeStateUnusefulProbability();
            float lockP = this.GetNodeStateLockProbability();

            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.weight; x++)
                {
                    PartOfBodyNodeModel model = new PartOfBodyNodeModel();


                    float unusefulV = Random.value;
                    float lockV = Random.value;

                    if (unusefulV < unusefulP)
                    {
                        model.state = PartOfBodyNodeState.Unuseful;
                    }
                    else if (lockV < lockP)
                    {
                        model.state = PartOfBodyNodeState.Lock;
                    }
                    else
                    {
                        model.state = PartOfBodyNodeState.Unlock;
                    }

                    model.x = x;
                    model.y = y;

                    this.nodeModels.Add(model);
                }
            }
        }

        /// <summary>
        /// Gets the index of the part of body node.
        /// </summary>
        /// <returns>The part of body node model index.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public int GetPartOfBodyNodeIndex(int x, int y)
        {
            return y * this.weight + x;
        }

        /// <summary>
        /// Gets the node state unuseful probability.
        /// </summary>
        /// <returns>The node state unuseful probability.</returns>
        private float GetNodeStateUnusefulProbability()
        {
            if (this.grade == PartOfBodyGrade.Tian)
            {
                return 0;
            }
            else if (this.grade == PartOfBodyGrade.Di)
            {
                return 0.15f;
            }
            else if (this.grade == PartOfBodyGrade.Xuan)
            {
                return 0.30f;
            }
            else if (this.grade == PartOfBodyGrade.Huang)
            {
                return 0.45f;
            }
            else
            {
                return 1f;
            }
        }

        /// <summary>
        /// Gets the node state lock probability.
        /// </summary>
        /// <returns>The node state lock probability.</returns>
        private float GetNodeStateLockProbability()
        {
            return 0.9f - this.level * 0.1f;
        }


        /// <summary>
        /// 连接点坐标,范围必须为【0，0.9】
        /// </summary>
        public Vector2 joinPoint;

        /// <summary>
        /// 如果是身体部位才有，与其他各个部位连接坐标
        /// </summary>
        private Dictionary<PartOfBodyType, Vector2> bodyJoinPointDic;

        /// <summary>
        /// 如果是身体部位才有，获取与其他各个部位连接坐标
        /// </summary>
        /// <returns>The body join point.</returns>
        /// <param name="bodyType">Body type.</param>
        public Vector2 GetBodyJoinPoint(PartOfBodyType bodyType)
        {
            if (this.bodyJoinPointDic == null)
            {
                Debug.LogError("not init bodyJoinPointDic!");
            }
            return this.bodyJoinPointDic[bodyType];
        }

        /// <summary>
        /// 如果是身体部位才有，设置与其他各个部位连接坐标
        /// </summary>
        /// <param name="bodyType">Body type.</param>
        /// <param name="point">Point.</param>
        public void SetBodyJoinPoint(PartOfBodyType bodyType, Vector2 point)
        {
            if (this.bodyJoinPointDic == null)
            {
                this.bodyJoinPointDic = new Dictionary<PartOfBodyType, Vector2>();
            }

            this.bodyJoinPointDic[bodyType] = point;
        }
    }

    /// <summary>
    /// 身体部位
    /// </summary>
    public enum PartOfBodyType
    {
        Body,
        LeftLeg,
        RightLeg,
        LetfArm,
        RightArm,
        Head
    }

    /// <summary>
    /// 部位健康状态
    /// </summary>
    public enum PartOfBodyState
    {
        /// <summary>
        /// 精血充沛
        /// </summary>
        Perfect,
        /// <summary>
        /// 正常
        /// </summary>
        Nomal,
        /// <summary>
        /// 受损
        /// </summary>
        Damaged,
        /// <summary>
        /// 坏死
        /// </summary>
        Necrosis,
        /// <summary>
        /// 未初始化
        /// </summary>
        NoInit
    }

    /// <summary>
    /// 品级：天、地、玄、黄
    /// </summary>
    public enum PartOfBodyGrade
    {
        Tian,
        Di,
        Xuan,
        Huang
    }
}