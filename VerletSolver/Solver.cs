using System.Numerics;

namespace VerletSolver
{
    internal class Solver
    {
        Vector2 gravity = new(0.0f, 1000.0f);
        ObjectManager manager;
        ProgramSettings settings;

        public Solver(ProgramSettings settings, ObjectManager manager)
        {
            this.settings = settings;
            this.manager = manager;
        }

        public void SpawnObjects(float count)
        {
            // Create the randomizer
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                VerletObject obj = new();

                // Generate random coords for the curPosition of each object
                float randX = random.Next(0, settings.WindowWidth);
                float randY = random.Next(0, settings.WindowHeight);

                // Set the initial properties of each object
                obj.currentPosition = new Vector2(randX, randY);
                obj.oldPosition = obj.currentPosition;
                obj.acceleration = Vector2.Zero;

                // Add the object to the object manager
                manager.AddVerletObject(obj);
            }
        }

        public void Update(float dt)
        {
            ApplyGravity();
            UpdatePositions(dt);
        }

        public void ApplyGravity()
        {
            foreach (var obj in manager.verletObjects)
            {
                obj.Accelerate(gravity);
            }
        }

        public void UpdatePositions(float dt) 
        {
            foreach (var obj in manager.verletObjects)
            {
                obj.UpdatePosition(dt);
            }
        }
    }
}
