using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JingMai
{

    public class ReikiBaseModel
    {
        /// <summary>
        /// 灵气属性
        /// </summary>
        public ReikiType type;

        /// <summary>
        /// 灵气等级
        /// </summary>
        public int level;

        /// <summary>
        /// 灵气数量
        /// </summary>
        public int count;

        public ReikiBaseModel(ReikiType type, int level, int count)
        {
            this.type = type;
            this.level = level;
            this.count = count;
        }
    }

    public enum ReikiType {
        Jin,
        Mu,
        Shui,
        Huo,
        Tu,
        Bing,
        Feng,
        Lei,
    }

}