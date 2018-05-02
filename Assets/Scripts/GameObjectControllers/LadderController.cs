using UnityEngine;

namespace GameObjectControllers
{
    public class LadderController : MonoBehaviour
    {
        private GameObject _gameController;

        void Start()
        {
            _gameController = GameObject.FindWithTag("GameController");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("In OnTriggerEnter");
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("It is a player!");
                _gameController.GetComponent<GameMasterController>().LevelUp();
            }
        }
    }
}