using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPulse : MonoBehaviour
{
    [SerializeField]
    private float _expandingSpeed = 1f;

    void Update()
    {
        CalculateGrowth();
    }

    void CalculateGrowth()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }
}
