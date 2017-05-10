using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 7;
            Prim(n);
            Dijkstra(n,0);

        }
        public static void Prim(int n)
        {
            int[,] matrix = new int[n, n];
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    matrix[i, j] = r.Next(1, 10);
                    matrix[j, i] = matrix[i, j];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            List<List<int[]>> mat = new List<List<int[]>>(n);
            for (int i = 0; i < n; i++)
            {
                mat.Add(new List<int[]>());
            }
            int[] visited = new int[n];
            visited[0] = 1;
            while (true)
            {
                int min = int.MaxValue;
                int point = 0;
                int from = 0;
                for (int i = 0; i < visited.Count(); i++)
                {
                    if (visited[i] == 0) continue;
                    for (int j = 0; j < n; j++)
                    {
                        if (matrix[i, j] != 0 && visited[j] == 0 && matrix[i, j] < min)
                        {
                            from = i;
                            min = matrix[i, j];
                            point = j;
                        }
                    }
                }
                if (min == int.MaxValue) break;
                visited[point] = 1;
                mat[from].Add(new int[2] { point, min });
            }
            int sum = 0;
            for (int j = 0; j < mat.Count; j++)
            {
                Console.Write("From: " + (j + 1) + " ");
                for (int k = 0; k < mat[j].Count; k++)
                {
                    Console.Write("To " + (mat[j][k][0] + 1) + " Weight: " + mat[j][k][1] + " ;");
                    sum += mat[j][k][1];
                }
                Console.WriteLine();
            }
            Console.WriteLine(sum);
        }
        public static void Dijkstra(int n, int vertex)
        {
            int[,] matrix = new int[n, n];
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    matrix[i, j] = r.Next(1, 100);
                    matrix[j, i] = matrix[i, j];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            bool[] visited = new bool[n];
            List<int[]> stack = new List<int[]>();
            int[] len = new int[n];
            for(int i = 0; i < n; i++)
            {
                if(i != vertex) len[i] = int.MaxValue;
            }
            Dij(n, matrix, vertex, ref visited, ref stack, ref len);
            Console.Write("Distance from " + vertex+" : ");
            for (int i = 0; i < n; i++)
            {
                Console.Write(len[i] + " ");
            }
            
        }
        static public void Dij(int n, int[,] matrix, int vertex, ref bool[] visited, ref List<int[]> stack, ref int[] len)
        {
            visited[vertex] = true;
            for (int i = 0; i< n; i++)
            {
                if (visited[i]) continue;
                if (matrix[vertex, i] != 0)
                {
                    if (len[i] > (len[vertex] + matrix[vertex, i]))
                        len[i] = len[vertex] + matrix[vertex, i];
                }
                Insert(i, len[i], ref stack);
            }
            if (stack.Count != 0)
            {
                int call = stack[0][0];
                stack.RemoveAt(0);
                Dij(n, matrix, call, ref visited, ref stack, ref len);
            }
            return;
        }
        static public void Insert(int i, int weight, ref List<int[]> stack)
        {
            if (stack.Count == 0 )
            {
                stack.Add(new int[2] { i, weight });
                return;
            }
            if(weight < stack[0][1])
            {
                stack.Insert(0, new int[2] { i, weight });
            }

            for(int k = 0; k < stack.Count; k++)
            {
                if (weight >= stack[k][1])
                {
                    stack.Insert(k + 1, new int[2] { i, weight });
                    return;
                }
            }
        }
    }
    
}
