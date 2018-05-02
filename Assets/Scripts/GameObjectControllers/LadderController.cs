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
            if (other.gameObject.CompareTag("Player"))
            {
                _gameController.GetComponent<GameMasterController>().LevelUp();
            }
        }
    }
}