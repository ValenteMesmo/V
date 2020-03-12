using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

namespace Skeletor
{  
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new Game1())
            {
                game.Run();
            }
        }
    }
}
