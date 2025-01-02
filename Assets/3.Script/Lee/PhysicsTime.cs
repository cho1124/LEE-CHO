using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PhysicsTime : MonoBehaviour
{
    [SerializeField] UnityEvent slowEvent;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            slowEvent?.Invoke();
    }
}
