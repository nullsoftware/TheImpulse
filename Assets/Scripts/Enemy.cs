using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1;

    private Rigidbody _rigidbody;
    private FieldOfView _fieldOfView;
    private NavMeshAgent _meshAgent;

    private float _currentRotatioAngle = 0;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _fieldOfView = GetComponent<FieldOfView>();
        _meshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _meshAgent.enabled = true;
    }

    private void Update()
    {
        if (_fieldOfView.CanSeePlayer && _meshAgent.enabled)
        {
            //float distance = Vector3.Distance(_fieldOfView.Target.transform.position, transform.position);

            var targetRotation = Quaternion.LookRotation(_fieldOfView.Target.transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            if (_meshAgent.destination != _fieldOfView.Target.transform.position)
                _meshAgent.SetDestination(_fieldOfView.Target.transform.position);
        }
        else
        {
            transform.Rotate(Vector3.up, 40f * Time.deltaTime);
            // idle patrol
        }
    }
}
