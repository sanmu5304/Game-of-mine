using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JingMai
{
    public class PartOfBodyNodeModel
    {
        /// <summary>
        /// 节点状态
        /// </summary>
        public PartOfBodyNodeState state;

        /// <summary>
        /// The x.
        /// </summary>
        public int x;

        /// <summary>
        /// The y.
        /// </summary>
        public int y;

    }

    /// <summary>
    /// 节点状态
    /// </summary>
    public enum PartOfBodyNodeState
    {
        /// <summary>
        /// 不可用
        /// </summary>
        Unuseful,
        /// <summary>
        /// 未解锁
        /// </summary>
        Lock,
        /// <summary>
        /// 已解锁
        /// </summary>
        Unlock
    }
}
