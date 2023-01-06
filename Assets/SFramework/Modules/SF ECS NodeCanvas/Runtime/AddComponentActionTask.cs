using Leopotam.EcsLite;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFramework.ECS.Runtime;

namespace SFramework.ECS.Runtime.NodeCanvas
{
    [Category("ECS/Components")]
    public abstract class AddComponentActionTask<T> : ActionTask<SFEntity> where T : ISFComponent
    {
        public BBParameter<T> component;
        protected IEcsPool pool;

        protected override string OnInit()
        {
            pool = agent.World.GetPoolByType(component.varType);
            return base.OnInit();
        }


        protected override string info => $"<color=green>ADD</color> {component.varType.Name} TO <color=yellow>{(agent != null ? agent.gameObject.name : "NULL")}</color>";

        protected override void OnExecute()
        {
            if (agent.EcsPackedEntity.Unpack(agent.World, out var entity))
            {
                if (pool.Has(entity))
                {
                    pool.SetRaw(entity, component.value);
                }
                else
                {
                    pool.AddRaw(entity, component.value);
                }

                EndAction(true);
            }

            EndAction(false);
        }
    }
}