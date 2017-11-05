//
// マウスの入力解析
//
using System;
using System.Text;
using UnityEngine;

public class AnalyseMouse : MonoBehaviour
{
    // low
    public bool On { get; private set; }
    public Vector3 Position { get; private set; }

    // middle
    public bool Down { get; private set; }
    public bool Up { get; private set; }
    public Vector3 FromPosition { get; private set; }
    public Vector3 ToPosition { get; private set; }
    public Vector3 Velocity { get; private set; }
    public Vector3 LastPosition { get; private set; }

    // high
    public bool Press { get; private set; }
    public bool Dragging { get; private set; }
    public bool DragStart { get; private set; }
    public bool DragEnd { get; private set; }

    // setting
    public int DragStartBorder = 10;

    // private
    int count;

    void OnGUI()
    {
        var sb = new StringBuilder();
        sb.AppendFormat("On           {0}\n", On);
        sb.AppendFormat("Position     {0}\n", Position);
        sb.AppendFormat("Down         {0}\n", Down);
        sb.AppendFormat("Up           {0}\n", Up);
        sb.AppendFormat("FromPosition {0}\n", FromPosition);
        sb.AppendFormat("ToPosition   {0}\n", ToPosition);
        sb.AppendFormat("Velocity     {0}\n", Velocity);
        sb.AppendFormat("Press        {0}\n", Press);
        sb.AppendFormat("Dragging     {0}\n", Dragging);
        sb.AppendFormat("DragStart    {0}\n", DragStart);
        sb.AppendFormat("DragEnd      {0}\n", DragEnd);
        GUILayout.Label(sb.ToString());
    }

    public void Reset()
    {
    }

    void Update()
    {
        UpdateLow();
        UpdateButton();
        UpdatePosition();
        UpdateDrag();
        UpdatePress();
    }

    void UpdateLow()
    {
        On = Input.GetMouseButton(0);
        Position = Input.mousePosition;
    }

    void UpdateButton()
    {
        if (1 <= count)
        {
            if (On)
            {
                ++count;
            }
            else
            {
                count = 0;
            }
        }
        else
        {
            if (On)
            {
                count = 1;
            }
            else
            {
                --count;
            }
        }
        Down = (count == 1);
        Up = (count == 0);
    }

    void UpdatePress()
    {
        Press = Up && !Dragging;
    }


    void UpdatePosition()
    {
        if (Down)
        {
            FromPosition = Position;
        }
        if (Up)
        {
            ToPosition = Position;
        }
        Velocity = Position - LastPosition;
        LastPosition = Position;
    }

    void UpdateDrag()
    {
        DragStart = false;
        DragEnd = false;

        if (Dragging)
        {
            if( Up )
            {
                Dragging = false;
                DragEnd = true;
            }
        }
        else
        {
            if (On)
            {
                var d = Vector3.Distance(FromPosition, Position);
                if (DragStartBorder <= d)
                {
                    Dragging = true;
                    DragStart = true;
                }
            }
        }

    }
}
