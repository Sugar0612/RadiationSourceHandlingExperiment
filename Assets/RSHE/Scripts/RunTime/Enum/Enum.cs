
// Camera Tag
public enum CameraTag
{
    None,
    Manager,
    Player,
    WitnessFront,
    WitnessLeftSide,
    WitnessRightSide,
    Video,
    OverviewPlayer
}

// User's Identity
public enum EIdentity
{
    None = 0,
    A1 = 1, A2 = 2, 
    B1 = 3, B2 = 4, 
    C1 = 5, C2 = 6, C3 = 7,
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
    OverviewWindow = 1 << 6,
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

/// <summary> 任务箭头类型 </summary>
public enum ArrowType
{ 
    None,
    Horizontal,
    Vertical
}