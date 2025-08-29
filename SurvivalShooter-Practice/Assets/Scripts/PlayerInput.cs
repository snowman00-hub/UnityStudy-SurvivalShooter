using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string AxisHorizontal = "Horizontal";
    public static readonly string AxisVertical = "Vertical";

    public float HorizontalMove { get; private set;}
    public float VerticalMove { get; private set;}

    private void Update()
    {
        HorizontalMove = Input.GetAxisRaw(AxisHorizontal);
        VerticalMove = Input.GetAxisRaw(AxisVertical);
    }
}
