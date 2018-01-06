using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class Character : MonoBehaviour
{
    public string displayName;
    public Framedata framedata;
    public Characterdata characterdata;

    protected string scriptName;
    protected InputCollector inputCollector;
    protected InputHandler inputHandler;
    protected List<Structs.InputPacket> inputBuffer;
    protected Structs.InputPacket tempInput;
    protected GameObject opponent;
    protected List<Structs.MoveContainer> listOfMoves;
    protected List<Action> bufferedMoves;

    public virtual void Awake()
    {
        listOfMoves = new List<Structs.MoveContainer>();
        BuildMoveList();

        inputCollector = new InputCollector();
        inputBuffer = new List<Structs.InputPacket>();
        inputHandler = new InputHandler(this);

        // TODO: Make handler set these values
        if (transform.parent.tag == "Player 1")
        {
            opponent = GameObject.FindGameObjectWithTag("Player 2");
        }
        else if (transform.parent.tag == "Player 2")
        {
            opponent = GameObject.FindGameObjectWithTag("Player 1");
        }
    }

	// Use this for initialization
	public virtual void Start ()
    {
        
	}
	
	// Input collection and checking for netsent packages
    void Update()
    {
        tempInput = inputCollector.Loop(this);
	}

    void FixedUpdate()
    {
        Loop();
    }

    // Character logic, called by main loop
    public virtual void Loop()
    {
        HandleInput();

        CheckHitboxes();

        UpdateState();

        Flush();
    }

    // Return 0 if opponent is to your left, 1 if opponent is to your right, 2 if you are at exactly the same X-position
    public int CheckForward()
    {
        if (transform.position.x < opponent.transform.position.x)
        {
            return 1;
        }
        else if (transform.position.x > opponent.transform.position.x)
        {
            return 0;
        }
        else
        {
            return 2;
        }
    }

    // Enqueue input done during the frame, then check input for moves
    void HandleInput()
    {
        inputBuffer.Add(tempInput);

        CheckInput();
    }

    // Check input for moves
    void CheckInput()
    {
        inputHandler.Loop();
    }

    // Check for collisions, return on first collision
    void CheckHitboxes()
    {
        //TODO:
    }

    // Update state for the character
    void UpdateState()
    {
        //TODO:
    }

    // Clean up temporary stuff, get ready for next frame
    void Flush()
    {
        inputCollector.Flush();
    }

    public List<Structs.InputPacket> GetInputBuffer()
    {
        return inputBuffer;
    }

    public Framedata GetFramedata()
    {
        return framedata;
    }

    public List<Structs.MoveContainer> GetListOfMoves()
    {
        return listOfMoves;
    }

    public virtual void BuildMoveList()
    {
        // Override in all characters
    }
}
