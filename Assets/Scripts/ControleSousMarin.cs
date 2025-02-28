using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ControleSousMarin : MonoBehaviour
{
    private float _vitesse;
    private Rigidbody _rb;

    private Vector3 directionInput;

    private Animator _animator;

    private float _rotationVelocity;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>(); 
        _vitesse = 2f;
    }

    void OnMove(InputValue directionBase)
    {
        Vector2 directionAvecVitesse = directionBase.Get<Vector2>() * _vitesse;
        directionInput = new Vector3(0f, 0f, directionAvecVitesse.y);
        _animator.SetFloat("Mouvement", directionInput.magnitude);
    }

    void FixedUpdate()
    {
        
        Vector3 mouvement = directionInput;
        float rotation = 0f;
        
        if (directionInput.magnitude > 0f)
        {
            // calculer rotation cible
            float rotationCible = Vector3.SignedAngle(-Vector3.forward, directionInput, Vector3.up);
            // faire le changement plus graduel avec une interpolation
            rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationCible, ref _rotationVelocity, 0.12f);
            // appliquer la rotation cible directement
            _rb.MoveRotation(Quaternion.Euler(0.0f, rotation, 0.0f));
        }
        
        _rb.AddForce(mouvement, ForceMode.VelocityChange);

        Vector3 vitesseSurPlane = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        _animator.SetFloat("Vitesse", vitesseSurPlane.magnitude);
        _animator.SetFloat("Deplacement", vitesseSurPlane.magnitude);
    }
}
