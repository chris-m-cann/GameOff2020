using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Luna
{
    [CreateAssetMenu(menuName = "Custom/SaveData")]
    public class BasicSaveData : ScriptableObject
    {
        [Serializable]
        public struct RunData
        {
            public int Depth;
            public int Score;
        }

        public List<RunData> Runs = new List<RunData>();
        [SerializeField] private int maxRunsStored = 5;
        public int DepthToWin = 5;


        public void SaveCurrentRun(CurrentRunData run)
        {
            Runs.Add(new RunData
            {
                Depth = run.Depth,
                Score = run.Score
            });

            ClampRuns();
            SaveToPrefs();
        }

        private void ClampRuns()
        {
            Runs = Runs.OrderBy(s => s.Depth).ThenBy(s => s.Score).Take(maxRunsStored).ToList();
        }

        public void LoadFromPrefs()
        {
            var json = PlayerPrefs.GetString("SaveData", "");

            if (json == "") return;

            JsonUtility.FromJsonOverwrite(json, this);
        }

        public void SaveToPrefs()
        {
            ClampRuns();

            var json = JsonUtility.ToJson(this);

            PlayerPrefs.SetString("SaveData", json);
            PlayerPrefs.Save();
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey("SaveData");
            PlayerPrefs.Save();
        }
    }
}