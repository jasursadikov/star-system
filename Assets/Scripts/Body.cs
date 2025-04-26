using UnityEngine;

namespace JasurSadikov.StarSystem
{
    public abstract class Body : MonoBehaviour
    {
        [SerializeField, Min(1f)] float diameter = 1f;
        [SerializeField, Min(float.Epsilon)] float spinSpeed = 1f;

        new Transform transform;

        protected float Diameter { get => diameter; set => diameter = Mathf.Max(Mathf.Epsilon, value); }
        protected float SpinSpeed { get => spinSpeed; set => spinSpeed = Mathf.Max(float.Epsilon, value); }

        protected virtual void Awake()
        {
            transform = gameObject.transform;
        }

        protected virtual void OnEnable()
        {
            Initialize();
        }

        protected virtual void Update()
        {
            transform.Rotate(transform.up * spinSpeed * Time.deltaTime);
        }

        protected void Initialize()
        {
            transform.localScale = Vector3.one * diameter;
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            transform = transform ? transform : gameObject.transform;

            OnEnable();
        }
#endif
    }
}