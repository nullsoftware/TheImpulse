using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    private bool _isShooting;
    private int _currentAmmoAmount;

    public bool IsShooting => _isShooting;

    public virtual bool ReloadRequested => _currentAmmoAmount == 0;

    public void StartShooting()
    {
        if (_isShooting)
            return;

        _isShooting = true;
        StartCoroutine(OnShootingStarted());
    }

    public void EndShooting()
    {
        if (!_isShooting)
            return;

        _isShooting = false;
        StartCoroutine(OnShootingFinished());
    }

    protected abstract IEnumerator OnShootingStarted();
    protected abstract IEnumerator OnShootingFinished();
}
