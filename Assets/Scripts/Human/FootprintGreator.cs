using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class FootprintGreator : MonoBehaviour
{
    [SerializeField] private Transform _humanTransform;
    [SerializeField] private GameObject _stepPrefab;
    [SerializeField] private LayerMask _groundMask;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2, _groundMask))
            {
                Vector3 pos = hit.point;
                pos.y += 0.05f;
                Quaternion rot = Quaternion.Euler(Vector3.Cross(hit.point, _humanTransform.position));
                rot.y = _humanTransform.rotation.y;
                rot.w = _humanTransform.rotation.w;
                GameObject step = Instantiate(_stepPrefab, pos, rot);
            }
        }
    }
}