using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierCubeBehaviour : MonoBehaviour
{
    [SerializeField] private Text _canvasMultiplierText;
    public int MultiplierValue;

    private void Awake()
    {
        _canvasMultiplierText.text = MultiplierValue.ToString() + "X";
    }
}
