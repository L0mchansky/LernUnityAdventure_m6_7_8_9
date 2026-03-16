using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    [SerializeField] private Animator _animator;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
            _animator.SetBool("IsRunning", true);

        if (Input.GetKeyUp(KeyCode.S))
            _animator.SetBool("IsRunning", false);

        if (Input.GetKeyUp(KeyCode.Space))
            _animator.SetTrigger("Die");
    }
}
