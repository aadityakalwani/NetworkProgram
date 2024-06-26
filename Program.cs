﻿using System.Diagnostics;

namespace NetworkProgram;

/*
 * current issues:
 * - the program is not able to handle disconnected graphs
 * - the program is not able to handle graphs with negative weights
 * 
 */

internal abstract class Program
{
    public static void Main()
    {
        Console.WriteLine("Enter the number of nodes exist in the graph:\n");
        int numberOfNodes = GetValidInt(2, 65000);
        int[,] adjacencyMatrix = new int[numberOfNodes, numberOfNodes];
        
        Console.WriteLine("Because this is a simple program, i'll randomly assign the distances of the edges between the nodes");
        Matrix.AssignRandomDistances(adjacencyMatrix, numberOfNodes);
        
        Console.WriteLine("We have created your adjacency matrix. Enter \"y\" to see it:\n");
        if (Console.ReadLine().ToLower() == "y")
        {
            Matrix.DisplayMatrix(adjacencyMatrix);
        }
        
        int userMenuChoice = Menu();
        switch (userMenuChoice)
        {
            case 1:
                Algorithms.ApplyDijkstra(adjacencyMatrix, numberOfNodes);
                break;
            case 2:
                Algorithms.ApplyPrims(adjacencyMatrix);
                break;
            case 3:
                Algorithms.ApplyKruskal(adjacencyMatrix);
                break;
        }
        
    }

    private static int Menu()
    {
        
        Console.WriteLine("\nChoose an algorithm to apply:\n1 - Dijkstra's\n2 - Prim's\n3 - Kruskal's\nEnter a number:");
        
        return GetValidInt(1, 3);
    }
    
    private static int GetValidInt(int min, int max)
    {
        if (int.TryParse(Console.ReadLine(), out int validInt))
        {
            if (validInt >= min && validInt <= max)
            {
                return validInt;
            }
            Console.WriteLine($"No bro enter a number between {min} and {max}");
            return GetValidInt(min, max);
        }
        Console.WriteLine("Please enter a valid integer");
        return GetValidInt(min, max);
    }
}

// SP and MST related methods
static class Algorithms
{
    public static void ApplyDijkstra(int[,] adjacencyMatrix, int numberOfNodes)
    {
        /*
         * algorithm steps:
         *
         * create an empty list of permanent vertices
         * create a list of non-permanent vertices
         * starting at vertex 0 (A)
         *
         * current vertex = starting vertex
         *
         * while the list of non-permanent vertices is not empty
         *     for every vertex in the network
         *         if it is connected to the current vertex
         *            set the temporary distance label of the vertex to the distance between it and the current vertex
         *     then find the vertex with the smallest temporary distance label
         *     make it permanent and store it in the permanent vertices list, remove it from the non-permanent list
         *     set the current vertex to the newly added permanent vertex
         *     [repeat]
         */
        
        // initialize starting vertex
        Console.WriteLine("Enter the vertex you want to start at (eg. A3, B9...):\n");
        string userVertex = Console.ReadLine().ToUpper();
        int startVertex = Matrix.GetIndex(userVertex);
        Console.WriteLine($"You have chosen to start at a vertex of index position {startVertex}");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Thread.Sleep(69);
        
        // initialize lists
        List<Vertex> PermanentVerticesList = new List<Vertex>();
        List<Vertex> NonPermanentVerticesList = new List<Vertex>();
        for (int i = 0; i < numberOfNodes; i++)
        {
            NonPermanentVerticesList.Add(new Vertex(i, i));
        }
        
        NonPermanentVerticesList[startVertex].TemporaryDistanceLabel = 0;

        while (PermanentVerticesList.Count < numberOfNodes)
        {
            // find the vertex with the smallest temporary distance label
            
            /*
             int smallestValue = int.MaxValue; // set this as a huge number to start with
             int smallestVertex = -1; // initialize with an invalid index to just let shit work later
               
             * for (int i = 0; i < NonPermanentVerticesList.Count; i++) // for every item in the nonPermanentVerticesLife
               {
                   Vertex vertex = NonPermanentVerticesList[i];
                   if (vertex.TemporaryDistanceLabel < smallestValue)
                   {
                       smallestValue = vertex.TemporaryDistanceLabel;
                       smallestVertex = vertex.VertexNumber;
                   }
               }
             */

            Vertex smallestVertex = null; // I don't like this being null but perhaps this is what we must do
            int smallestValue = int.MaxValue;
            
            
            foreach (Vertex vertex in NonPermanentVerticesList)
            {
                if (vertex.TemporaryDistanceLabel < smallestValue)
                {
                    smallestValue = vertex.TemporaryDistanceLabel;
                    smallestVertex = vertex;
                }
            }
            // smallestVertex is now the vertex with the smallest temporary distance label
            
            // make the smallest vertex permanent by removing it from the non-permanent list and adding it to the permanent list
            NonPermanentVerticesList.Remove(smallestVertex);
            PermanentVerticesList.Add(smallestVertex);
            smallestVertex.PermanentDistanceLabel = smallestVertex.TemporaryDistanceLabel;

            // updating the temporary distance labels of the non-permanent vertices to see if a shorter route/arc has now been found
            foreach (Vertex vertex in NonPermanentVerticesList)
            {
                if (adjacencyMatrix[smallestVertex.VertexNumber, vertex.VertexNumber] != 0)
                {
                    int newDistance = smallestVertex.TemporaryDistanceLabel + adjacencyMatrix[smallestVertex.VertexNumber, vertex.VertexNumber];
                    if (newDistance < vertex.TemporaryDistanceLabel)
                    {
                        vertex.TemporaryDistanceLabel = newDistance;
                        vertex.Predecessor = smallestVertex.VertexNumber;
                    }
                }
            }
        }
        // after this while loop, all vertices have been made permanent
        
        for (int i = 1; i < numberOfNodes; i++) // for every vertex in the network
        {
            List<int> path = new(); // the path list stores the vertexNumber of the vertices in the shortest path
            int currentVertexIndex = i;
            while (currentVertexIndex != 0) // trace back until reaching the start vertex
            {
                path.Add(currentVertexIndex);
                currentVertexIndex = PermanentVerticesList[currentVertexIndex].Predecessor;
            }
            path.Add(0); // add start vertex

            // Print the shortest path
            Console.Write($"Shortest path from {userVertex} to {Matrix.GetAlphabet(i)}:");

            int totalLength = 0;
            for (int j = path.Count - 1; j >= 0; j--)
            {
                Console.Write($" -> {Matrix.GetAlphabet(path[j])} ({PermanentVerticesList[path[j]].PermanentDistanceLabel})");
                totalLength += PermanentVerticesList[path[j]].TemporaryDistanceLabel;

            }
            Console.Write($"    | Total length of {totalLength}");
            Console.WriteLine();
        }
        
        stopwatch.Stop();
        Console.WriteLine($"Applying Dijkstra's took {stopwatch.ElapsedMilliseconds}ms for this matrix of {numberOfNodes} nodes.");
        Console.WriteLine("This includes a 69 millisecond sleep for dramatic effect.");
        
    }
    
