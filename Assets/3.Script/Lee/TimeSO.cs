using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "TimeSO", fileName = "newTimeSO")]
public class TimeSO : ScriptableObject
{
    [Range(0.0f, 2.0f)]
    [SerializeField] private float targetScale;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;
    [SerializeField] private float intencity;

    public float TargetScale => targetScale;
    public float Duration => duration;
    public Ease Ease => ease;
    public float Intencity => intencity;
}
