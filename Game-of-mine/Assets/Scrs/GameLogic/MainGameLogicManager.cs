using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{

    public class MainGameLogicManager : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}
