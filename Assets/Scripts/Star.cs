using UnityEngine;

namespace JasurSadikov.StarSystem
{
    public class Star : Body
    {
        [SerializeField] Planet planet;

        [SerializeField] int seed;

        [SerializeField] float starDiameterMin;
        [SerializeField] float starDiameterMax;
        [SerializeField] int planetsCountMin;
        [SerializeField] int planetsCountMax;
        [SerializeField] float planetDiameterMin;
        [SerializeField] float planetDiameterMax;
        [SerializeField] float distanceMin;
        [SerializeField] float distanceMax;

        Planet[] planets;

        void Start()
        {
            Random.InitState(seed);

            int planetsCount = (int)Mathf.Lerp(planetsCountMin, planetsCountMax, Random.value);
            planets = new Planet[planetsCount];

            Diameter = Mathf.Lerp(starDiameterMin, starDiameterMax, Random.value);

            float distance = Diameter + Mathf.Lerp(distanceMin, distanceMax, Random.value);

            for (int index = 0; index < planetsCount; index++)
            {
                float planetRadius = Mathf.Lerp(planetDiameterMin, planetDiameterMax, Random.value);
                planets[index] = Instantiate(planet, transform);

                planets[index].Initialize(this, distance, planetRadius, Mathf.Lerp(-1f, 1f, Random.value), Mathf.Lerp(-1f, 1f, Random.value));
                planets[index].transform.position = transform.position + transform.forward * distance;
                planets[index].transform.RotateAround(transform.position, Vector3.up, Mathf.Lerp(0, 360, Random.value));

                distance += planetRadius * 2f + Mathf.Lerp(distanceMin, distanceMax, Random.value);
            }
        }

        protected override void Update() { }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, starDiameterMin);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, starDiameterMax);
        }
        protected override void OnValidate()
        {
            base.OnValidate();

            starDiameterMin = Mathf.Max(0, starDiameterMin);
            starDiameterMax = Mathf.Min(float.MaxValue, starDiameterMax);

            planetsCountMin = Mathf.Max(0, planetsCountMin);
            planetsCountMax = Mathf.Min(int.MaxValue, planetsCountMax);

            planetDiameterMin = Mathf.Max(0, planetDiameterMin);
            planetDiameterMax = Mathf.Min(float.MaxValue, planetDiameterMax);
        }
#endif
    }
}