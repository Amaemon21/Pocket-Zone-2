using UnityEngine;

namespace Inventory
{
    public class GameStatePlayerProvider : IGameStateProvider
    {
        private const string KEY = "GAME STATE";

        public GameStateData GameState { get; private set; }

        public void SaveGameState()
        {
            throw new System.NotImplementedException();
        }

        public void LoadGameState()
        {
            if (PlayerPrefs.HasKey(KEY))
            {
                var json = PlayerPrefs.GetString(KEY);
                GameState = JsonUtility.FromJson<GameStateData>(json);
            }
        }
    }
}