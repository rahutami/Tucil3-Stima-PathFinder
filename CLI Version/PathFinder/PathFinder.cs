using System;
using System.Collections.Generic;
using System.Text;

namespace PathFinder
{
    class PathFinder
    {
        public static List<Node> FindShortestPath(string start, string destination, Graph map)
        {
            List<Node> path = new List<Node>();
            Node startNode = map.GetNode(start);
            Node destNode = map.GetNode(destination);
            map.EstimateDistanceToDestination(destNode);
            return path;
        }
    }
}
