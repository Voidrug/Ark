using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum ConditionType
    {
        Location,
        AnimName,
        End,
    }

    public enum Team 
    {
        Team00,
        Team01,
        Team02,
        End,
    }

    public enum RangeType
    {
        Grid,
        Circle,
    }

    public enum MessageType
    {
        Signal,
        UnitBirth,
        WeaponStart,
        WeaponStop,
        ActorCreate,
        UnitDamaged,
    }

    public enum ActionType
    {
        AnimPlay,
        AnimStop,
    }
    public enum Location
    {
        Caster,
        Creator,
        Target,
        End,
    }

    [Flags]
    public enum LMFlag
    {
        CasterDir = 1 << 0, // 1
        Option2 = 1 << 1, // 2
        Option3 = 1 << 2, // 4
        Option4 = 1 << 3  // 8
    }

    public enum Attribute
    {
        None,
        Guard,
        Caster,
        Vanguard,
        Supporter,
        Medic,
        Defender,
        Sniper,
        Specialist,
        End,
    }

    public enum CharTier
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
    }

    public enum DamageType
    {
        Physics,
        Magic,
        True,
        Element,
        End,
    }
    public enum DamageFlag
    {
        Kill = 1 << 0, // 1 //유닛을 피해량에 관계없이 즉시 처치합니다
        Live = 1 << 1, // 2 //적용된 피해는 체력을 1이하로 만들 수 없습니다
        Option3 = 1 << 2, // 4
        Option4 = 1 << 3  // 8
    }

    public enum Operator
    {
        Add,
        Multiply,
        AdditiveMultiply,
    }

    public enum UnitPos
    {
        Floor = 1 << 0, // 1
        Cliff = 1 << 1, // 2
    }

    [Flags]
    public enum Filter
    {
        Surface = 1 << 0,
        Fly = 1 << 1,
        Missile = 1 << 2,
        Oneself = 1 << 3,
        Invincible = 1 << 4,
        Stasis = 1 << 5,
    }

    [Flags]
    public enum Aliance
    {
        Player = 1 << 0,
        Aliance = 1 << 1,
        Netural = 1 << 2,
        Enemy = 1 << 3,
    }

    [Flags]
    public enum Alias
    {
        Unit = 1 << 0,
        Missile = 1 << 1,
        Decal = 1 << 2,
        Status
    }
}
