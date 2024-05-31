using TMPro;
using UnityEngine;
using Worm;

public class CharacterHUD : MonoBehaviour
{
    public TextMeshProUGUI characterName;
    public HealthHUD healthHUD;
    public StaminaHUD staminaHUD;

    public void Initialize(WormController wormController)
    {
        healthHUD.Initialize(wormController.health);
        staminaHUD.Initialize(wormController.stamina);
    }
}
