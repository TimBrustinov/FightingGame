using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class TrieNode
    {
        public Dictionary<AnimationType, TrieNode> Children { get; private set; }
        public bool IsEndOfCombo { get; set; }

        public TrieNode()
        {
            Children = new Dictionary<AnimationType, TrieNode>();
            IsEndOfCombo = false;
        }
    }
}
