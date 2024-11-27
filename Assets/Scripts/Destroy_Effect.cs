using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Effect : MonoBehaviour
{
    public float Timer;
    void Update()
    {
        Destroy(gameObject, Timer);
    }
}
