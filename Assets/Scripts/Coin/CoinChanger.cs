using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinChanger : MonoBehaviour
{
    [SerializeField] private LayerMask _coinLayerMask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 50, _coinLayerMask))
            {
                Color newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                if (hit.collider.GetComponent<Renderer>())
                    hit.collider.GetComponent<Renderer>().material.color = newColor;
            }
        }
    }
}
