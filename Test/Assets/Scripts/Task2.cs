using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Task2 : MonoBehaviour
    {
        [SerializeField]
        float force;
        [SerializeField]
        float mass = 0.5f;
        [SerializeField]
        float distance;
        float acceleration;
        float velocity;
        [SerializeField]
        float duration;
        private void OnValidate()
        {
            if (duration <= 0)
            {
                duration = 1f;
            }
            if (distance < 0)
            {
                distance = 0f;
            }
        }
        void Start()
        {
            Debug.Log(CalculateStartingVelocity(distance, duration)+"m/s");
        }

        public float CalculateStartingVelocity(float distance, float duration)
        {

            acceleration = force / mass;
            velocity = (distance-((acceleration*duration*duration)/2f))/ duration;
            return velocity;
        }
    }
}
