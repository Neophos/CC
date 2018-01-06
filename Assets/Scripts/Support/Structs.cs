using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class Structs 
{
    [System.Serializable]
    public class Pair
    {
        public int first;
        public int second;
    }

    [System.Serializable]
    public class MoveContainer
    {
        public MoveContainer(int prio, string inp, Action moveFunc)
        {
            priority = prio;
            input = inp;
            moveFunction = moveFunc;
        }

        public int priority;
        public string input;
        public Action moveFunction;
    }

    // A packet containing a frame's worth of input.
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct InputPacket
    {
        public ushort frameNumber;
        public byte inputs;
    }

    // Every hit in every attack has a separate hitpacket that contains information about it.
    [System.Serializable]
    public class HitPacket
    {
        // Settings
        public bool absoluteTrajectory;
        public bool customTrajectory;
        public bool customStun;
        public bool floatsOnHit;
        public bool floatsOnBlock;
        
        // Always set by hand
        public int damage;
        public int level;
        public TypeOfAttack typeOfAttack;
        
        // Custom according to settings, otherwise automatically set
        public Vector2 trajectoryPath;
        public float blockDamageFactor;
        public int initialStun;
        public int hitstun;
        public int blockstun;
    }

    // All generic states.
    [System.Serializable]
    public enum StandardStates
    {
        STANDING = 0,
        CROUCHING,
        PREJUMPING,
        FLOATING,
        DASHING,
        RUNNING,
        STANDINGHITSTUN,
        CROUCHINGHITSTUN,
        FLOATINGHITSTUN,
        STANDINGBLOCKSTUN,
        CROUCHINGBLOCKSTUN,
        FLOATINGBLOCKSTUN,
        STANDINGRECOVERY,
        CROUCHINGRECOVERY,
        FLOATINGRECOVERY,
        INITIALSTUN,
        INITIALSTUNBREAK,
        INVINCIBLE,
        THROWINVINCIBLE,
        STRIKEINVINCIBLE,
        SUPERARMOR,
        GUARDPOINT,
        COUNTERHIT
    }

    [System.Serializable]
    public enum TypeOfAttack
    {
        MID = 0,
        LOW,
        OVERHEAD,
        THROW,
        COMMANDTHROW,
        UNBLOCKABLE,
        CATCH,
        PARRY
    }
}
