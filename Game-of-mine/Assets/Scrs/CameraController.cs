using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // 当前缩放尺寸
    private float curSize = 5f;

    // 缩放限制系数
    private readonly float maxSize = 6f;
    private readonly float minSize = 3f;

    private readonly float gesturesScaleSpeed = 0.002f;
    private readonly float mouseScaleSpeed = 0.1f;

    // 记录上一次手机触摸位置判断用户是在做放大还是缩小手势
    private Vector2 lastSizePosition1;
    private Vector2 lastSizePosition2;

    // 记录上一次手机触摸位置判断用户移动屏幕方向
    private Vector3 moveStartScreenPos;
    private Vector3 targetMoveScreenPos;

    private Vector3 oldCameraPos;
    private float curRate;

    private bool needMove = false;
    private bool canMove = false;

    private readonly float moveLerpSpeed = 0.4f;

    // 镜头位置限制区域
    private readonly float moveMaxX = 5;
    private readonly float moveMinX = -5;
    private readonly float moveMaxY = 5;
    private readonly float moveMinY = -5;


    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            this.needMove = true;

            this.curRate = this.GetRate();

            this.moveStartScreenPos = Input.mousePosition;

            this.oldCameraPos = Camera.main.transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            this.targetMoveScreenPos = Input.mousePosition;
        }

        this.curSize -= Input.mouseScrollDelta.y * this.mouseScaleSpeed;
#endif

        // 判断触摸数量为单点触摸
        if (Input.touchCount == 1 && this.canMove)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                this.needMove = true;

                this.curRate = this.GetRate();

                this.moveStartScreenPos = Input.GetTouch(0).position;

                this.oldCameraPos = Camera.main.transform.position;
            }

            this.targetMoveScreenPos = Input.GetTouch(0).position;
        }

        // 判断触摸数量为多点触摸
        if (Input.touchCount > 1)
        {
            this.canMove = false;
            // 前两只手指触摸类型都为移动触摸
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {

                if (this.lastSizePosition1 == Vector2.zero && this.lastSizePosition2 == Vector2.zero)
                {
                    this.lastSizePosition1 = Input.GetTouch(0).position;
                    this.lastSizePosition2 = Input.GetTouch(1).position;
                }
                else
                {
                    this.curSize = this.CalculateCameraSize(this.lastSizePosition1, this.lastSizePosition2, Input.GetTouch(0).position, Input.GetTouch(1).position);
                    this.lastSizePosition1 = Input.GetTouch(0).position;
                    this.lastSizePosition2 = Input.GetTouch(1).position;
                }
            }
        }
        else
        {
            this.lastSizePosition1 = Vector2.zero;
            this.lastSizePosition2 = Vector2.zero;
        }

        if (Input.touchCount == 0)
        {
            this.canMove = true;
        }

    }

    private float CalculateCameraSize(Vector2 lastPos1, Vector2 lastPos2, Vector2 curPos1, Vector2 curPos2)
    {
        float lastDis = Vector2.Distance(lastPos1, lastPos2);
        float curDis = Vector2.Distance(curPos1, curPos2);

        float offsetSize = (curDis - lastDis) * this.gesturesScaleSpeed;

        float targetSize = this.curSize - offsetSize;

        if (targetSize > this.maxSize)
        {
            return this.maxSize;
        }

        if (targetSize < this.minSize)
        {
            return this.minSize;
        }

        return targetSize;
    }

    private float GetRate()
    {
        Vector3 zeroWorldPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 oneWorldPos = Camera.main.ScreenToWorldPoint(Vector3.up);

        return Vector3.Distance(zeroWorldPos, oneWorldPos);
    }

    private Vector3 GetMoveLimitPositon(Vector3 position)
    {
        float targetX = position.x;
        float targetY = position.y;
        if (targetX > this.moveMaxX)
        {
            targetX = this.moveMaxX;
        }
        else if (targetX < this.moveMinX)
        {
            targetX = this.moveMinX;
        }

        if (targetY > this.moveMaxY)
        {
            targetY = this.moveMaxY;
        }
        else if (targetY < this.moveMinY)
        {
            targetY = this.moveMinY;
        }
        return new Vector3(targetX, targetY, position.z);
    }

    private void LateUpdate()
    {
        if (this.needMove)
        {
            // 获取镜头目标位置
            Vector3 screenOffset = this.targetMoveScreenPos - this.moveStartScreenPos;
            Vector3 targetCameraPos = this.oldCameraPos - screenOffset * this.curRate;

            this.transform.position = Vector3.Lerp(this.transform.position, this.GetMoveLimitPositon(targetCameraPos), this.moveLerpSpeed);
        }


        Camera.main.orthographicSize = this.curSize;
    }
}
