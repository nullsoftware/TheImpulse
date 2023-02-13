using CartoonFX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BulletTarget))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] private ParticleSystem _lowHealthEffect;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _shieldEffect;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private ParticleSystem _fireEffect;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;
    private FieldOfView _fieldOfView;
    private NavMeshAgent _meshAgent;
    private BulletTarget _healthInfo;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _fieldOfView = GetComponent<FieldOfView>();
        _meshAgent = GetComponent<NavMeshAgent>();
        _healthInfo = GetComponent<BulletTarget>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_healthInfo.HealthPoints > 0)
        {
            _meshAgent.enabled = true;
            _rigidbody.isKinematic = true;
        }
    }

    private void Update()
    {
        if (_meshAgent.enabled)
        {
            if (_fieldOfView.CanSeePlayer)
            {
                float distance = Vector3.Distance(_fieldOfView.Target.transform.position, transform.position);

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

    public void OnDamaged()
    {
        if (_healthInfo.HealthPoints < 15 && !_lowHealthEffect.isPlaying)
        {
            _hitEffect.gameObject.SetActive(true);
            _lowHealthEffect.gameObject.SetActive(true);

            StartCoroutine(SelfDestruct());
        }
    }

    public void OnDeath()
    {
        _meshAgent.enabled = false;
        _rigidbody.isKinematic = false;
        _rigidbody.freezeRotation = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
        _shieldEffect.gameObject.SetActive(false);
        _lowHealthEffect.gameObject.SetActive(false);
        _explosionEffect.gameObject.SetActive(true);
        _hitEffect.gameObject.SetActive(false);
        _meshRenderer.enabled = false;

        Destroy(_shieldEffect.gameObject);
        Destroy(_lowHealthEffect.gameObject);

        DestroyAfterDelay(2);
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3);
        _shieldEffect.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        _healthInfo.DealDamage(_healthInfo.HealthPoints);
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
