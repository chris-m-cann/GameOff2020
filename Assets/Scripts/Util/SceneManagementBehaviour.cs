using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class SceneManagementBehaviour : MonoBehaviour
    {
        public void LoadNextScene()
        {
            var current = SceneManager.GetActiveScene().buildIndex;
            var next = current + 1;

            if (next >= SceneManager.sceneCount)
            {
                next = 0;
            }

            SceneManager.LoadScene(next);
        }
    }
}