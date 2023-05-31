using System.Numerics;
using Raylib_cs;

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

                // Random particle colors
                // Calculate the hue value based on the value of `i`
                float hue = i % 360;

                // Convert HSB to RGB
                Raylib_cs.Color particleColor = Raylib.ColorFromHSV(hue, 1f, 1f);

                // Assign the particle color
                obj.particleColor = particleColor;

                // Add the object to the object manager
                manager.AddVerletObject(obj);
            }
        }

        public void SpewObjects(float count)
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

                // Random particle colors
                // Calculate the hue value based on the value of `i`
                float hue = i % 360;

                // Convert HSB to RGB
                Raylib_cs.Color particleColor = Raylib.ColorFromHSV(hue, 1f, 1f);

                // Assign the particle color
                obj.particleColor = particleColor;

                // Add the object to the object manager
                manager.AddVerletObject(obj);
            }
        }

        public void Update(float dt)
        {
            int subSteps = settings.PhysicsSubsteps;
            float subDt = dt / (float)subSteps;

            // Sub-stepping the simulation to allow more accurate collisions
            for (int i = subSteps; i != 0; i--)
            {
                ApplyGravity();
                ApplyConstraints();
                SolveCollisions();
                UpdatePositions(subDt);
            }
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

        public void ApplyConstraints()
        {
            int windowWidth = settings.WindowWidth;
            int windowHeight = settings.WindowHeight;
            float particleSize = settings.ParticleSize;

            foreach (var obj in manager.verletObjects)
            {
                // Clamp X coordinate within the window bounds
                if (obj.currentPosition.X < particleSize)
                    obj.currentPosition.X = particleSize;
                else if (obj.currentPosition.X > windowWidth - particleSize)
                    obj.currentPosition.X = windowWidth - particleSize;

                // Clamp Y coordinate within the window bounds
                if (obj.currentPosition.Y < particleSize)
                    obj.currentPosition.Y = particleSize;
                else if (obj.currentPosition.Y > windowHeight - particleSize)
                    obj.currentPosition.Y = windowHeight - particleSize;
            }
        }

        public void SolveCollisions()
        {
            int objectCount = manager.verletObjects.Count;
            VerletObject[] objectContainer = manager.verletObjects.ToArray();

            for (int i = 0; i < objectCount; i++)
            {
                VerletObject obj1 = objectContainer[i];
                for (int j = i+1; j < objectCount; j++)
                {
                    VerletObject obj2 = objectContainer[j];
                    Vector2 collisionAxis = obj1.currentPosition - obj2.currentPosition;
                    float dist = collisionAxis.Length();

                    if (dist < settings.ParticleSize*2)
                    {
                        Vector2 n = collisionAxis / dist;
                        float delta = settings.ParticleSize*2 - dist;
                        obj1.currentPosition += 0.5f * delta * n;
                        obj2.currentPosition -= 0.5f * delta * n;
                    }
                }
            }
        }

    }
}
