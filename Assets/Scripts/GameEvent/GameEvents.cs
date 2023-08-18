using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameEvents
{
    public static GameEvent SelectedCharacter = new();
    public static GameEvent<float> PlayerSpeed = new();
    public static GameEvent<float> PlayerAnimSpeed = new();
}
