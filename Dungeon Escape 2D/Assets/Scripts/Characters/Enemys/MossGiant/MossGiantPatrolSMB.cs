using Gamekit2D;
using UnityEngine;

namespace DCode
{
    public class MossGiantPatrolSMB : SceneLinkedSMB<EnemyBehaviour>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float dist =  m_MonoBehaviour.Speed;

            if (m_MonoBehaviour.ObstaclesCheck())
            {
                m_MonoBehaviour.SetHorizontalSpeed(-dist);

            }
            else
            {
                m_MonoBehaviour.SetHorizontalSpeed(dist);
            }
        }
    }

}