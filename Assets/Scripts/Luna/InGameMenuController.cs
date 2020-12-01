using System;
using UnityEngine;

namespace Luna
{
    public class InGameMenuController : MonoBehaviour
    {
        enum MenuState
        {
            Game,
            Paused,
            Dead
        }

        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject deathMenu;
        [SerializeField] private GameObject backPannel;

        private MenuState _state = MenuState.Game;
        private float _previousScale = 1f;

        public void OnPlayerDeath()
        {
            backPannel.SetActive(true);
            pauseMenu.SetActive(false);
            deathMenu.SetActive(true);

            _state = MenuState.Dead;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (_state)
                {
                    case MenuState.Game:
                        Pause();
                        break;
                    case MenuState.Paused:
                        Play();
                        break;
                    case MenuState.Dead:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Play()
        {
            backPannel.SetActive(false);
            deathMenu.SetActive(false);
            pauseMenu.SetActive(false);
            Time.timeScale = _previousScale;
            _state = MenuState.Game;
        }

        public void Pause()
        {
            _previousScale = Time.timeScale;
            Time.timeScale = 0f;

            backPannel.SetActive(true);
            deathMenu.SetActive(false);
            pauseMenu.SetActive(true);
            _state = MenuState.Paused;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}