using Nebby;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    public class MrBonesApplication : MainGameBehaviour<MrBonesApplication>
    {
        public override string GameLoadingSceneName => "loadingscene";

        public override string LoadingFinishedSceneName => "mainmenu";

        protected override IEnumerator LoadGameContent()
        {
            EntityStateCatalog.Initialize();
            yield return new WaitForEndOfFrame();
        }
    }
}