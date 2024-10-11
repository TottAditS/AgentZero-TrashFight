using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class Movement : MonoBehaviour
{
    [Header("Refs")]
    public CharacterController ctrl;
    public Transform groundCheck;
    public Transform camPos;
    public Animator anim;

    [Header("Player Values")]
    public float pSpeed;
    public float pJumpHeight;
    public Vector3 direction;
    private float rotSpeed;
    public float rotSmoothTime = 0.1f;
    public float gravity;

    [Header("Ground Check")]
    public bool isGround;
    public float checkerSize;
    public LayerMask groundMask;

    //[Header("Dodge Check")]
    //public bool isDodging;
    //public bool canDodge;
    //public float dodgeDistance;
    //public float dodgeCD;
    //public float dodgeSpeed;
    //public float dodgeAdjuster1;
    //public float dodgeAdjuster2;
    ////public Vector2 dodgeDir;

    [Header("Speed")]
    [SerializeField] private Vector3 velo;
    [SerializeField] private Vector3 speed;

    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        ctrl = GetComponent<CharacterController>();
        camPos = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
        //isDodging = false;
        //canDodge = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, checkerSize, groundMask);

        if (isGround && velo.y < 0f)
            velo.y = -5f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = new Vector3(x, 0f, z).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Player rotation & Cam angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camPos.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotSpeed, rotSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //Quaternion targetRot = Quaternion.LookRotation(direction, Vector3.up);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);

            // Move
            //anim.SetTrigger("Run");
            ctrl.Move(moveDir.normalized * pSpeed * Time.deltaTime);
            speed = moveDir * pSpeed;

        }
        else
        {
            //anim.SetTrigger("Idle");
        }

        // Jump
        //if (isGround && Input.GetButtonDown("Jump"))
        //    velo.y = Mathf.Sqrt(pJumpHeight * -2f * gravity);

        velo.y += gravity * Time.deltaTime;
        ctrl.Move(velo * Time.deltaTime);

        // Dodge
        //if (!isDodging && canDodge && isGround && Input.GetButtonDown("Dodge"))
        //{
        //    anim.SetTrigger("Dodge");

        //    canDodge = false;
        //    isDodging = true;
        //    Vector3 dodge = transform.forward;

        //    StartCoroutine(dashMove(dodge, dodgeDistance));
        //    Invoke(nameof(dodgeReset), dodgeCD);

        //}
    }

    //IEnumerator dashMove(Vector3 dodge, float dodgeDist)
    //{
    //    dodge = dodge * dodgeSpeed;
    //    float count = 0;

    //    while (count < dodgeDist)
    //    {
    //        ctrl.Move(dodge);
    //        count = count + dodgeAdjuster1;
    //        yield return new WaitForSeconds(dodgeAdjuster2);
    //    }

    //    isDodging = false;
    //}

    //void dodgeReset()
    //{
    //    ctrl.Move(move * pSpeed * Time.deltaTime);
    //    canDodge = true;
    //}
}