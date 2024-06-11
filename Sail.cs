using System;
using System.Numerics;

namespace SillyBilly.SillyBilly
{
    /*
        The following class handles math and operations relating to sails
    */
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

        /*
            The following method will calculate an integer thrust value dependent on the difference in angle between
            2 vectors. While the vectors are 3D, they'll functionally be 2D.

            @pre Vector must be a valid vector with valid float datatypes
            @post theta = arccos([windDirection * sailDirection]/[||windDirection||sailDirection||]) * 180/pi
                  windScale * cos(theta * pi/180)
            @return [uint >= 0 <= MAX_INT]
        */
        public uint CalculateWindSailThrust(Vector3 windDirection, Vector3 sailDirection)
        {
            float dotProduct = Vector3.Dot(windDirection, sailDirection);
            float angle = Math.Acos(dotProduct / (windDirection.Length() * sailDirection.Length())) * (180 / Math.PI);
            return (int)(windScale * Math.Cos(angle * (Math.PI / 180)));
        }

        /*
            The following method will create a new random wind vector

            @pre n/a
            @post float rotational Vector3
            @return n/a
        */
        public void UpdateWindDirection()
        {
            float x = (float)(rand.NextDouble() * 2 - 1);
            float y = (float)(rand.NextDouble() * 2 - 1);
            //float z = (float)(rand.NextDouble() * 2 - 1);
            float z = 0;

            windDirection = Vector3.Normalize(new Vector3(x, y, z));
            lastWindUpdate = DateTime.Now;
        }

        /*
            The following method will update the wind direction every 5-10 minutes.

            @pre n/a
            @post new windDirection
            @return n/a
        */
        public void CheckAndUpdateWindDirection()
        {
            TimeSpan elapsed = DateTime.Now - lastWindUpdate;

            if (elapsed.TotalMinutes >= 5 && elapsed.TotalMinutes <= 10)
            {
                UpdateWindDirection();
            }
        }

        /*
            Getter for Wind Direction...

            @pre n/a
            @post checks current windDirection
            @return Wind Direction 
        */
        public Vector3 GetWindDirection()
        {
            CheckAndUpdateWindDirection();
            return windDirection;
        }
        
        /*
            The following calculates the sail area of a sail using the "Arrestor Hook" method

            @pre Unsure
            @post Calculates the area of a 2D Sail
            @return Wind Direction 
        */
        /*
        public float calculateSailArea(//Something){

        }
        */
    }
}
