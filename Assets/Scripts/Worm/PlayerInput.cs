using UnityEngine;
using Worm;

public class PlayerInput : WormInput
{
    void Update()
    {
        if (WormController.IsDead) return;
        
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