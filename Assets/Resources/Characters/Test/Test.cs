using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : Character {

    public override void Awake()
    {
        base.Awake();
    }

	// Use this for initialization
	public override void Start()
    {
        base.Start();
	}

    void CheckInput()
    {

    }

    public void _A()
    {
        Debug.Log("5A");
    }

    public void _236A()
    {
        Debug.Log("236A");
    }

    public void _623A()
    {
        Debug.Log("623A");
    }

    public override void BuildMoveList()
    {
        // TODO: Build from external file

        listOfMoves.Add(new Structs.MoveContainer(11, "236A", _236A));
        listOfMoves.Add(new Structs.MoveContainer(10, "623A", _623A));
        listOfMoves.Add(new Structs.MoveContainer(100, "A", _A));
    }
}
