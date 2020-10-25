using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum InputXBOX
{
    A,
    B,
    X,
    Y,
    LB,
    RB,
    Back,
    Start,
    L_Indent,
    R_Indent
}

public class InputController : SingletonMonoBehaviour<InputController>
{
    // なにが入力されたか受け取る（アナログスティックと +キーは別）
    private int Input_XBOX_Number = 0;

    // アナログスティック
    private Vector2 R_Stick;
    private Vector2 L_Stick;

    //
    float L_R_Trigger;

    public int GetXBOXInput() { return Input_XBOX_Number; }

    override protected void Awake()
    {
        base.Awake();
    }
    void Update()
    {
        // A
        if (Input.GetKeyDown("joystick button 0"))
        {
            Input_XBOX_Number = (int)InputXBOX.A;
            Debug.Log("A" + Input_XBOX_Number);
        }

        // B
        if (Input.GetKeyDown("joystick button 1"))
        {
            Input_XBOX_Number = (int)InputXBOX.B;
            Debug.Log("B" + Input_XBOX_Number);
        }

        // X
        if (Input.GetKeyDown("joystick button 2"))
        {
            Input_XBOX_Number = (int)InputXBOX.X;
            Debug.Log("X" + Input_XBOX_Number);
        }

        // Y
        if (Input.GetKeyDown("joystick button 3"))
        {
            Input_XBOX_Number = (int)InputXBOX.Y;
            Debug.Log("Y" + Input_XBOX_Number);
        }

        // LB
        if (Input.GetKeyDown("joystick button 4"))
        {
            Input_XBOX_Number = (int)InputXBOX.LB;
            Debug.Log("LB" + Input_XBOX_Number);
        }

        // RB
        if (Input.GetKeyDown("joystick button 5"))
        {
            Input_XBOX_Number = (int)InputXBOX.RB;
            Debug.Log("RB" + Input_XBOX_Number);
        }

        // Back
        if (Input.GetKeyDown("joystick button 6"))
        {
            Input_XBOX_Number = (int)InputXBOX.Back;
            Debug.Log("Back" + Input_XBOX_Number);
        }

        // Start
        if (Input.GetKeyDown("joystick button 7"))
        {
            Input_XBOX_Number = (int)InputXBOX.Start;
            Debug.Log("Start" + Input_XBOX_Number);
        }

        // Lスティック 押し込み
        if (Input.GetKeyDown("joystick button 8"))
        {
            Input_XBOX_Number = (int)InputXBOX.L_Indent;
            Debug.Log("L_Indent" + Input_XBOX_Number);
        }

        // Rスティック 押し込み
        if (Input.GetKeyDown("joystick button 9"))
        {
            Input_XBOX_Number = (int)InputXBOX.R_Indent;
            Debug.Log("R_Indent" + Input_XBOX_Number);
        }

        // 右ステック
        float hori = Input.GetAxis("Horizontal");
        // 左スティック
        float vert = Input.GetAxis("Vertical");
        if ((hori != 0) || (vert != 0))
        {
            Debug.Log("stick:" + hori + "," + vert);
        }



        //L Stick
        L_Stick.x = Input.GetAxis("L_Stick_H"); // X 軸
        L_Stick.y = Input.GetAxis("L_Stick_V"); // Y 軸
        if ((L_Stick.x != 0) || (L_Stick.y != 0))
        {
            Debug.Log("L stick: X" + L_Stick.x + ", Y" + L_Stick.y);
        }

        //R Stick
        R_Stick.x = Input.GetAxis("R_Stick_H"); // X 軸
        R_Stick.y = Input.GetAxis("R_Stick_V"); // Y 軸
        if ((R_Stick.x != 0) || (R_Stick.y != 0))
        {
            Debug.Log("R stick: X" + R_Stick.x + ", Y" + R_Stick.y);
        }

        //D-Pad
        float dph = Input.GetAxis("D_Pad_H"); // X 軸
        float dpv = Input.GetAxis("D_Pad_V"); // Y 軸
        if ((dph != 0) || (dpv != 0))
        {
            Debug.Log("D Pad:" + dph + "," + dpv);
        }

        //Trigger
        L_R_Trigger = Input.GetAxis("L_R_Trigger");

        // LT
        if (L_R_Trigger > 0)
        {
            Debug.Log("L trigger:" + L_R_Trigger);
        }
        // RT
        else if (L_R_Trigger < 0)
        {
            Debug.Log("R trigger:" + L_R_Trigger);
        }
        else
        {
            Debug.Log("  trigger:none");
        }
    }
}
