using System;
using UnityEngine;
using Util;
using Util.Inventory;

namespace Luna
{
    public class LevelExit : MonoBehaviour
    {
        [SerializeField] private CurrentRunData run;
        [SerializeField] private BasicSaveData save;
        [SerializeField] private InventoryKey wealthKey;
        [SerializeField] private string endSceneName;
        [SerializeField] private Vector2Int direction;


        public void MoveToNextLevel(Collider2D colin)
        {
            var inventroy = colin.GetComponent<IProvider<Inventory>>()?.Get();

            if (inventroy != null)
            {
                AggregateSlot wealth;
                if (inventroy.RetrieveSlot(wealthKey, out wealth))
                {
                    run.Score = wealth.Total;
                }
            }

            run.Depth++;
            run.LeftLastRoomBy = direction;

            if (save != null && save.DepthToWin == run.Depth)
            {
                SceneManagementBehaviour.LoadScene(endSceneName);
            }
            else
            {
                SceneManagementBehaviour.ReloadScene();
            }
        }
    }
}