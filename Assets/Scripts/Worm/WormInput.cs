using UnityEngine;

namespace Worm
{
    public class WormInput : MonoBehaviour
    {
        protected WormController WormController { get; private set; }
    
        public void Setup(WormController wormController)
        {
            WormController = wormController;
        }
    }
}