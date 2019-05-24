using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Framedata : ScriptableObject
{
    [System.Serializable]
    public class Move
    {
        // For easier viewing of frame data in editor
        public bool condensedView;

        // Various variables for moves
        public string animation;
        public string name;
        public string movelistName;
        public string input;
        public int framesOfStartup;
        public int numberOfHits;
        public List<Structs.Pair> activeAndRecovery;
        public int framesOfRecovery;
        public int meterBuildFactor;
        public bool throwInvincible;
        public AudioClip sound;
        public bool specialCancel;
        public bool superCancel;
        public bool jumpCancel;
        public int gatlingBracket;

        public List<Structs.HitPacket> hitData;
    }
    
    public List<Move> moves;

    void AddNew()
    {
        moves.Add(new Move());

    }

    void Remove(int index)
    {
        moves.RemoveAt(index);
    }
}
