using System.Diagnostics;

namespace NetworkProgram;

internal abstract class Program
{
    public static void Main()
    {
        Console.WriteLine("How many nodes exist in the graph?\nPlease use a number 2 - 60 otherwise idk what to call the vertices:");
        int numberOfNodes = GetValidInt(2, 9999);
        int[,] adjacencyMatrix = new int[numberOfNodes, numberOfNodes];
        
        Console.WriteLine("Because this is a simple program, i'll randomly assign the distances of the edges between the nodes");
        Matrix.AssignRandomDistances(adjacencyMatrix, numberOfNodes);
        
        Console.WriteLine("After assigning these distances, here is the current adjacency matrix:\n");
        Matrix.DisplayMatrix(adjacencyMatrix);
        
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
        
        Console.WriteLine("\nWe are applying Dijkstra's from a matrix; let us cook");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Thread.Sleep(69);

        
        
        List<Vertex> PermanentVerticesList = new List<Vertex>();
        List<Vertex> NonPermanentVerticesList = new List<Vertex>();
        for (int i = 0; i < numberOfNodes; i++)
        {
            NonPermanentVerticesList.Add(new Vertex(i, i, int.MaxValue));
        }

        // initialize starting vertex (vertex 0/A)
        NonPermanentVerticesList[0].TemporaryDistanceLabel = 0;

        while (PermanentVerticesList.Count < numberOfNodes)
        {
            // find the vertex with the smallest temporary distance label
            int smallestValue = int.MaxValue;
            int smallestVertex = -1; // initialize with an invalid index to just let shit work later
            foreach (Vertex vertex in NonPermanentVerticesList)
            {
                if (!vertex.PermanentlyAdded && vertex.TemporaryDistanceLabel < smallestValue)
                {
                    smallestValue = vertex.TemporaryDistanceLabel;
                    smallestVertex = vertex.VertexNumber;
                }
            }

            if (smallestVertex == -1)
            {
                // Graph is disconnected or all vertices are permanently added
                Console.WriteLine("Graph is disconnected or all vertices are permanently added");
                break;
            }

            // Make the smallest vertex permanent
            Vertex currentVertex = NonPermanentVerticesList[smallestVertex];
            currentVertex.PermanentlyAdded = true;
            PermanentVerticesList.Add(currentVertex);

            // updating distances if a shorter path is found
            for (int i = 0; i < numberOfNodes; i++)
            {
                if (adjacencyMatrix[currentVertex.VertexNumber, i] != 0)
                {
                    int newDistance = currentVertex.TemporaryDistanceLabel + adjacencyMatrix[currentVertex.VertexNumber, i];
                    if (newDistance < NonPermanentVerticesList[i].TemporaryDistanceLabel)
                    {
                        NonPermanentVerticesList[i].TemporaryDistanceLabel = newDistance;
                        NonPermanentVerticesList[i].Predecessor = currentVertex.VertexNumber; // adding in predecessor to backtrack and store route
                    }
                }
            }
        }
        
        for (int i = 1; i < numberOfNodes; i++)
        {
            List<int> path = new List<int>();
            int currentVertexIndex = i;
            while (currentVertexIndex != 0) // Trace back until reaching the start vertex
            {
                path.Add(currentVertexIndex);
                currentVertexIndex = NonPermanentVerticesList[currentVertexIndex].Predecessor;
            }
            path.Add(0); // Add start vertex

            // Print the shortest path
            Console.Write($"Shortest path from A to {Matrix.GetAlphabet(i)}:");

            int totalLength = 0;
            for (int j = path.Count - 1; j >= 0; j--)
            {
                Console.Write($" -> {Matrix.GetAlphabet(path[j])} ({NonPermanentVerticesList[path[j]].TemporaryDistanceLabel})");
                totalLength += NonPermanentVerticesList[path[j]].TemporaryDistanceLabel;

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
    public bool PermanentlyAdded;
    public int Predecessor;
    
    public Vertex(int stopNumber, int vertexNumber,  int temporaryDistanceLabel)
    {
        VertexNumber = vertexNumber;
        StopNumber = stopNumber;
        PermanentDistanceLabel = 999;
        Predecessor = -1;
        TemporaryDistanceLabel = temporaryDistanceLabel;
        PermanentlyAdded = false;
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
        
        // a way to get the alphabet value in terms of "A1, B1, C2, etc. and then go Z1, A2, B2, C2 ... Z2, A3, etc"
        // loop through so long as the desired index is more than i * 26
        // then index % 26 to get the correct letter
        // add the letter to the return value
        // add i to the return value

        
        // find the position of the letter in the alphabet
        int remainder = index % 26;
        
        // find the number of times the index is greater than 26
        int quotient = index / 26;
        
        returnValue += alphabet[remainder];
        returnValue += quotient + 1;
        

        return returnValue;
    }
}
