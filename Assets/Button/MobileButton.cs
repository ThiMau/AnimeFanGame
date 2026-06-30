using UnityEngine;
using UnityEngine.EventSystems;

public class MobileButton : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler
{
    public VegitoController player;

    public ButtonType buttonType;

    public enum ButtonType
    {
        Left,
        Right,
        Jump
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Left:
                player.MoveLeft();
                break;

            case ButtonType.Right:
                player.MoveRight();
                break;

            case ButtonType.Jump:
                player.InputJump();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonType == ButtonType.Left ||
            buttonType == ButtonType.Right)
        {
            player.StopMove();
        }
    }
}