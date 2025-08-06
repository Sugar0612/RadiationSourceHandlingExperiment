
// Camera Tag
public enum CameraTag
{
    None,
    Manager,
    Player,
    WitnessFront,
    WitnessLeftSide,
    WitnessRightSide,
    Video
}

// User's Identity
public enum EIdentity
{
    None = 0,
    A1 = 1, A2 = 2, A3 = 3,
    B1 = 4, B2 = 5, B3 = 6,
    C1 = 7, C2 = 8, C3 = 9,
}

// User's body parts
public enum EBodyParts
{
    None,
    Head,
    RightHand,
    LeftHand,
    Foot
}

// User's Network State
public enum EUserState
{
    Offline = 0,
    Online = 1,
    Neno = 2
}

// Windows Type.
public enum EWindowType
{
    None = 0,
    MainWindow = 1 << 0,
    SceneWindow = 1 << 1,
    UserWindow = 1 << 2,
    VideoWindow = 1 << 3,
    VRJoinWindow = 1 << 4,
    GameWinow = 1 << 5,
    // VRVideoWindow = 1 << 6,
}

// Game Collider Trigger Mode
public enum GameColliderTriggerMode
{
    Multiplayer,
    Single
}

/// <summary> 游戏模式 </summary>
public enum EGameMode
{
    None,
    Teaching, // 教学
    PracticalTraining, // 实训
    SelfTest, // 自测
    Assessment // 考核
}