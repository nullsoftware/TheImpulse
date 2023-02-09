using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

[RequireComponent(typeof(StarterAssetsInputs))]
[RequireComponent(typeof(ThirdPersonController))]
public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera;
    [SerializeField] private float _normalCameraSensativity = 1f;
    [SerializeField] private float _aimCameraSensativity = 0.5f;
    [SerializeField] private LayerMask _aimColliderMask = new LayerMask();

    private StarterAssetsInputs _input;
    private ThirdPersonController _controller;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _controller = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        Vector3 aimTarget = Vector3.zero;
        Vector3 center = new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(center);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, _aimColliderMask))
        {
            aimTarget = hit.point;
        }

        if (_input.Aim)
        {
            _aimVirtualCamera.gameObject.SetActive(true);
            _controller.SetSensativity(_aimCameraSensativity);
            _controller.SetRotateOnMove(false);

            Vector3 aimDirection = aimTarget;

            if (aimDirection == Vector3.zero)
            {
                aimDirection = ray.origin + ray.direction.normalized * 20f;
            }

            aimDirection.y = transform.position.y;
            aimDirection = (aimDirection - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, 20f * Time.deltaTime);

            if (_input.Shoot)
            {

            }
        }
        else
        {
            _aimVirtualCamera.gameObject.SetActive(false);
            _controller.SetRotateOnMove(true);
            _controller.SetSensativity(_normalCameraSensativity);
        }

    }
}
