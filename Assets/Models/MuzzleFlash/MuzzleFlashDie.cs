using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashDie : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.05f);
    }
}
