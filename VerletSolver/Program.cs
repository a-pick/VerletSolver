using Raylib_cs;

struct ProgramSettings {
    public int WindowWidth;
    public int WindowHeight;
    public string WindowTitle;
    public int ScaleFactor;
    public Color ParticleColor;
    public int ParticleSize;
    public Color BackgroundColor;
    public int PhysicsSubsteps;
}

namespace VerletSolver
{

    static class Program
    {
        static (int, int) GetScaledCoordinates(int originalX, int originalY, ProgramSettings settings)
        {
            int scaledX = (int)(originalX * settings.ScaleFactor);
            int scaledY = (int)(originalY * settings.ScaleFactor);

            return (scaledX, scaledY);
        }

        public static void Main()
        {
            // Initialize the program settings
            ProgramSettings settings;
            settings.WindowWidth = 1000;
            settings.WindowHeight = 1000;
            settings.WindowTitle = "Verlet Solver";
            settings.ScaleFactor = 1;
            settings.ParticleColor = Color.RAYWHITE;
            settings.ParticleSize = 5;
            settings.BackgroundColor = Color.DARKGRAY;
            settings.PhysicsSubsteps = 4;

            // Create the object manager
            ObjectManager manager = new();

            // Initialize the solver
            Solver solver = new(settings, manager);

            // Spawn some objects
            solver.SpewObjects(1000);

            Raylib.InitWindow((int)(settings.WindowWidth * settings.ScaleFactor), (int)(settings.WindowHeight * settings.ScaleFactor), settings.WindowTitle);
            Raylib.SetTargetFPS(144);

            // The game loop
            while (true)
            {
                // Set the time delta
                float dt = Raylib.GetFrameTime();
                solver.Update(dt);

                Raylib.BeginDrawing();
                Raylib.ClearBackground(settings.BackgroundColor);

                // Draw the particles
                foreach (VerletObject obj in manager.verletObjects)
                {
                    if (obj != null)
                    {
                        (int sX, int sY) = GetScaledCoordinates((int)obj.currentPosition.X, (int)obj.currentPosition.Y, settings);
                        Raylib.DrawCircle(sX, sY, settings.ParticleSize*settings.ScaleFactor, obj.particleColor);
                    }
                }

                Raylib.EndDrawing();

                // If the user attempts to close the window, close it
                if (Raylib.WindowShouldClose())
                    break;
            }

            Raylib.CloseWindow();
        }
    }
}