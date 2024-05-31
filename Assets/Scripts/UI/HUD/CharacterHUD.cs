using TMPro;
using UnityEngine;
using Worm;

public class CharacterHUD : MonoBehaviour
{
    public TextMeshProUGUI characterName;
    public HealthHUD healthHUD;
    public StaminaHUD staminaHUD;

    public WormController WormControllerReference { get; private set; }

    public void Initialize(WormController wormController)
    {
        WormControllerReference = wormController;
        
        healthHUD.Initialize(wormController.health);
        staminaHUD.Initialize(wormController.stamina);
    }
}
