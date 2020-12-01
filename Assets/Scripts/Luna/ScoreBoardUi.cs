using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Luna
{
    public class ScoreBoardUi : MonoBehaviour
    {
        [SerializeField] private BasicSaveData saveData;

        [SerializeField] private Transform layoutGroup;
        [SerializeField] private RunUi entryPrefab;


        private void Start()
        {
            saveData.LoadFromPrefs();
            var runs = saveData.Runs.OrderBy(it => it.Depth).ThenBy(it => it.Score);

            foreach (var run in runs)
            {
                var ui = Instantiate(entryPrefab, layoutGroup).GetComponent<RunUi>();
                ui.DisplayRun(run);
            }
        }
    }
}