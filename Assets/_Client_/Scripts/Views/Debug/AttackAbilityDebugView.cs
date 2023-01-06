using SFramework.Core.Runtime;
using SFramework.Generated;
using UnityEngine;

namespace _Client_.Scripts.Views.Debug
{
    public class AttackAbilityDebugView : SFView
    {
        [SerializeField]
        private _AttackAbility _attackAbility;

        private void OnDrawGizmos()
        {
            if (_attackAbility == null) return;
            Gizmos.DrawWireSphere(_attackAbility.transform.position, _attackAbility.value.attackRadius);
        }
    }
}