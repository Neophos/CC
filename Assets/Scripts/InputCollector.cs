using UnityEngine;
using System.Collections;

public class InputCollector {

    // 8 bit struct
    // Bit #0 = up, #1 = down, #2 = forward, #3 = back, #4 = A, #5 = B, #6 = C, #7 = D
    Structs.InputPacket playerInput;

    public InputCollector()
    {
        playerInput = new Structs.InputPacket();
    }
	
	// Loop is called every frame by the character's update
	public Structs.InputPacket Loop(Character owner)
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (owner.CheckForward() == 0)
            {
                playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 3);
            }
            else if (owner.CheckForward() == 1)
            {
                playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 2);
            }
            else if (owner.CheckForward() == 2)
            {
                playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 2);
                playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 3);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (owner.CheckForward() == 0)
            {
                playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 2);
            }
            else if (owner.CheckForward() == 1)
            {
                playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 3);
            }
            else if (owner.CheckForward() == 2)
            {
                playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 3);
                playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 2);
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 4);
        }
        if (Input.GetKey(KeyCode.W))
        {
            playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 5);
        }
        if (Input.GetKey(KeyCode.E))
        {
            playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 6);
        }
        if (Input.GetKey(KeyCode.R))
        {
            playerInput.inputs = HelperFunctions.SetBit(playerInput.inputs, 7);
        }

        playerInput.frameNumber = MainLoop.GetGlobalFrameNumber();

        return playerInput;
	}

    public void Flush()
    {
        for (int i = 0; i < 8; i++)
        {
            playerInput.inputs = HelperFunctions.ClearBit(playerInput.inputs, i);
        }
    }
}
