using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class AnimationTrie
    {
        private readonly TrieNode root;

        public AnimationTrie()
        {
            root = new TrieNode();
        }

        public void Insert(AnimationType[] combo)
        {
            TrieNode node = root;
            foreach (AnimationType c in combo)
            {
                if (!node.Children.ContainsKey(c))
                {
                    node.Children[c] = new TrieNode();
                }
                node = node.Children[c];
            }
            node.IsEndOfCombo = true;
        }

        public bool Search(AnimationType[] word)
        {
            TrieNode node = root;
            foreach (AnimationType c in word)
            {
                if (!node.Children.ContainsKey(c))
                {
                    return false;
                }
                node = node.Children[c];
            }
            return node.IsEndOfCombo;
        }
    }
}
