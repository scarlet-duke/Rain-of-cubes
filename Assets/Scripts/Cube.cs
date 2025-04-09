using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minTimeDisappearance = 2;
    [SerializeField] private int _maxTimeDisappearance = 5;

    private bool _touch = false;

    public void Disappear()
    {
        if(_touch == false)
        {
            int timeDisappearance = Random.Range(_minTimeDisappearance, _maxTimeDisappearance + 1);

            _touch = true;
            GetComponent<Renderer>().material.color = Random.ColorHSV();
            Destroy(gameObject, timeDisappearance);
        }

    }
}
