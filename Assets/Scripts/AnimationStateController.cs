using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    float velZ = 0f;
    float velX = 0f;
    public float accelerate = 2f;
    public float decelerate = 2f;
    float speedcap = 0.5f;
    Rigidbody rb;
    public float force = 10f;
    public GameObject orangorang;
    public bool isCombat;
    public float ComboCurr;

    private void Start()
    {
        ComboCurr = 0;
        animator = orangorang.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isCombat = false;
    }
    private void Update()
    {
        Getinput();
        changeVFX();
        //Debug.Log("X=" + velX);
        //Debug.Log("Z=" + velZ);
    }

    private void ResetCombo()
    {
        ComboCurr = 0;
    }

    private void Getinput()
    {
        velX = Mathf.Clamp(velX, -2f, 2f);
        velZ = Mathf.Clamp(velZ, -2f, 2f);

        bool Forward = Input.GetKey(KeyCode.W);
        bool Left = Input.GetKey(KeyCode.A);
        bool Right = Input.GetKey(KeyCode.D);
        bool Back = Input.GetKey(KeyCode.S);
        bool Run = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.G))
        {
            isCombat = !isCombat;
            Debug.Log(isCombat);
        }

        if (isCombat)
        {
            if (ComboCurr > 3)
            {
                ResetCombo();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ComboCurr++;
                Debug.Log(ComboCurr);
            }
        }

        if (Forward && velZ < speedcap && !Run)
        {
            velZ += Time.deltaTime * accelerate;
        }
        if (Left && velZ > -speedcap && !Run)
        {
            velX -= Time.deltaTime * accelerate;
        }
        if (Right && velZ < speedcap && !Run)
        {
            velX += Time.deltaTime * accelerate;
        }
        if (Back && velZ > -speedcap && !Run)
        {
            velZ -= Time.deltaTime * accelerate;
        }
        if (Run)
        {
            // Cek arah hadap karakter menggunakan Dot Product
            float direction = Vector3.Dot(transform.forward, Vector3.forward);

            if (direction > 0) // Karakter menghadap ke depan
            {
                velZ += Time.deltaTime * accelerate;
                Debug.Log("MAJUUU");
            }
            else if (direction < 0) // Karakter menghadap ke belakang
            {
                velZ -= Time.deltaTime * accelerate;
                Debug.Log("MUNDURR");
            }
        }
        if (!Forward && velZ > 0f)
        {
            velZ -= Time.deltaTime * decelerate;
        }
        if (!Forward && !Back && velZ != 0f && (velX > 0.05f && velX < 0.05f))
        {
            velZ -= Time.deltaTime;
        }
        if (!Left && velX < 0f)
        {
            velX += Time.deltaTime * decelerate;
        }
        if (!Right && velX > 0f)
        {
            velX -= Time.deltaTime * decelerate;
        }
        if (!Left && !Right && velX != 0f && (velX > 0.05f && velX < 0.05f))
        {
            velX -= Time.deltaTime;
        }

        SetAnimation();

        //if (Forward)
        //{
        //    rb.AddForce(transform.forward * force, ForceMode.Acceleration);
        //}
        //if (Left)
        //{
        //    rb.AddForce(-transform.right * force, ForceMode.Acceleration);
        //}
        //if (Right)
        //{
        //    rb.AddForce(transform.right * force, ForceMode.Acceleration);
        //}
        //if (Run)
        //{
        //    rb.AddForce(transform.forward * force * 2, ForceMode.Acceleration);
        //}
    }

    private void SetAnimation()
    {
        animator.SetFloat("Velocity Z", velZ);
        animator.SetFloat("Velocity X", velX);
        animator.SetFloat("Combo", ComboCurr);
        animator.SetBool("IsKombat", isCombat);
    }


    //=====================================================================
    //VFX

    public GameObject idleVFX;
    public GameObject Combo1VFX;
    public GameObject Combo2VFX;
    public GameObject Combo3VFX;

    public void changeVFX()
    {
        if (isCombat)
        {
            if (ComboCurr == 0)
            {
                idleVFX.SetActive(true);
                Combo1VFX.SetActive(false);
                Combo2VFX.SetActive(false);
                Combo3VFX.SetActive(false);
            }
            if (ComboCurr == 1)
            {
                Combo1VFX.SetActive(true);
            }
            if (ComboCurr == 2)
            {
                Combo2VFX.SetActive(true);
            }
            if (ComboCurr == 3)
            {
                Combo3VFX.SetActive(true);
            }
        }
    }
}
