using TMPro;
using UnityEngine;
using Worm;

namespace UI.HUD
{
    public class CharacterHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private HealthHUD healthHUD;
        [SerializeField] private StaminaHUD staminaHUD;

        public WormController WormControllerReference { get; private set; }

        public void Initialize(WormController wormController)
        {
            WormControllerReference = wormController;

            healthHUD.Initialize(wormController.Health);
            staminaHUD.Initialize(wormController.Stamina);
        }
    }
}