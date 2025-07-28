using System;
using System.Collections;
using UnityEngine;

public class QuaternionUtils
{
    private RotationSO _rotationSo;
    private float _rotationSpeed;
    
    public void SetRotation(RotationSO rotationSo)
    {
        _rotationSo = rotationSo;
        _rotationSpeed = _rotationSo.RotationSpeed;
    }

    public void ResetRotation(Transform transform)
    {
        transform.eulerAngles = Vector3.zero;
    }

    public void SetRotation(Transform transform,Vector3 rotation)
    {
        transform.eulerAngles = rotation;
    }
    
    public void LookAtPosition(Transform transform,Vector3 targetPosition)
    {
        transform.LookAt(targetPosition);
    }
    
    public IEnumerator RotateToPosition(Transform transform,Vector3 target,Action onComplete = null)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
        onComplete?.Invoke();
    }
}