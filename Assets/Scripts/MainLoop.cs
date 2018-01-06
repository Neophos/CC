using UnityEngine;
using System.Collections;

public class MainLoop : MonoBehaviour
{
    Character player1;
    Character player2;

    static ushort globalFrameNumber;

    bool fighting;
    bool menu;
    bool characterSelect;

    // Use this for initialization
    void Awake()
    {
        fighting = true;

        menu = false;

        characterSelect = false;

        player1 = GameObject.FindGameObjectWithTag("Player 1").GetComponent<Character>();
        //player2 = GameObject.FindGameObjectWithTag("Player 2").GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (fighting)
        {
            AddToGlobalFrameNumber();

            player1.Loop();
            //player2.Loop();

            //Network.Send();
        }
        else if (menu)
        {

        }
        else if (characterSelect)
        {

        }
    }

    void AddToGlobalFrameNumber()
    {
        globalFrameNumber++;
    }

    public static ushort GetGlobalFrameNumber()
    {
        return globalFrameNumber;
    }
}
