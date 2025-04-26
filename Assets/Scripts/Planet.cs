using UnityEngine;

namespace JasurSadikov.StarSystem
{
    public class Planet : Body
    {
        const float lineWidth = 0.05f;

        [SerializeField] LineRenderer lineRenderer;

        float distance;
        float orbitSpinSpeed;
        Body host;

        public void Initialize(Body host, float distance, float diameter, float localSpinSpeed, float orbitSpinSpeed)
        {
            this.host = host;
            this.distance = distance;
            this.orbitSpinSpeed = orbitSpinSpeed;

            SpinSpeed = localSpinSpeed;
            Diameter = diameter;

            Initialize();

            DrawLineRenderer();
        }

        protected override void Update()
        {
            base.Update();

            if (!host)
                return;

            transform.RotateAround(host.transform.position, Vector3.up, orbitSpinSpeed * Time.deltaTime);
        }

        void DrawLineRenderer()
        {
            int segments = Mathf.CeilToInt(distance * 16);

            lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = segments;
            lineRenderer.loop = true;

            for (int i = 0; i < segments; i++)
            {
                float angle = 2f * Mathf.PI * i / segments;
                float x = Mathf.Cos(angle) * distance;
                float z = Mathf.Sin(angle) * distance;

                Vector3 offset = host.transform.right * x
                                 + host.transform.forward * z;
                lineRenderer.SetPosition(i, host.transform.position + offset);
            }
        }
    }
}