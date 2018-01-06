using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputHandler
{
    Character owner;
    List<Structs.InputPacket> buffer;
    List<Structs.MoveContainer> listOfMoves;
    Framedata characterFramedata;

    Globals globals;

    Action moveToExecute;

    int individualBufferCounter;
    int lastInputFound;
    int totalBufferCounter;
    int lastInputCounter;
    int offsetCounter;

    public InputHandler(Character character)
    {
        owner = character;
        buffer = owner.GetInputBuffer();
        listOfMoves = owner.GetListOfMoves();
        characterFramedata = owner.GetFramedata();

        globals = Resources.Load<Globals>("Globals");

        GetAllInputs();
    }

    public void Loop()
    {
        int i = 0;

        while (i < listOfMoves.Count)
        {
            if (CheckIfInputWasJustPressed('A'))
            {
                i = i;
            }

            // Go through dictionary
            if (CheckBufferForInputString(listOfMoves[i].input))
            {
                //moveToExecute = listOfMoves[i].moveFunction;


                // TODO: Put in action queue rather than invoking

                listOfMoves[i].moveFunction.Invoke();

                break;
            }

            i++;
        }
    }

    void GetAllInputs()
    {
        for (int i = 0; i < characterFramedata.moves.Count; i++)
        {
            
        }
    }

    bool CheckBufferForInputString(string move)
    {
        char[] moveInput = move.ToCharArray();

        lastInputFound = 0;
        individualBufferCounter = 0;
        totalBufferCounter = 0;
        offsetCounter = 0;

        lastInputCounter = 0;

        if (CheckIfInputWasJustPressed(moveInput[moveInput.Length - 1]))
        {
            // Input was pressed last frame

            offsetCounter++;

            // Move is only one input long
            if (moveInput.Length == 1)
            {
                return true;
            }

            // Move is several inputs long
            // 
            for (int i = moveInput.Length - 2; i >= 0; i--)
            {
                // All inputs but first
                if (i > 0)
                {
                    if(CheckIfInputWasPressedWithinDuration(moveInput[i], ref offsetCounter))
                    {
                        continue;
                    }

                    return false;
                }

                // First input in move
                if (i == 0)
                {
                    if (CheckIfInputWasPressedWithinDuration(moveInput[i], ref offsetCounter))
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
        
        return false;
    }

    bool CheckIfInputIsSetInByte(byte inputByte, char inputChar)
    {
        // 10 bit struct
        // Bit #0 = up, #1 = down, #2 = forward, #3 = back, #4 = A, #5 = B, #6 = C, #7 = D
        if (inputChar == '1')
        {
            if (HelperFunctions.ReadBit(inputByte, 1) && HelperFunctions.ReadBit(inputByte, 3))
            {
                return true;
            }
        }
        else if (inputChar == '2')
        {
            if (HelperFunctions.ReadBit(inputByte, 1))
            {
                return true;
            }
        }
        else if (inputChar == '3')
        {
            if (HelperFunctions.ReadBit(inputByte, 1) && HelperFunctions.ReadBit(inputByte, 2))
            {
                return true;
            }
        }
        else if (inputChar == '4')
        {
            if (HelperFunctions.ReadBit(inputByte, 3))
            {
                return true;
            }
        }
        else if (inputChar == '5')
        {
            // No direction is pressed
            if (!HelperFunctions.ReadBit(inputByte, 0)
               && !HelperFunctions.ReadBit(inputByte, 1)
               && !HelperFunctions.ReadBit(inputByte, 2)
               && !HelperFunctions.ReadBit(inputByte, 3))
            {
                return true;
            }
        }
        else if (inputChar == '6')
        {
            if (HelperFunctions.ReadBit(inputByte, 2))
            {
                return true;
            }
        }
        else if (inputChar == '7')
        {
            if (HelperFunctions.ReadBit(inputByte, 3) && HelperFunctions.ReadBit(inputByte, 0))
            {
                return true;
            }
        }
        else if (inputChar == '8')
        {
            if (HelperFunctions.ReadBit(inputByte, 0))
            {
                return true;
            }
        }
        else if (inputChar == '9')
        {
            if (HelperFunctions.ReadBit(inputByte, 0) && HelperFunctions.ReadBit(inputByte, 2))
            {
                return true;
            }
        }
        else if (inputChar == 'A')
        {
            if (HelperFunctions.ReadBit(inputByte, 4))
            {
                return true;
            }
        }
        else if (inputChar == 'B')
        {
            if (HelperFunctions.ReadBit(inputByte, 5))
            {
                return true;
            }
        }
        else if (inputChar == 'C')
        {
            if (HelperFunctions.ReadBit(inputByte, 6))
            {
                return true;
            }
        }
        else if (inputChar == 'D')
        {
            if (HelperFunctions.ReadBit(inputByte, 7))
            {
                return true;
            }
        }

        return false;
    }

    bool CheckIfInputWasJustPressed(char inputChar)
    {
        // 10 bit struct
        // Bit #0 = up, #1 = down, #2 = forward, #3 = back, #4 = A, #5 = B, #6 = C, #7 = D

        // TODO: Go through buffer, return true if the input wasn't present last frame

        if (CheckIfInputIsSetInByte(buffer[buffer.Count - 1].inputs, inputChar))
        {
            if (!CheckIfInputIsSetInByte(buffer[buffer.Count - 2].inputs, inputChar))
            {
                return true;
            }
        }

        return false;
    }

    // Returns true if button was pressed within duration
    bool CheckIfInputWasPressedWithinDuration(char inputChar, ref int offset, int duration = 0)
    {
        // 10 bit struct
        // Bit #0 = up, #1 = down, #2 = forward, #3 = back, #4 = A, #5 = B, #6 = C, #7 = D

        // TODO: Go through buffer, return true if the input was pressed within the last <duration> frames

        bool inputFound = false;

        if (duration == 0)
        {
            duration = globals.moveInputIndividualBufferLength;
        }

        for (int i = 0; i < duration; i++)
        {
            if (CheckIfInputIsSetInByte(buffer[buffer.Count - 1 - i - offset].inputs, inputChar))
            {
                inputFound = true;

                offset++;
                totalBufferCounter++;

                break;
            }

            offset++;
            totalBufferCounter++;

            if (totalBufferCounter > globals.moveInputTotalBufferLength)
            {
                return false;
            }
        }

        if (inputFound)
        {
            for (int i = 0; i < duration; i++)
            {
                offset++;
                totalBufferCounter++;

                if (!CheckIfInputIsSetInByte(buffer[buffer.Count - 1 - i - offset].inputs, inputChar))
                {
                    return true;
                }

                if (totalBufferCounter > globals.moveInputTotalBufferLength)
                {
                    return false;
                }
            }
        }

        return false;
    }

    // Returns number of frames ago the button was pressed. Returns -1 if no input was found.
    int CheckIfInputWasPressed(char inputChar, int offset = 0)
    {
        // 10 bit struct
        // Bit #0 = up, #1 = down, #2 = forward, #3 = back, #4 = A, #5 = B, #6 = C, #7 = D

        // TODO: Go through buffer, return true if the input was pressed within the last <duration> frames

        bool inputFound = false;
        int frameCounter = 0;

        for (int i = 0; i < globals.moveInputIndividualBufferLength; i++)
        {
            frameCounter++;
            totalBufferCounter++;

            if (CheckIfInputIsSetInByte(buffer[buffer.Count - 1 - i - offset].inputs, inputChar))
            {
                inputFound = true;

                break;
            }

            if (totalBufferCounter > globals.moveInputTotalBufferLength)
            {
                return -1;
            }
        }

        if (inputFound)
        {
            for (int i = 0; i < globals.moveInputIndividualBufferLength; i++)
            {
                frameCounter++;
                totalBufferCounter++;

                if (!CheckIfInputIsSetInByte(buffer[buffer.Count - 1 - i - offset - frameCounter].inputs, inputChar))
                {
                    return frameCounter;
                }

                if (totalBufferCounter > globals.moveInputTotalBufferLength)
                {
                    return -1;
                }
            }
        }

        return -1;
    }

    bool CheckIfInputWasReleased(char inputChar)
    {
        // 10 bit struct
        // Bit #0 = up, #1 = down, #2 = forward, #3 = back, #4 = A, #5 = B, #6 = C, #7 = D

        // TODO: Go through buffer, return true if the input was present last frame but isn't now

        return false;
    }

    bool CheckIfInputWasHeld(int duration, char inputChar)
    {
        // 10 bit struct
        // Bit #0 = up, #1 = down, #2 = forward, #3 = back, #4 = A, #5 = B, #6 = C, #7 = D

        // TODO: Go through buffer, return true if the input has been held at least duration frames

        return false;
    }
}
