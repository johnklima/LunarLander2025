using UnityEngine.InputSystem;

public static class InputsManager
{
    private static readonly SpaceShipInput input;

    static InputsManager()
    {
        input = new SpaceShipInput();
        input.Gameplay.Enable();
    }

    public static SpaceShipInput.GameplayActions Player
    {
        get { return input.Gameplay; }
    }

    public static SpaceShipInput.UIActions UI
    {
        get { return input.UI; }
    }

    public static void EnablePlayerMap(bool enable)
    {
        if (enable) input.Gameplay.Enable();
        else input.Gameplay.Disable();
    }

    public static void EnableUIMap(bool enable)
    {
        if (enable) input.UI.Enable();
        else input.UI.Disable();
    }

    public static bool GetIsCurrentDiviceMouse(PlayerInput playerInput)
    {
        return playerInput.currentControlScheme == "Keyboard&Mouse";
    }

    public static void SwitchToUI()
    {
        EnablePlayerMap(false);
        EnableUIMap(true);
    }

    public static void SwitchToPlayer()
    {
        EnableUIMap(false);
        EnablePlayerMap(true);
    }
}
