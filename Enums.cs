namespace MortenSurvivor
{


    public enum MusicTrack
    {

        BattleMusic,
        BackgroundMusic

    }


    public enum Sound
    {

        PlayerWalk1,
        PlayerWalk2,
        PlayerShoot,
        PlayerTakeDamage,
        PlayerHeal,
        EnemyHonk,
        ProjectileSmashed,
        MagicShoot,
        PowerUpSound

    }


    public enum UpgradeType
    {

        Mitre,
        Rosary,
        Bible,
        WallGoose,
        PopeStaff,
        GeasterEgg

    }


    public enum ItemType
    {

        XPCrystal,
        DamageBoost,
        SpeedBoost,
        HealBoost,
        ConfuseEnemy,
        ScareEnemy

    }


    public enum PlayerType
    {

        UndercoverMortenWalk

    }


    public enum ProjectileType
    {

        Eggs,
        GeasterEgg,
        Magic

    }


    public enum EnvironmentTile
    {

        TopLeft,
        Top,
        TopRight,
        Left,
        Center,
        Right,
        BottomLeft,
        Bottom,
        BottomRight,
        AvSurface,
        Room,
        Firepit

    }


    public enum WeaponType
    {

        Sling,
        GeasterSling,
        PopeStaff

    }


    public enum EnemyType
    {

        Slow,
        SlowChampion,
        Fast,
        FastChampion,
        Goosifer

    }


    public enum DEBUGItem
    {

        DEBUGPixel

    }


    public enum MenuItem
    {

        StackableButton,
        SingleButton,
        MouseCursor,
        Start,
        Pause,
        Upgrade,
        Loss

    }

    public enum MouseKeys
    {
        LeftButton,
        RightButton,
        MiddleButton,
        xButton1,
        xButton2
    }

    public enum StatusType
    {
        XpUp,
        LevelUp,
        HealthUpdate,
        EnemiesKilled        
    }

}
