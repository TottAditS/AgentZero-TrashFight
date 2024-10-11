using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
    public int combo;
    private float cooldown;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        combo = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Combo",combo);

        cooldown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && combo == 0 && cooldown < 0.05)
        {
            cooldown = 0.7f;
            combo++;
        }
        if (Input.GetMouseButtonDown(0) && combo == 1 && cooldown < 0.05)
        {
            cooldown = 0.7f;
            combo++;
        }
        if (Input.GetMouseButtonDown(0) && combo == 2 && cooldown < 0.05)
        {
            cooldown = 0.7f;
            combo++;
        }
        if (Input.GetMouseButtonDown(0) && combo == 3 && cooldown < 0.05)
        {
            cooldown = 0.7f;
            combo++;
        }
        if (cooldown < 0.01f)
        {
            combo = 0;
        }
    }
}
