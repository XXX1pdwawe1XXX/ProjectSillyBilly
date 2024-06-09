using System;
using System.Numerics;

namespace SillyBilly.SillyBilly
{
    class Sail
    {
        private bool headWind = false;
        private float windScale = 3.0f;
        private float sailDegree = 0;
        private float windDegree = 0;
        private float windSailThrust = 0;
        private Vector3 windDirection = new Vector3(0, 0, 0);
        private Vector3 sailDirection = new Vector3(0, 0, 0);
        private DateTime lastWindUpdate;
        private static Random rand = new Random();

        public Sail()
        {
            UpdateWindDirection();
        }

        public int CalculateWindSailThrust(Vector3 windDirection, Vector3 sailDirection)
        {
            float dotProduct = Vector3.Dot(windDirection, sailDirection);
            float angle = Math.Acos(dotProduct / (windDirection.Length() * sailDirection.Length())) * (180 / Math.PI);
            return (int)(windScale * Math.Cos(angle * (Math.PI / 180)));
        }

        public void UpdateWindDirection()
        {
            float x = (float)(rand.NextDouble() * 2 - 1);
            float y = (float)(rand.NextDouble() * 2 - 1);
            float z = (float)(rand.NextDouble() * 2 - 1);

            windDirection = Vector3.Normalize(new Vector3(x, y, z));
            lastWindUpdate = DateTime.Now;
        }

        public void CheckAndUpdateWindDirection()
        {
            TimeSpan elapsed = DateTime.Now - lastWindUpdate;

            if (elapsed.TotalMinutes >= 5 && elapsed.TotalMinutes <= 10)
            {
                UpdateWindDirection();
            }
        }

        public Vector3 GetWindDirection()
        {
            CheckAndUpdateWindDirection();
            return windDirection;
        }
    }
}
