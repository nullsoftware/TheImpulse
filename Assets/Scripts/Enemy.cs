using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _keepDistance = 20;

    private Transform _target;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag(Constants.PlayerTag).transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if (distance < _keepDistance)
        {
            Vector3 direction = _target.position - transform.position;
            direction.Normalize();

            _rigidbody.velocity = direction * 2;
        }
    }
}
