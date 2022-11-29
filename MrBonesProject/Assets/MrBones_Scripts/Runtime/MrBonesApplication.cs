using Nebby;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace MrBones
{
    public class MrBonesApplication : MainGameBehaviour<MrBonesApplication>
    {

        protected override IEnumerator LoadGameContent()
        {
            EntityStateCatalog.Initialize();
            yield return new WaitForEndOfFrame();
        }
    }
}