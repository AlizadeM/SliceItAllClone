using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierCubeBehaviour : MonoBehaviour
{
    public int MultiplierValue;

    [SerializeField] private Text _canvasMultiplierText;

    private void Awake()
    {
        _canvasMultiplierText.text = MultiplierValue.ToString() + "X";
    }
}
