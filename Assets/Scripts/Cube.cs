using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minTimeDisappearance = 2;
    [SerializeField] private int _maxTimeDisappearance = 5;

    public event Action<Cube> Disappeard;
    private bool _isTouchet = false;

    private void OnEnable()
    {
        _isTouchet = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouchet == false)
        {
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                Disappear();
            }
        }
    }

    public void Disappear()
    {
        int timeDisappearance = Random.Range(_minTimeDisappearance, _maxTimeDisappearance + 1);

        _isTouchet = true;
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        StartCoroutine(DelayedDisappear(timeDisappearance));
    }

    private IEnumerator DelayedDisappear(float delay)
    {
        yield return new WaitForSeconds(delay);
        Disappeard?.Invoke(this);
    }
}
