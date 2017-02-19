using UnityEngine;

public enum InputType
{
    Tap,
    RightTap,

    Left,
    Right,
    Up,
    Down,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight,
}

public class InputHelper
{
    static InputHelper _instance;

    public static InputHelper Instance
    {
        get
        {
            if (_instance == null)
                _instance = new InputHelper();
            return _instance;
        }
    }

    float minSwipeLength = 150f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;

    public InputType? CheckInput()
    {
        if (UserData.Instance.Accessibility)
        {
            if (Input.GetButtonDown("Accessibility Button 1"))
            {
                return InputType.Tap;
            }

            if (Input.GetButtonDown("Accessibility Button 2"))
            {
                return InputType.RightTap;
            }

            return null;
        }

        var mouse = MouseSwipe();
        if (mouse != null) return mouse;

        if (Input.GetButtonDown("Jump"))
            return InputType.Tap;

        var gamepad = GamepadMove();
        if (gamepad != null)
            return gamepad;

        return null;
    }

    private InputType? MouseSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            return ProcessSwipe();
        }

        return null;
    }

    private InputType? GamepadMove()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var currentMovement = new Vector3(h, 0, v);
        if (currentMovement.sqrMagnitude > 0.1f)
        {
            currentMovement.Normalize();
            return ProcessMovement(currentMovement);
        }

        return null;
    }

    private InputType? ProcessSwipe()
    {
        var currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

        if (currentSwipe.magnitude < minSwipeLength)
            return InputType.Tap;

        currentSwipe.Normalize();
        return ProcessMovement(currentSwipe);
    }

    private static InputType? ProcessMovement(Vector3 currentMovement)
    {
        if (currentMovement.x > 0)
        {
            return InputType.Right;
        }
        else
        {
            return InputType.Left;
        }
    }
}