using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JingMai
{

    public class PartOfBodyManager : MonoBehaviour
    {
        public GameObject PartOfBodyPrefab;

        public GameObject PartOfBodyRoot;

        public PartOfBodyGrade grade;
        public int level = 0;

        void Start()
        {
            this.GenerateBodyPart();

        }


        public void GenerateBodyPart()
        {
            Debug.Log("GenerateBodyPart");

            this.GenerateAllPartOfBody();

        }

        public List<PartOfBodyController> partOfBodyControllers;

        private void GenerateAllPartOfBody()
        {

            this.ClearPartOfBody();

            this.partOfBodyControllers = new List<PartOfBodyController>();

            this.GenerateSomePartOfBody(PartOfBodyType.Body, 11, 15, Vector2.zero);

            this.GenerateSomePartOfBody(PartOfBodyType.LetfArm, 11, 2, new Vector2(1f, 0));
            this.GenerateSomePartOfBody(PartOfBodyType.RightArm, 11, 2, new Vector2(0, 0));
            this.GenerateSomePartOfBody(PartOfBodyType.LeftLeg, 3, 15, new Vector2(0, 1f));
            this.GenerateSomePartOfBody(PartOfBodyType.RightLeg, 3, 15, new Vector2(1f, 1f));
            this.GenerateSomePartOfBody(PartOfBodyType.Head, 3, 4, new Vector2(0.5f, 0));

        }

        private void GenerateSomePartOfBody(PartOfBodyType type, int w, int h, Vector2 joinPoint)
        {

            // 生成modle
            PartOfBodyModel bodyModel = new PartOfBodyModel();

            bodyModel.type = type;
            bodyModel.grade = this.grade;
            bodyModel.level = this.level;

            bodyModel.weight = w;
            bodyModel.height = h;

            if (type == PartOfBodyType.Body)
            {
                bodyModel.SetBodyJoinPoint(PartOfBodyType.LetfArm, new Vector2(0, 0.8f));
                bodyModel.SetBodyJoinPoint(PartOfBodyType.RightArm, new Vector2(1f, 0.8f));
                bodyModel.SetBodyJoinPoint(PartOfBodyType.LeftLeg, new Vector2(0.2f, 0f));
                bodyModel.SetBodyJoinPoint(PartOfBodyType.RightLeg, new Vector2(0.8f, 0f));
                bodyModel.SetBodyJoinPoint(PartOfBodyType.Head, new Vector2(0.5f, 1f));
            }
            else
            {
                bodyModel.joinPoint = joinPoint;
            }

            bodyModel.maxHP = 100;
            bodyModel.curHP = 100;

            bodyModel.InitPartOfBodyNodeModels();

            PartOfBodyController bodyPartOfBodyController = Instantiate(this.PartOfBodyPrefab, this.PartOfBodyRoot.transform).GetComponent<PartOfBodyController>();

            this.partOfBodyControllers.Add(bodyPartOfBodyController);

            bodyPartOfBodyController.InitPartOfBodyUI(bodyModel, this.partOfBodyControllers[0]);
        }

        private void ClearPartOfBody()
        {
            if (this.partOfBodyControllers != null)
            {
                foreach (var pobc in this.partOfBodyControllers)
                {
                    Destroy(pobc.gameObject);
                }
            }
        }
    }
}