using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Project.Game
{

    public class CharactersModel
    {
        public int id;
        public string CurrentTile;
        public List<string> WalkedTiles = new List<string>();
        public List<string> ActionTilesAtk = new List<string>();
        public List<string> ActionTilesDef = new List<string>();

        public int CurrentHP;
        public int CurrentAP;

    }
}
