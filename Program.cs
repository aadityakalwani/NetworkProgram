using System.Diagnostics;

namespace NetworkProgram;

class Program
{
    public static void Main()
    {
        Console.WriteLine("How many nodes exist in the graph?\nPlease use a number below 10:");
        int numberOfNodes = GetValidInt(1, 10);
        int[,] adjacencyMatrix = new int[numberOfNodes, numberOfNodes];
        
        Console.WriteLine("Because this is a simple program, i'll randomly assign the distances of the edges between the nodes");
        Matrix.AssignRandomDistances(adjacencyMatrix, numberOfNodes);
        
        Console.WriteLine("After assigning these distances, here is the current adjacency matrix:\n");
        Matrix.DisplayMatrix(adjacencyMatrix);
        
        int userMenuChoice = Menu();
        switch (userMenuChoice)
        {
            case 1:
                Algorithms.ApplyDijkstra(adjacencyMatrix);
                break;
            case 2:
                Algorithms.ApplyPrims(adjacencyMatrix);
                break;
            case 3:
                Algorithms.ApplyKruskal(adjacencyMatrix);
                break;
        }
        
    }

    public static int Menu()
    {
        
        Console.WriteLine("\nChoose an algorithm to apply:\n1 - Dijkstra's\n2 - Prim's\n3 - Kruskal's\nEnter a number:");
        
        return GetValidInt(1, 3);
    }
    
    public static int GetValidInt(int min, int max)
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
class Algorithms
{
    public static void ApplyDijkstra(int[,] adjacencyMatrix)
    {
        Console.WriteLine("\nWe are applying Dijkstra's from a matrix; let us cook");
    }
    
    public static void ApplyKruskal(int[,] adjacencyMatrix)
    {
        Console.WriteLine("\nWe are applying Kruskal's from a matrix; let us cook");
    }
    
    public static void ApplyPrims(int[,] adjacencyMatrix)
    {
        Console.WriteLine("\nWe are applying Prim's from a matrix; let us cook");
    }
    
}

// Matrix-related methods
class Matrix
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
                    adjacencyMatrix[i, j] = random.Next(1, 10);
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
        string vertices = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Console.Write("  ");
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.Write(" " + vertices[i] + "");
        }
        Console.WriteLine();

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            Console.Write(vertices[i] + "| ");
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
