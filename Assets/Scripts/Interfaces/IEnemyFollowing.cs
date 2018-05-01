using UnityEngine;

namespace Interfaces
{
    // All enemies that follow the player implement this
    public interface IEnemyFollowing
    {
        
        // When enemy has to move
        void Move(Vector3 movement);
        
        // When enemy has to rotate
        void Rotate(Quaternion rotation);
    }
}