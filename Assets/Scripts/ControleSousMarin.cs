using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ControleSousMarin : MonoBehaviour
{
    private float _vitesseAct;

    private float _vitesseBase;
    private float _vitesseFast;
    private Rigidbody _rb;

    private Vector3 directionInput;

    private Animator _animator;

    private float _rotationVelocity;
    private float _animationMult;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>(); 
        _vitesseBase = 0.3f;
        _vitesseFast = 0.6f;


    }
    void OnShift(){
        _vitesseAct = _vitesseFast;
        _animationMult = 3f;
        _animator.SetFloat("AnimMult", _animationMult);

    }
    void OnMove(InputValue directionBase)
    {
        _animationMult = 1f;
        _animator.SetFloat("AnimMult", _animationMult);
        
        _vitesseAct = _vitesseBase;
        Vector3 directionAvecVitesse = directionBase.Get<Vector3>() * _vitesseAct;
        directionInput = new Vector3(0f, directionAvecVitesse.y, directionAvecVitesse.z);
    }

    void FixedUpdate()
    {
        
        Vector3 mouvement = directionInput;
        
        _rb.AddForce(mouvement, ForceMode.VelocityChange);

        Vector3 vitesseSurPlane = new Vector3(0f, _rb.velocity.z, 0f);

        _animator.SetFloat("Mouvement", vitesseSurPlane.magnitude);
    }
}
