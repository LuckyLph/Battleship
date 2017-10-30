using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Battleship
{
  public class Endscreen
  {
    RenderWindow window;                                                  //Objet de type RenderWindow pour la fenêtre de rendu


    public Endscreen()
    {

    }

    public void InitializeEndscreen(Resources resources, RenderWindow window)
    {
      this.window = window;
    }

    /// <summary>
    /// Met à jour l'écran de fin de partie
    /// </summary>
    public void Update(bool hasWon)
    {

    }
  }
}
