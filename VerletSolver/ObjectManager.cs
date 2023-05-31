using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerletSolver
{
    public class ObjectManager
    {
        public List<VerletObject> verletObjects;

        public ObjectManager()
        {
            verletObjects = new List<VerletObject>();
        }

        public void AddVerletObject(VerletObject obj)
        {
            verletObjects.Add(obj);
        }
    }
}
