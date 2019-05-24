using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Characterdata : ScriptableObject
{
    [System.Serializable]
    public class Character
    {
        // Various variables for characters
        public string characterName;

        public int forwardWalkingSpeed;
        public int backwardsWalkingSpeed;

        public bool hasAForwardRun;
        public int forwardRunSpeed;
        public float forwardRunAcceleration;
        public bool hasAForwardDash;
        public bool hasAForwardHoverDash;
        public bool hasAForwardTeleportDash;
        public float forwarddashSpeed;

        public bool hasABackRun;
        public bool hasABackDash;
        public bool hasABackHoverDash;
        public bool hasABackTeleportDash;
        public float backdashSpeed;
        public float backrunSpeed;

        public int numberOfJumps;
        public float airControlFactor;
        public float verticalJumpSpeed;
        public float horizontalJumpSpeed;

        public Framedata framedata;
    }

    public Character character;
}