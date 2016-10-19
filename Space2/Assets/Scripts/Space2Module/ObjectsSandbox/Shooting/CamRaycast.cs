using UnityEngine;

namespace Assets.Scripts.Space2Module.ObjectsSandbox.Shooting
{
    public class CamRaycast : MonoBehaviour
    {
        public float Power = 100f;
        public float Radius = 20f;
        public float FakeUpforce = 3f;

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && Time.timeScale > 0.001f)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 explosionPos = hit.point;
                    Collider[] colliders = Physics.OverlapSphere(explosionPos, Radius);

                    foreach (Collider collided in colliders)
                    {
                        Rigidbody rb = collided.GetComponent<Rigidbody>();

                        if (rb != null)
                            rb.AddExplosionForce(Power, explosionPos, Radius, FakeUpforce);

                    }
                }
            }
        }
    }
}
