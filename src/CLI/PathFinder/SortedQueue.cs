﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder
{
    class SortedQueue
    {
        private List<Node> queue;

        public SortedQueue()
        {
            queue = new List<Node>();
        }
        public void Enqueue(Node node)
        {
            int i = 0;

            while (i < queue.Count && queue[i].GetEstimatedDistance() < node.GetEstimatedDistance())
            {
                i++;
            }

            queue.Insert(i, node);
        }

        public Node Dequeue()
        {
            if (queue.Count == 0) return null;
            Node first = queue[0];
            queue.RemoveAt(0);
            return first;
        }

        public bool IsEmpty()
        {
            return queue.Count == 0;
        }

        public List<Node> GetQueue()
        {
            return queue;
        }
    }
}
