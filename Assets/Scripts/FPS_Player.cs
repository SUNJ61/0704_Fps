using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player : MonoBehaviour
{
    [Header("방향 컨트롤")]
    public float moveSpeed = 5f; //이동 속도
    public float turnSpeed = 90f; //회전 속도
    [SerializeField]
    float h = 0f, v = 0f, r = 0f; //키의 값을 받아오는 멤버 변수

    [SerializeField] //private라 선언했지만 인스펙트 상에 보이게 해주는 명령어
    private Transform tr;

    [Header("화면 컨트롤")]
    public float xSensitivity = 100f; //마우스 감도
    public float ySensitivity = 100f; //마우스 감도
    public float YminLimit = -45f; //상하 회전 x축 회전 제한 값.
    public float YmaxLimit = 45f; //상하 회전 y축 회전 제한 값. 상하 45도씩만 바라볼 수 있음
    public float XminLimit = -360f; //좌우 회전 제한 값.
    public float XmaxLimit = 360f; //좌우 회전 제한 값. 360도 돌 수 있음

    [SerializeField]
    private float xRot = 0.0f;
    [SerializeField]
    private float yRot = 0.0f;

    [Header("점프 컨트롤")]
    [SerializeField]
    private float jumpForce = 5.5f;
    private bool jumpCtrl = true;

    void Start()
    {
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        PlayerMove();
        PlayerRotation();
        PlayerRun();
        PlayerJump();
    }
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCtrl)
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
            jumpCtrl = false;
        }
        else if(GetComponent<Rigidbody>().velocity == Vector3.up * 0f)
        {
            jumpCtrl = true;
        }
    }
    private void PlayerRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            moveSpeed = 10f;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = 5f;
    }

    private void PlayerRotation()
    {
        xRot += Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime; //계속 마우스가 움직이는 만큼 값을 더함
        yRot += Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime; //계속 마우스가 움직이는 만큼 값을 더함

        yRot = Mathf.Clamp(yRot, YminLimit, YmaxLimit); //yRot을 최대 45, 최소 -45로 지정

        tr.eulerAngles = new Vector3(-yRot, xRot, 0f);//백터방향으로 회전하는 함수
    }

    private void PlayerMove()
    {
        h = Input.GetAxis("Horizontal"); //키보드에서 A나 D를 누르는 중이면 그 키값을 h에 대입(GetAxis = 누르는 중을 체크하는 명령어)
                                         //A를 누르면 -1 , 누르지 않으면 0 , D를 누르면 1을 넘긴다.
        v = Input.GetAxis("Vertical"); //키보드에서 W나 S를 누르는 중이면 그 키값을 v에 대입
                                       //S를 누르면 -1, 누르지 않으면 0 , W를 누르면 1을 넘긴다.

        Vector3 Normal = (h * Vector3.right) + (v * Vector3.forward);
        tr.Translate(Normal.normalized * Time.deltaTime * moveSpeed);
        //tr.Translate(Vector3.right * h * Time.deltaTime * moveSpeed);//좌표 이동 함수
        //tr.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
        ////키입력에 따라 h와 v가 양수와 음수로 바뀌어 이동 방향이 바뀐다, 델타타임은 부드러운 프레임으로 움직이기위해 곱했다.
    }
}
