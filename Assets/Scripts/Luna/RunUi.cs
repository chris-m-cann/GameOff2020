using TMPro;
using UnityEngine;

namespace Luna
{
    public class RunUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text depth;
        [SerializeField] private TMP_Text score;

        public void DisplayRun(BasicSaveData.RunData run)
        {
            depth.text = run.Depth.ToString();
            score.text = run.Score.ToString();
        }

    }
}