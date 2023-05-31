using Raylib_cs;
using System.Numerics;

namespace VerletSolver
{
    public class VerletObject
    {
        public Vector2 currentPosition;
        public Vector2 oldPosition;
        public Vector2 acceleration;
        public Color particleColor;
        public Vector2 velocity;

        public void UpdatePosition(float dt)
        {
            // Get the velocity vector
            velocity = currentPosition - oldPosition;

            // Save the current position
            oldPosition = currentPosition;

            // Perform the Verlet integration
            currentPosition = currentPosition + velocity + acceleration * dt * dt;

            // Reset acceleration
            acceleration = Vector2.Zero;
        }

        public void Accelerate(Vector2 acc)
        {
            acceleration += acc;
        }
    }
}
