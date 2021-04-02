using System;

namespace PathFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = Graph.CreateGraph("D:\\Users\\USER\\Documents\\OneDrive - Institut Teknologi Bandung\\Kuliah\\Semester 4\\IF2211 - Strategi Algoritma\\Tugas\\Tucil 3\\Path-Finder\\test\\test.txt");
            graph.PrintNodes();
        }
    }
}
