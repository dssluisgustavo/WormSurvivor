using UnityEngine;
using Worm;

public class PlayerInput : WormInput
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WormController.Hide();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            WormController.Unhide();
        }
    }
}