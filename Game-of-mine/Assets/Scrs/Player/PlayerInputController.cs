using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{


    private void FixedUpdate()
    {
        this.Move();
    }

    public float moveSpeed = 1;
    void Move(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            Vector3 direction = new Vector3(h, v, 0).normalized;
            this.transform.localPosition += direction * this.moveSpeed * Time.fixedDeltaTime;
        }
    }
}
