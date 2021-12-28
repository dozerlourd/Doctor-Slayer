using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputButton
{
    public bool rightArrowBtn;
    public bool leftArrowBtn;

    public bool jumpBtn;

    public bool punchAttackBtn;
    public bool kickAttackBtn;

    
}

public class InputManager : SingletonObject<InputManager>
{
    public InputButton inputButton;
    public float InputVecX => inputButton.rightArrowBtn ? (inputButton.leftArrowBtn ? 0 : 1) : (inputButton.leftArrowBtn ? -1 : 0);

    public void SetInputValue(out bool variable, bool value) => variable = value;

    public bool GetJumpBtnValue() => inputButton.jumpBtn;
    public bool GetPunchAttackBtnValue() => inputButton.punchAttackBtn;
    public bool GetKickAttackBtnValue() => inputButton.kickAttackBtn;
}
