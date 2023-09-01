using UnityEngine;
using UnityEngine.UI;

namespace Code.HUD.Menu
{
    public class StartGameButton : MonoBehaviour
    {
        private Button StartButton;

        private void Awake()
        {
            StartButton = gameObject.GetComponent<Button>();
            StartButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            Time.timeScale = 1;
            ScreenSwitcher.ShowScreen(ScreenType.Game);
        }
    }
}