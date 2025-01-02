 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

public class SlowMo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;

    [SerializeField] private List<TimeSO> sequence;

    public bool IsSequencing = false;

    private Queue<TimeSO> times = new Queue<TimeSO>();

    private void Awake()
    {
        RefillSequence();
    }

    private void RefillSequence()
    {
        for (int i = 0; i < sequence.Count; i++)
            times.Enqueue(sequence[i]);
    }

    public void StartSequence()
    {
        ChangeTime();
    }

    private void ChangeTime()
    {
        if (times.TryDequeue(out TimeSO timeSO))
        {
            IsSequencing = true;

            DOTween.To(() => Time.timeScale,
                x =>
                {
                    Time.timeScale = x;
                    Time.fixedDeltaTime = x * 0.2f;
                    tmp.text = Time.timeScale.ToString();
                },

                timeSO.TargetScale, timeSO.Duration).SetEase(timeSO.Ease).
                OnComplete(() =>
                {
                    IsSequencing = false;
                    ChangeTime();
                });
        }

        else
        {
            IsSequencing = false;
            RefillSequence();
        }
    }
}
