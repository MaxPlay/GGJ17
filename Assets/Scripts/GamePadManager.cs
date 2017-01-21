using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public static class GamePadManager
{
    static GamePadState[] currentState;
    static GamePadState[] previousState;

    private static float deathZoneTrigger;

    static GamePadManager()
    {
        currentState = new GamePadState[4];
        previousState = new GamePadState[4];
    }

    public static float DeathZoneTrigger
    {
        get { return deathZoneTrigger; }
        set { deathZoneTrigger = Mathf.Clamp01(value); }
    }

    public static void Update()
    {
        currentState.CopyTo(previousState, 0);
        for (int i = 0; i < 4; i++)
        {
            currentState[i] = GamePad.GetState((PlayerIndex)i);
            if (currentState[i].IsConnected && !previousState[i].IsConnected)
                OnConnected(i);
            if (currentState[i].IsConnected && !previousState[i].IsConnected)
                OnDisconnected(i);
        }
    }

    public delegate void GamePadEventHandler(GamePadEventArgs e);

    public class GamePadEventArgs : EventArgs
    {
        private GamePadState gamePadState;

        public GamePadState GamePadState
        {
            get { return gamePadState; }
            set { gamePadState = value; }
        }

        private PlayerIndex playerIndex;

        public PlayerIndex PlayerIndex
        {
            get { return playerIndex; }
            set { playerIndex = value; }
        }

        public GamePadEventArgs(GamePadState state, PlayerIndex index)
        {
            playerIndex = index;
            gamePadState = state;
        }
    }

    public static event GamePadEventHandler Connected;
    public static event GamePadEventHandler Disconnected;

    private static void OnConnected(int index)
    {
        if (Connected != null)
            Connected(new GamePadEventArgs(currentState[index], (PlayerIndex)index));
    }

    private static void OnDisconnected(int index)
    {
        if (Disconnected != null)
            Disconnected(new GamePadEventArgs(currentState[index], (PlayerIndex)index));
    }

    public static Vector2 ThumbLeft(PlayerIndex index)
    {
        GamePadThumbSticks.StickValue value = currentState[(int)index].ThumbSticks.Left;
        return new Vector2(value.X, value.Y);
    }

    public static Vector2 ThumbRight(PlayerIndex index)
    {
        GamePadThumbSticks.StickValue value = currentState[(int)index].ThumbSticks.Right;
        return new Vector2(value.X, value.Y);
    }

    public static GamePadDPad DPad(PlayerIndex index)
    {
        return currentState[(int)index].DPad;
    }

    public static bool DPadDown(PlayerIndex index)
    {
        return DPad(index).Down == ButtonState.Pressed;
    }

    public static bool DPadDownPressed(PlayerIndex index)
    {
        return DPad(index).Down == ButtonState.Pressed && previousState[(int)index].DPad.Down == ButtonState.Released;
    }

    public static bool DPadDownReleased(PlayerIndex index)
    {
        return DPad(index).Down == ButtonState.Released && previousState[(int)index].DPad.Down == ButtonState.Pressed;
    }

    public static bool DPadUp(PlayerIndex index)
    {
        return DPad(index).Up == ButtonState.Pressed;
    }

    public static bool DPadUpPressed(PlayerIndex index)
    {
        return DPad(index).Up == ButtonState.Pressed && previousState[(int)index].DPad.Up == ButtonState.Released;
    }

    public static bool DPadUpReleased(PlayerIndex index)
    {
        return DPad(index).Up == ButtonState.Released && previousState[(int)index].DPad.Up == ButtonState.Pressed;
    }

    public static bool DPadLeft(PlayerIndex index)
    {
        return DPad(index).Left == ButtonState.Pressed;
    }

    public static bool DPadLeftPressed(PlayerIndex index)
    {
        return DPad(index).Left == ButtonState.Pressed && previousState[(int)index].DPad.Left == ButtonState.Released;
    }

    public static bool DPadLeftReleased(PlayerIndex index)
    {
        return DPad(index).Left == ButtonState.Released && previousState[(int)index].DPad.Left == ButtonState.Pressed;
    }

    public static bool DPadRight(PlayerIndex index)
    {
        return DPad(index).Right == ButtonState.Pressed;
    }

    public static bool DPadRightPressed(PlayerIndex index)
    {
        return DPad(index).Right == ButtonState.Pressed && previousState[(int)index].DPad.Right == ButtonState.Released;
    }

    public static bool DPadRightReleased(PlayerIndex index)
    {
        return DPad(index).Right == ButtonState.Released && previousState[(int)index].DPad.Right == ButtonState.Pressed;
    }

    public enum ButtonType
    {
        A,
        B,
        Back,
        Guide,
        LeftShoulder,
        LeftStick,
        RightShoulder,
        RightStick,
        Start,
        X,
        Y
    }

    public static bool Button(PlayerIndex index, ButtonType type)
    {
        switch (type)
        {
            case ButtonType.A:
                return currentState[(int)index].Buttons.A == ButtonState.Pressed;
            case ButtonType.B:
                return currentState[(int)index].Buttons.B == ButtonState.Pressed;
            case ButtonType.Back:
                return currentState[(int)index].Buttons.Back == ButtonState.Pressed;
            case ButtonType.Guide:
                return currentState[(int)index].Buttons.Guide == ButtonState.Pressed;
            case ButtonType.LeftShoulder:
                return currentState[(int)index].Buttons.LeftShoulder == ButtonState.Pressed;
            case ButtonType.LeftStick:
                return currentState[(int)index].Buttons.LeftStick == ButtonState.Pressed;
            case ButtonType.RightShoulder:
                return currentState[(int)index].Buttons.RightShoulder == ButtonState.Pressed;
            case ButtonType.RightStick:
                return currentState[(int)index].Buttons.RightStick == ButtonState.Pressed;
            case ButtonType.Start:
                return currentState[(int)index].Buttons.Start == ButtonState.Pressed;
            case ButtonType.X:
                return currentState[(int)index].Buttons.X == ButtonState.Pressed;
            case ButtonType.Y:
                return currentState[(int)index].Buttons.Y == ButtonState.Pressed;
            default:
                return false;
        }
    }

    public static bool ButtonPressed(PlayerIndex index, ButtonType type)
    {
        switch (type)
        {
            case ButtonType.A:
                return currentState[(int)index].Buttons.A == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.A == ButtonState.Released;
            case ButtonType.B:
                return currentState[(int)index].Buttons.B == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.B == ButtonState.Released;
            case ButtonType.Back:
                return currentState[(int)index].Buttons.Back == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.Back == ButtonState.Released;
            case ButtonType.Guide:
                return currentState[(int)index].Buttons.Guide == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.Guide == ButtonState.Released;
            case ButtonType.LeftShoulder:
                return currentState[(int)index].Buttons.LeftShoulder == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.LeftShoulder == ButtonState.Released;
            case ButtonType.LeftStick:
                return currentState[(int)index].Buttons.LeftStick == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.LeftStick == ButtonState.Released;
            case ButtonType.RightShoulder:
                return currentState[(int)index].Buttons.RightShoulder == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.RightShoulder == ButtonState.Released;
            case ButtonType.RightStick:
                return currentState[(int)index].Buttons.RightStick == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.RightStick == ButtonState.Released;
            case ButtonType.Start:
                return currentState[(int)index].Buttons.Start == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.Start == ButtonState.Released;
            case ButtonType.X:
                return currentState[(int)index].Buttons.X == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.X == ButtonState.Released;
            case ButtonType.Y:
                return currentState[(int)index].Buttons.Y == ButtonState.Pressed &&
                    previousState[(int)index].Buttons.Y == ButtonState.Released;
            default:
                return false;
        }
    }

    public static bool ButtonReleased(PlayerIndex index, ButtonType type)
    {
        switch (type)
        {
            case ButtonType.A:
                return currentState[(int)index].Buttons.A == ButtonState.Released &&
                    previousState[(int)index].Buttons.A == ButtonState.Pressed;
            case ButtonType.B:
                return currentState[(int)index].Buttons.B == ButtonState.Released &&
                    previousState[(int)index].Buttons.B == ButtonState.Pressed;
            case ButtonType.Back:
                return currentState[(int)index].Buttons.Back == ButtonState.Released &&
                    previousState[(int)index].Buttons.Back == ButtonState.Pressed;
            case ButtonType.Guide:
                return currentState[(int)index].Buttons.Guide == ButtonState.Released &&
                    previousState[(int)index].Buttons.Guide == ButtonState.Pressed;
            case ButtonType.LeftShoulder:
                return currentState[(int)index].Buttons.LeftShoulder == ButtonState.Released &&
                    previousState[(int)index].Buttons.LeftShoulder == ButtonState.Pressed;
            case ButtonType.LeftStick:
                return currentState[(int)index].Buttons.LeftStick == ButtonState.Released &&
                    previousState[(int)index].Buttons.LeftStick == ButtonState.Pressed;
            case ButtonType.RightShoulder:
                return currentState[(int)index].Buttons.RightShoulder == ButtonState.Released &&
                    previousState[(int)index].Buttons.RightShoulder == ButtonState.Pressed;
            case ButtonType.RightStick:
                return currentState[(int)index].Buttons.RightStick == ButtonState.Released &&
                    previousState[(int)index].Buttons.RightStick == ButtonState.Pressed;
            case ButtonType.Start:
                return currentState[(int)index].Buttons.Start == ButtonState.Released &&
                    previousState[(int)index].Buttons.Start == ButtonState.Pressed;
            case ButtonType.X:
                return currentState[(int)index].Buttons.X == ButtonState.Released &&
                    previousState[(int)index].Buttons.X == ButtonState.Pressed;
            case ButtonType.Y:
                return currentState[(int)index].Buttons.Y == ButtonState.Released &&
                    previousState[(int)index].Buttons.Y == ButtonState.Pressed;
            default:
                return false;
        }
    }

    public static float TriggerLeft(PlayerIndex index)
    {
        return currentState[(int)index].Triggers.Left;
    }

    public static float TriggerRight(PlayerIndex index)
    {
        return currentState[(int)index].Triggers.Right;
    }

    public static bool TriggerLeftPressed(PlayerIndex index)
    {
        return currentState[(int)index].Triggers.Left > deathZoneTrigger;
    }

    public static bool TriggerLeftReleased(PlayerIndex index)
    {
        return currentState[(int)index].Triggers.Left <= deathZoneTrigger;
    }

    public static bool TriggerRightPressed(PlayerIndex index)
    {
        return currentState[(int)index].Triggers.Right > deathZoneTrigger;
    }

    public static bool TriggerRightReleased(PlayerIndex index)
    {
        return currentState[(int)index].Triggers.Right <= deathZoneTrigger;
    }

    public static bool IsConnected(PlayerIndex index)
    {
        return currentState[(int)index].IsConnected;
    }
}
