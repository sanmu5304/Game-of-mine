using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JingMai
{
    public class COReikiBaseModel
    {
        /// <summary>
        /// The weight.
        /// </summary>
        public int weight;

        /// <summary>
        /// The height.
        /// </summary>
        public int height;

        /// <summary>
        /// 部位限制，只能在制定部位安装，null 则为无限制
        /// </summary>
        public PartOfBodyType partLimit;

        /// <summary>
        /// 建造方向
        /// </summary>
        public COReikiBuildDirection buildDirection;

    }

    /// <summary>
    /// 建造方向
    /// </summary>
    public enum COReikiBuildDirection {
        Up,
        Down,
        Left,
        Right
    }
}