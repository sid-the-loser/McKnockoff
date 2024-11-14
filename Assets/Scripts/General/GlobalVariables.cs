using UnityEngine;

namespace General
{
    public static class GlobalVariables
    {
        public static bool GamePaused = false;

        public static bool FastDistanceCheck(Vector3 v1, Vector3 v2, float distance)
        {
            return Mathf.Pow(distance, 2) >=
                   Mathf.Pow(v1.x - v2.x, 2) +
                   Mathf.Pow(v1.y - v2.y, 2) +
                   Mathf.Pow(v1.z - v2.z, 2);
        }
    }
}
