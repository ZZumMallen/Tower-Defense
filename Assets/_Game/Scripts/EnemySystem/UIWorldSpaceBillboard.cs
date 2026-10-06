
using UnityEngine;


namespace Partisan
{

    public class EnemyHealhBar : MonoBehaviour
    {

        private Camera m_camera;
        private Quaternion m_newRotation;

        private void Start()
        {
            m_camera = Camera.main;
        }

        private void LateUpdate()
        {
            var dir = m_camera.transform.position - transform.forward;
            m_newRotation = Quaternion.LookRotation(dir);
            transform.rotation = m_newRotation;
        }
    }
}
