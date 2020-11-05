using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class RestartOnKeyPress : MonoBehaviour
    {
        [SerializeField] private KeyCode key;

        private void Update()
        {
            if (Input.GetKeyDown(key)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}