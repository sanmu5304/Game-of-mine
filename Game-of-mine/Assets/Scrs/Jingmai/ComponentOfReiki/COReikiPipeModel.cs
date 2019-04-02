using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JingMai
{
    public class COReikiPipeModel : COReikiBaseModel
    {
        /// <summary>
        /// 经脉方向
        /// </summary>
        public COReikiBuildDirection pipeDirection;
    }

    public enum  COReikiPipeDirection {
        TurnLeft,
        TurnRight,
        GoStraight
    }
}