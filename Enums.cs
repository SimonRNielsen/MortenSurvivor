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

        PopeStaff,
        Mitre,
        GeesusBlood,
        GeasterEgg,
        HolyWater

    }


    public enum ItemType
    {

        Rosary,
        Bible,
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
        Firepit,
        Stone,
        HayStack,
        Hay,
        Nest

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
        Loss,
        Win

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
        EnemiesKilled,
        XpUp,
        LevelUp,
        HealthUpdate,
        BarTop,
        BarViolet,
        BarBottom, 
        HealthBottom,
        HealthTop
    }

}
