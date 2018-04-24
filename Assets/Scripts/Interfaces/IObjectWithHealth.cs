using UnityEngine;

namespace Interfaces
{
    // Player and Enemy implement this, as they have health and can be killed
    public interface IObjectWithHealth
    {
        
        // When getting hit
        void ChangeColor(Color newColor);
        
        // When killed
        void Die();
    }
}