using System.Collections.Generic;
using System.Text;
using System;
using static KTANE_helper.IOHandler;

namespace KTANE_helper.Solvers
{
    internal class MazeSolver : Solvable<MazeSolver>
    {
        internal override void Solve(BombKnowledge bk)
        {
            // Find out which maze
            var maze = Maze.GetMaze(CoordinateQuery("What are the coordinates of a circle (these are 1 based).", Maze.MinimalValue, Maze.MaximalValue));

            // Find out start and target
            var startPosition = Maze.LinearCoordinate(CoordinateQuery("Where is the square?", Maze.MinimalValue, Maze.MaximalValue));
            var targetPosition = Maze.LinearCoordinate(CoordinateQuery("Where is the triangle?", Maze.MinimalValue, Maze.MaximalValue));

            // Calculate and print path
            Show("The solution to the maze is: " + BFS(maze, startPosition, targetPosition));
        }

        private string BFS(Maze maze, int startPosition, int targetPosition)
        {
            if (startPosition == targetPosition)
            {
                return $"Start position {startPosition} is the target.";
            }

            Queue<int> queue = new Queue<int>();
            HashSet<int> visited = new HashSet<int>();
            Dictionary<int, int?> cameFrom = new Dictionary<int, int?>();

            // Initialize BFS
            queue.Enqueue(startPosition);
            visited.Add(startPosition);
            cameFrom[startPosition] = null;

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                // Process neighbors
                foreach (int neighbor in maze.GetNeighbours(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                        cameFrom[neighbor] = current;

                        if (neighbor == targetPosition)
                        {
                            return ReconstructPath(cameFrom, startPosition, targetPosition);
                        }
                    }
                }
            }

            return "No path found.";
        }

