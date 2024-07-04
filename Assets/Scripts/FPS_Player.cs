using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player : MonoBehaviour
{
    [Header("���� ��Ʈ��")]
    public float moveSpeed = 5f; //�̵� �ӵ�
    public float turnSpeed = 90f; //ȸ�� �ӵ�
    [SerializeField]
    float h = 0f, v = 0f, r = 0f; //Ű�� ���� �޾ƿ��� ��� ����

    [SerializeField] //private�� ���������� �ν���Ʈ �� ���̰� ���ִ� ��ɾ�
    private Transform tr;

    [Header("ȭ�� ��Ʈ��")]
    public float xSensitivity = 100f; //���콺 ����
    public float ySensitivity = 100f; //���콺 ����
    public float YminLimit = -45f; //���� ȸ�� x�� ȸ�� ���� ��.
    public float YmaxLimit = 45f; //���� ȸ�� y�� ȸ�� ���� ��. ���� 45������ �ٶ� �� ����
    public float XminLimit = -360f; //�¿� ȸ�� ���� ��.
    public float XmaxLimit = 360f; //�¿� ȸ�� ���� ��. 360�� �� �� ����

    [SerializeField]
    private float xRot = 0.0f;
    [SerializeField]
    private float yRot = 0.0f;

    [Header("���� ��Ʈ��")]
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
        xRot += Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime; //��� ���콺�� �����̴� ��ŭ ���� ����
        yRot += Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime; //��� ���콺�� �����̴� ��ŭ ���� ����

        yRot = Mathf.Clamp(yRot, YminLimit, YmaxLimit); //yRot�� �ִ� 45, �ּ� -45�� ����

        tr.eulerAngles = new Vector3(-yRot, xRot, 0f);//���͹������� ȸ���ϴ� �Լ�
    }

    private void PlayerMove()
    {
        h = Input.GetAxis("Horizontal"); //Ű���忡�� A�� D�� ������ ���̸� �� Ű���� h�� ����(GetAxis = ������ ���� üũ�ϴ� ��ɾ�)
                                         //A�� ������ -1 , ������ ������ 0 , D�� ������ 1�� �ѱ��.
        v = Input.GetAxis("Vertical"); //Ű���忡�� W�� S�� ������ ���̸� �� Ű���� v�� ����
                                       //S�� ������ -1, ������ ������ 0 , W�� ������ 1�� �ѱ��.

        Vector3 Normal = (h * Vector3.right) + (v * Vector3.forward);
        tr.Translate(Normal.normalized * Time.deltaTime * moveSpeed);
        //tr.Translate(Vector3.right * h * Time.deltaTime * moveSpeed);//��ǥ �̵� �Լ�
        //tr.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
        ////Ű�Է¿� ���� h�� v�� ����� ������ �ٲ�� �̵� ������ �ٲ��, ��ŸŸ���� �ε巯�� ���������� �����̱����� ���ߴ�.
    }
}
