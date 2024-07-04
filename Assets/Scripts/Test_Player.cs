using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Test_Player : MonoBehaviour
{
    public Transform tr;

    public float move_S = 5.0f;
    public float turn_S = 90.0f;

    public float h=0, v=0;

    public float Xsen = 100;
    public float Ysen = 100;
    public float yMax = 30;
    public float yMin = -30;
    public float xMax = 360;
    public float xMin = -360;

    private float xRot = 0.0f;
    private float yRot = 0.0f;

    public float jumpForce = 5.5f;
    public bool jump_T = true;

    void Start()
    {
        tr = GetComponent<Transform>();
    }


    void Update()
    {
        Move();
        Run();
        Rocation();
        if (Input.GetKeyDown(KeyCode.Space) && jump_T)
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
            jump_T = false;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            jump_T = true;
        }
    }

    private void Rocation()
    {
        xRot += Input.GetAxis("Mouse X") * Xsen * Time.deltaTime;
        yRot += Input.GetAxis("Mouse Y") * Ysen * Time.deltaTime;

        yRot = Mathf.Clamp(yRot, yMin, yMax);

        tr.eulerAngles = new Vector3(-yRot, xRot, 0);
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            move_S = 10.0f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.W))
        {
            move_S = 5.0f;
        }
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 Normal = (h * Vector3.right.normalized) + (v * Vector3.forward.normalized);
        tr.Translate(Normal * Time.deltaTime * move_S);
    }
}