        private string ReconstructPath(Dictionary<int, int?> cameFrom, int startPosition, int targetPosition)
        {
            List<int> path = new List<int>();
            int? current = targetPosition;

            while (current != null)
            {
                path.Add(current.Value);
                current = cameFrom[current.Value];
            }

            path.Reverse();
            var stringBuilder = new StringBuilder();

            for (int i = 1; i < path.Count; ++i)
            {
                stringBuilder.Append((path[i] - path[i - 1]) switch
                {
                    -1 => "Left",
                    1 => "Right",
                    -Maze.Width => "Up",
                    Maze.Width => "Down",
                    _ => "???",
                } + " ");
            }

            return stringBuilder.ToString();
        }
    }

    public class Maze
    {
        // Maze coordinates are 1-based

        private IEnumerable<int>[] _neighbours;
        private Maze(IEnumerable<int>[] neighbours) => this._neighbours = neighbours;

        public IEnumerable<int> GetNeighbours(int x, int y) => GetNeighbours(LinearCoordinate(x, y));
        public IEnumerable<int> GetNeighbours(int linearCoordinate) => this._neighbours[linearCoordinate];

        internal static int LinearCoordinate((int, int) coordinates) => LinearCoordinate(coordinates.Item1, coordinates.Item2);
        internal static int LinearCoordinate(int x, int y) => (y - 1) * Height + (x - 1);

        internal static Maze GetMaze((int, int) coordinates) => GetMaze(coordinates.Item1, coordinates.Item2);
        internal static Maze GetMaze(int x, int y) => AllMazes[
            (x, y) switch
            {
                (1, 2) or (6, 3) => 0,
                (2, 4) or (5, 2) => 1,
                (4, 4) or (6, 4) => 2,
                (1, 1) or (1, 4) => 3,
                (4, 6) or (5, 3) => 4,
                (3, 5) or (5, 1) => 5,
                (2, 1) or (2, 6) => 6,
                (3, 4) or (4, 1) => 7,
                (1, 5) or (3, 2) => 8,
                _ => throw new ArgumentException(),
            }];

        public static readonly Maze[] AllMazes = new Maze[9]
        {
            new(new List<int>[Size] {
                new() {1, 6      }, new() {0, 2}, new() {1, 8}, new() {4, 9}, new() {3, 5}, new() {4},
                new() {0, 12     }, new() {8, 13}, new() {2, 7}, new() {3, 10}, new() {9, 11}, new() {10, 17},
                new() {6, 18     }, new() {7, 14}, new() {13, 20}, new() {16, 21}, new() {15, 17}, new() {11, 16, 23},
                new() {12, 24    }, new() {20}, new() {14, 19, 21}, new() {15, 20}, new() {23}, new() {17, 22, 29},
                new() {18, 25, 30}, new() {24, 26}, new() {25, 32}, new() {28, 33}, new() {27}, new() {23, 35},
                new() {24, 31    }, new() {30}, new() {26, 33}, new() {27, 32}, new() {35}, new() {29, 34},
                }),
            new(new List<int>[Size] {
                new() {1}, new() {0, 2, 7}, new() {1}, new() {4, 9}, new() {3, 5, 10}, new() {4},
                new() {7, 12}, new() {1, 6}, new() {9, 14}, new() {3, 8}, new() {4, 11}, new() {10, 17},
                new() {6, 18}, new() {14, 19}, new() {8, 13}, new() {16, 21}, new() {15, 17}, new() {11, 16, 23},
                new() {12, 19, 24 }, new() {13, 18}, new() {21, 26}, new() {15, 20}, new() {28}, new() {17, 29},
                new() {18, 30}, new() {31}, new() {20, 32}, new() {28, 33}, new() {22, 27}, new() {23, 35},
                new() {24}, new() {25, 32}, new() {26, 31}, new() {27, 34}, new() {33, 35}, new() {29, 34},
                }),
            new(new List<int>[Size] {
                new() {1, 6}, new() {0, 2}, new() {1, 8}, new() {9}, new() {5, 10}, new() {4, 11},
                new() {0}, new() {13}, new() {2, 14}, new() {3, 10}, new() {4, 9}, new() {5, 17},
                new() {13, 18}, new() {7, 12, 19}, new() {8, 20}, new() {16, 21}, new() {15, 22}, new() {11, 23},
                new() {12, 24}, new() {13, 25}, new() {14, 26}, new() {15, 27}, new() {16, 28}, new() {17, 29},
                new() {18, 30}, new() {19, 26}, new() {20, 25}, new() {21, 33}, new() {22, 34}, new() {23, 35},
                new() {24, 31}, new() {30, 32}, new() {31, 33}, new() {27, 32}, new() {28, 35}, new() {29, 34},
                }),
            new(new List<int>[Size] {
                new() {1, 6}, new() {0, 7}, new() {3}, new() {2, 4}, new() {3, 5}, new() {4, 11},
                new() {0, 12}, new() {1, 13}, new() {9, 14}, new() {8, 10}, new() {9, 11}, new() {5, 10, 17},
                new() {6, 18}, new() {7, 14}, new() {8, 13}, new() {16, 21}, new() {15}, new() {11, 23},
                new() {12, 24}, new() {20}, new() {19, 21}, new() {15, 20, 22}, new() {21, 23}, new() {17, 22, 29},
                new() {18, 25, 30}, new() {24, 26}, new() {25, 27}, new() {26, 28}, new() {27, 34}, new() {23, 35},
                new() {24, 31}, new() {30, 32}, new() {31}, new() {34}, new() {28, 33}, new() {29},
                }),
            new(new List<int>[Size] {
                new() {1}, new() {0, 2}, new() {1, 3}, new() {2, 4}, new() {3, 5, 10}, new() {4, 11},
                new() {7, 12}, new() {6, 8}, new() {7, 9}, new() {8, 10, 15}, new() {4, 9}, new() {5},
                new() {6, 13, 18}, new() {12, 19}, new() {15}, new() {9, 14}, new() {17, 22}, new() {16, 23},
                new() {12, 24}, new() {13, 20}, new() {19, 21}, new() {20, 27}, new() {16}, new() {17, 29},
                new() {18, 30}, new() {26, 31}, new() {25, 27}, new() {21, 26, 28}, new() {27}, new() {23, 35},
                new() {24}, new() {25, 32}, new() {31, 33}, new() {32, 34}, new() {33, 35}, new() {29, 34},
                }),
            new(new List<int>[Size] {
                new() {6}, new() {2, 7}, new() {1, 8}, new() {4}, new() {3, 5, 10}, new() {4, 11},
                new() {0, 12}, new() {1, 13}, new() {2, 14}, new() {10, 15}, new() {4, 9}, new() {5, 17},
                new() {6, 13, 18}, new() {7, 12}, new() {8}, new() {9, 21}, new() {17, 22}, new() {11, 16},
                new() {12, 19}, new() {18, 25}, new() {21, 26}, new() {15, 20, 27}, new() {16, 28}, new() {29},
                new() {25, 30}, new() {19, 24}, new() {20}, new() {21, 33}, new() {22, 29}, new() {23, 28, 35},
                new() {24, 31}, new() {30, 32}, new() {31, 33}, new() {27, 32}, new() {35}, new() {29, 34},
                }),
            new(new List<int>[Size] {
                new() {1, 6}, new() {0, 2}, new() {1, 3}, new() {2, 9}, new() {5, 10}, new() {4, 11},
                new() {0, 12}, new() {8, 13}, new() {7}, new() {3, 10}, new() {4, 9}, new() {5, 17},
                new() {6,13}, new() {7,12}, new() {15,20}, new() {14}, new() {17,22}, new() {11,16},
                new() {19,24}, new() {18,25}, new() {14,21,26}, new() {20,22}, new() {16,21}, new() {29},
                new() {18,30}, new() {19}, new() {20,27}, new() {26,28}, new() {27,34}, new() {23,35},
                new() {24,31}, new() {30,32}, new() {31,33}, new() {32,34}, new() {28,33,35}, new() {29,34},
                }),
            new(new List<int>[Size] {
                new() {6}, new() {2,7}, new() {1,3}, new() {2,9}, new() {5,10}, new() {4,11},
                new() {0,7,12}, new() {1,6,8}, new() {7}, new() {3,10}, new() {4,9}, new() {5,17},
                new() {6,18}, new() {14,19}, new() {13,15}, new() {14,16}, new() {15,22}, new() {11,23},
                new() {12,24}, new() {13,20}, new() {19,26}, new() {22}, new() {16,21,23}, new() {17,22},
                new() {18,30}, new() {31}, new() {20,27}, new() {26,28}, new() {27,29}, new() {28},
                new() {24,31}, new() {25,30,32}, new() {31,33}, new() {32,34}, new() {33,35}, new() {34},
                }),
            new(new List<int>[Size] {
                new() {6}, new() {2,7}, new() {1,3}, new() {2,4}, new() {3,5,10}, new() {4,11},
                new() {0,12}, new() {1,13}, new() {9,14}, new() {8}, new() {4,16}, new() {5,17},
                new() {6,13,18}, new() {7,12,14}, new() {8,13}, new() {16,21}, new() {10,15}, new() {11,23},
                new() {12,24}, new() {25}, new() {21,26}, new() {15,20}, new() {23}, new() {17,22,29},
                new() {18,30}, new() {19,31}, new() {20,32}, new() {28,33}, new() {27,34}, new() {23},
                new() {24,31}, new() {25,30}, new() {26,33}, new() {27,32}, new() {28,35}, new() {34},
                }),
        };

        public override string ToString()
        {
            const int gridSize = 6; // Assuming the maze is a 6x6 grid
            char[,] mazeGrid = new char[gridSize * 2 + 1, gridSize * 2 + 1];

            // Initialize the grid with walls
            for (int i = 0; i < mazeGrid.GetLength(0); i++)
            {
                for (int j = 0; j < mazeGrid.GetLength(1); j++)
                {
                    mazeGrid[i, j] = '#';
                }
            }

            // Add nodes and paths
            for (int i = 0; i < _neighbours.Length; i++)
            {
                int row = i / gridSize * 2 + 1;
                int col = i % gridSize * 2 + 1;

                mazeGrid[row, col] = '.'; // Node

                foreach (var neighbor in _neighbours[i])
                {
                    int neighborRow = neighbor / gridSize * 2 + 1;
                    int neighborCol = neighbor % gridSize * 2 + 1;

                    // Add path
                    if (neighborRow == row)
                    {
                        // Horizontal path
                        mazeGrid[row, (col + neighborCol) / 2] = '-';
                    }
                    else if (neighborCol == col)
                    {
                        // Vertical path
                        mazeGrid[(row + neighborRow) / 2, col] = '|';
                    }
                }
            }

            // Print the maze
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < mazeGrid.GetLength(0); i++)
            {
                for (int j = 0; j < mazeGrid.GetLength(1); j++)
                {
                    stringBuilder.Append(mazeGrid[i, j]);
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }


        public const int MinimalValue = 1;
        public const int MaximalValue = 6;
        public const int Width = MaximalValue;
        public const int Height = MaximalValue;
        public const int Size = Width * Height;
    }
}