    public static void ApplyPrims(int[,] adjacencyMatrix)
    {
        Console.Clear();
        Console.WriteLine("\nWe are applying Prim's from a matrix; let us cook");
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        int numberOfNodes = adjacencyMatrix.GetLength(0);
        
        stopwatch.Stop();
        Console.WriteLine($"Applying Prim's took {stopwatch.ElapsedMilliseconds}ms for this matrix of {numberOfNodes} nodes.");
    }
    
    public static void ApplyKruskal(int[,] adjacencyMatrix)
    {
        Console.Clear();
        Console.WriteLine("\nWe are applying Kruskal's from a matrix; let us cook");
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        int numberOfNodes = adjacencyMatrix.GetLength(0);
        
        stopwatch.Stop();
        Console.WriteLine($"Applying Kruskal's took {stopwatch.ElapsedMilliseconds}ms for this matrix of {numberOfNodes} nodes.");
    }
}

// Vertex-related methods
class Vertex
{
    public int VertexNumber;
    public int StopNumber;
    public int PermanentDistanceLabel;
    public int TemporaryDistanceLabel;
    public int Predecessor;
    
    public Vertex(int stopNumber, int vertexNumber)
    {
        VertexNumber = vertexNumber;
        StopNumber = stopNumber;
        PermanentDistanceLabel = int.MaxValue;
        Predecessor = -1;
        TemporaryDistanceLabel = int.MaxValue;
    }
}

// Matrix-related methods
static class Matrix
{
    public static void AssignRandomDistances(int[,] adjacencyMatrix, int numberOfNodes)
    {
        Random random = new Random();
        
        for (int i = 0; i < numberOfNodes; i++)
        {
            for (int j = 0; j < numberOfNodes; j++)
            {
                if (i == j) // leading diagonal of 0s
                {
                    adjacencyMatrix[i, j] = 0;
                }
                else if (i < j) // above the diagonal
                {
                    adjacencyMatrix[i, j] = random.Next(0, 9);
                }
                else // below the diagonal is the mirror of the above
                {
                    adjacencyMatrix[i, j] = adjacencyMatrix[j, i];
                }
            }
        }
    }
    
    public static void DisplayMatrix(int[,] matrix)
    {
        Console.Write("  ");
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.Write(" " + GetAlphabet(i) + "");
        }
        Console.WriteLine();

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.Write(GetAlphabet(i) + "| ");
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    
    public static string GetAlphabet(int index)
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        string returnValue = "";
        
        // find the position of the letter in the alphabet
        int remainder = index % 26;
        
        // find the number of times the index is greater than 26
        int quotient = index / 26;
        
        returnValue = returnValue + alphabet[remainder] + (quotient + 1);

        return returnValue;
    }

    public static int GetIndex(string userVertex)
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        string letterString = userVertex[0].ToString();
        string numberString = userVertex[1].ToString();
        
        int letter = alphabet.IndexOf(letterString);
        int number = int.Parse(numberString) - 1;
        
        int index = number * 26 + letter;
        return index;
    }
}
