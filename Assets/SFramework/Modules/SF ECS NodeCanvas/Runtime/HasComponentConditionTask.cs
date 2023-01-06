using Leopotam.EcsLite;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using SFramework.ECS.Runtime;

namespace SFramework.ECS.Runtime.NodeCanvas
{
    [Category("ECS/Components")]
    public abstract class HasComponentConditionTask<T> : ConditionTask<SFEntity> where T : ISFComponent
    {
        private IEcsPool pool;

        protected override string OnInit()
        {
            pool = agent.World.GetPoolByType(typeof(T));
            return base.OnInit();
        }

        protected override string info =>
            $"<color=yellow>{agentParameterName}</color> <color=green>HAS</color> <color=magenta>{typeof(T).Name}</color>";

        protected override bool OnCheck()
        {
            return agent.EcsPackedEntity.Unpack(agent.World, out var entity) && pool.Has(entity);
        }
    }
}