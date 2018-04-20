using BehaviourControllers;
using NUnit.Framework;
using UnityEngine;

public class CameraBehaviourTest {

	[Test]
	public void CalculateDirectionIsDifferentFromOriginal()
	{
		CameraMovementBehaviourController movement =
			new CameraMovementBehaviourController {Offset = new Vector3(0, 10, 0), Interpolation = 0.5f};
		Vector3 currentPosition = new Vector3(10,11,5);
		Vector3 playerPosition = new Vector3(11,1,4);
		float deltaTime = 0.01f;

		Vector3 newPosition = movement.CalculateDirection(currentPosition, playerPosition, deltaTime);
            
		Assert.AreNotEqual(currentPosition, newPosition);
	}
}
