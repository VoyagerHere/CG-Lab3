using OpenTK.Mathematics;
using System;

namespace LearnOpenTK.Common
{
    public class Camera
    {
        public static Vector3 Position = new Vector3(0.0f, 0.0f, -7f);
        public static Vector3 View = new Vector3(0.0f, 0.0f, 1.0f);
        public static Vector3 Up = new Vector3(0.0f, 1.0f, 0.0f);
        public static Vector3 Side = new Vector3(1.0f, 0.0f, 0.0f);
        public static Vector2 Scale = new Vector2(1.0f);
    }
}