using System;
using System.Collections.Generic;
namespace PathFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph map = new Graph("D:\\Users\\USER\\Documents\\OneDrive - Institut Teknologi Bandung\\Kuliah\\Semester 4\\IF2211 - Strategi Algoritma\\Tugas\\Tucil 3\\Path-Finder\\test\\test.txt");
            map.PrintNodes();
            Console.WriteLine();

            string start = "jakarta";
            string destination = "malang";

            List<Node> path = PathFinder.FindShortestPath(start, destination, map);

            PathFinder.PrintPath(path);
        }
    }
}
