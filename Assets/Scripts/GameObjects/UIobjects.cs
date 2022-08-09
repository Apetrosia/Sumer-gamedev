using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class UIobjects : EcsMonoBehaviour
    {
        public Text HPBarTextobject;


        public override void InitEntity(EcsWorld ecsWorld)
        {
            base.InitEntity(ecsWorld);
            ref var hpcomponent = ref ecsWorld.GetPool<HPBarComponent>().Add(entity.index);

            hpcomponent.HPtext = HPBarTextobject;
        }
    }
}