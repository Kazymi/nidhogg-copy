using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MainMenuConfiguration 
{
    [SerializeField] private Button toGameButton;
    [SerializeField] private Button exitGameButton;

    public Button ToGameButton => toGameButton;
    public Button ExitButton => exitGameButton;
}
