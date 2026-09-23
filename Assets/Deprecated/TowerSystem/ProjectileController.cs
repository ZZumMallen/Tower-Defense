using UnityEngine;


namespace Partisan
{
/*    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        private Rigidbody _rb;


        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // this rb move requires the object to be intantiated in the correct direction
        private void FixedUpdate()
        {
            _rb.MovePosition(transform.position + transform.forward * (speed * Time.fixedDeltaTime));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log(collision.gameObject.name);
            }
        }
    }*/
}
