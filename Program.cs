namespace NetworkProgram;

class Program
{

    public static void Main()
    {
        Console.WriteLine("How many nodes exist in the graph?\nPlease use a small number:");
        int numberOfNodes = GetValidInt(1, 9999999);
        int[,] adjacencyMatrix = new int[numberOfNodes, numberOfNodes];
        
        Console.WriteLine("Because this is a simple program, i'll randomly assign the distances of the edges between the nodes");
        
        Random random = new Random();
        for (int i = 0; i < numberOfNodes; i++)
        {
            for (int j = 0; j < numberOfNodes; j++)
            {
                if (i == j)
                {
                    adjacencyMatrix[i, j] = 0;
                }
                else
                {
                    adjacencyMatrix[i, j] = random.Next(1, 10);
                }
            }
        }
        
        Console.WriteLine("After assigning these distances, here is the current adjacency matrix:");
        DisplayMatrix(adjacencyMatrix);
        
        Console.WriteLine("Enter your choice:\n1 - apply Dijkstra\n2 - apply Prim's\n3 - apply Kruskal's\n");
        int userMenuOption = GetValidInt(1, 9999999);

        switch (userMenuOption) 
        {
            case 1:
                ApplyDijkstra(adjacencyMatrix);
            
                break;
            case 2:
                ApplyPrims(adjacencyMatrix);
            
                break;
            case 3:
                ApplyKruskal(adjacencyMatrix);
            
                break;
        }
    }

    public static void ApplyDijkstra(int[,] adjacencyMatrix)
    {
        Console.WriteLine("We are applying Dijkstra's from a matrix; let us cook");
        
    }

    public static void ApplyPrims(int[,] adjacencyMatrix)
    {
        Console.WriteLine("doing Prim's");
    }

    public static void ApplyKruskal(int[,] adjacencyMatrix)
    {
        Console.WriteLine("doing Kruskal's");
        }
    
    public static void DisplayMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
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
