using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;

namespace Proyecto
{
	/// <summary>
	/// Compara objetos del tipo PathNode. Comprueba el coste de cada uno para ordenarlos por coste.
	/// </summary>
	public class NodeCostComparer : IComparer
	{
		/// <summary>
		/// Constructor de la clase (herencia de IComparer)
		/// </summary>
		public NodeCostComparer() : base() { }
		
		/// <summary>
		/// Compara ambos objetos
		/// </summary>
		/// <param name="x">Objeto 1</param>
		/// <param name="y">Objeto 2</param>
		/// <returns>Número de posiciones que hay que desplazar el elemento</returns>
		int IComparer.Compare(object x, object y)
		{
			
			PathNode elex = (PathNode)x;
			PathNode eley = (PathNode)y;
			
			if (elex == null && eley == null)
			{
				return 0;
			}
			else if (elex == null && eley != null)
			{
				return -1;
			}
			else if (elex != null && eley == null)
			{
				return 1;
			}
			else
			{
				return elex.cost.CompareTo(eley.cost);
			}
		}
	}
	
	/// <summary>
	/// Estructura de datos del tipo LIFO (Last in, First Out)
	/// </summary>
	class Stack
	{
		/// <summary>
		/// Lista efectiva
		/// </summary>
		private ArrayList stack;
		
		/// <summary>
		/// Constructor de la clase
		/// </summary>
		public Stack()
		{
			stack = new ArrayList();
		}
		
		/// <summary>
		/// Añade un elemento al final de la lista
		/// </summary>
		/// <param name="obj">Elemento a añadir</param>
		public void Push(Object obj)
		{
			stack.Add(obj);
		}
		
		/// <summary>
		/// Extrae el último elemento de la lista y lo elimina
		/// </summary>
		/// <returns>Elemento extraido</returns>
		public Object Pop()
		{
			Object ret = stack[stack.Count - 1];
			stack.RemoveAt(stack.Count - 1);
			return ret;
		}
		
		/// <summary>
		/// Número de elementos que contiene la pila
		/// </summary>
		/// <returns>Número de elementos que contiene la pila</returns>
		public int Count()
		{
			return stack.Count;
		}
	}
	
	/// <summary>
	/// Nodo de la ruta a elegir
	/// </summary>
	class PathNode
	{
		/// <summary>
		/// Posición X
		/// </summary>
		public int x;
		
		/// <summary>
		/// Posición Y
		/// </summary>
		public int y;
		
		/// <summary>
		/// Indica si este nodo ha sido ya visitado
		/// </summary>
		public bool visited;
		
		/// <summary>
		/// Nodo padre (X)
		/// </summary>
		public int parentx;
		
		/// <summary>
		/// Nodo padre (Y)
		/// </summary>
		public int parenty;
		
		/// <summary>
		/// Coste de llegada
		/// </summary>
		public int cost;
		
		/// <summary>
		/// Nodo de la ruta a elegir
		/// </summary>
		/// <param name="_x">Posición X</param>
		/// <param name="_y">Posición Y</param>
		/// <param name="_visited">Indica si este nodo ha sido ya visitado</param>
		/// <param name="_parentx">Nodo padre (X)</param>
		/// <param name="_parenty">Nodo padre (Y)</param>
		/// <param name="_cost">Coste de llegada</param>
		public PathNode(int _x, int _y, bool _visited, int _parentx, int _parenty, int _cost)
		{
			x = _x;
			y = _y;
			visited = _visited;
			parentx = _parentx;
			parenty = _parenty;
			cost = _cost;
		}
	}
	
	/// <summary>
	/// Camino del buscador de rutas
	/// </summary>
	class Path
	{
		/// <summary>
		/// Lista de nodos que aún no han sido visitados
		/// </summary>
		public ArrayList uncheckedNeighbors;
		
		/// <summary>
		/// Posición inicial del camino (X)
		/// </summary>
		public int startx;
		
		/// <summary>
		/// Posición final del camino (Y)
		/// </summary>
		public int starty;
		
		/// <summary>
		/// Camino del buscador de rutas
		/// </summary>
		/// <param name="sx">Posición inicial del camino (X)</param>
		/// <param name="sy">Posición inicial del camino (Y)</param>
		public Path(int sx, int sy)
		{
			startx = sx;
			starty = sy;
			uncheckedNeighbors = new ArrayList();
		}
	}
	
	/// <summary>
	/// Coordenada en el mapa
	/// </summary>
	class Coord
	{
		/// <summary>
		/// Posición X
		/// </summary>
		public int x;
		
		/// <summary>
		/// Posición Y
		/// </summary>
		public int y;
		
		/// <summary>
		/// Coordenada en el mapa
		/// </summary>
		/// <param name="_x">Posición X</param>
		/// <param name="_y">Posición Y</param>
		public Coord(int _x, int _y)
		{
			x = _x;
			y = _y;
		}
	}
	
	/// <summary>
	/// Buscador de rutas
	/// </summary>
	class Pathfinder
	{
		/// <summary>
		/// Tabla donde se almacenan todos los nodos candidatos
		/// </summary>
		public static Hashtable nodes = new Hashtable();
		
		/// <summary>
		/// Pila de coordenadas indicando la ruta a seguir
		/// </summary>
		public static Stack finalpath = new Stack();
		
		/// <summary>
		/// Heurística para buscar el camino
		/// </summary>
		public static short Algorithm;
		
		/// <summary>
		/// Añade un Nodo a la tabla
		/// </summary>
		/// <param name="map">Mapa donde buscar el camino</param>
		/// <param name="ob">Nodo Padre</param>
		/// <param name="path">Camino a seguir</param>
		/// <param name="x">Posición X del nodo</param>
		/// <param name="y">Posición Y del nodo</param>
		/// <param name="targetx">Posición X del destino</param>
		/// <param name="targety">Posición Y del destino</param>
		public static void addNode(char[,] map, PathNode ob, Path path, int x, int y, int targetx, int targety)
		{
			string nodekey = "" + x + "," + y + "";
			if (map[x,y] != 'X')
			{
				int cost;
				
				switch (Algorithm)
				{
				case 1:
				{
					cost = Math.Abs(x - targetx) + Math.Abs(y - targety);
					break;
				}
				case 2:
				{
					cost = (int)(Math.Sqrt(Math.Pow((x - targetx), 2) + Math.Pow((y - targety), 2)));
					break;
				}
				case 3:
				{
					cost = (int)(Math.Pow((x - targetx), 2) + Math.Pow((y - targety), 2));
					break;
				}
				default:
				{
					cost = (Math.Max(Math.Abs(x - targetx), Math.Abs(y - targety)));
					break;
				}
				}
				
				if (!nodes.ContainsKey(nodekey))
				{
					PathNode NewNode = new PathNode(x, y, false, ob.x, ob.y, cost);
					path.uncheckedNeighbors.Add(NewNode);
					path.uncheckedNeighbors.Sort(new NodeCostComparer());
					nodes.Add(nodekey, NewNode);
				}
			}
		}
		
		/// <summary>
		/// Convierte el camino a una pila de coordenadas
		/// </summary>
		/// <param name="N">Camino elegido</param>
		/// <param name="map">Matriz del mapa</param>
		public static void makePath(PathNode N, char[,] map)
		{
			while (N.parentx != -1)
			{
				finalpath.Push(new Coord(N.x, N.y));
				map[N.x, N.y] = 'P';
				printMap(map);
				Thread.Sleep(50);
				N = (PathNode)nodes["" + N.parentx + "," + N.parenty + ""];
			}
		}
		
		/// <summary>
		/// Busca un camino desde una posición hasta otra en un mapa
		/// </summary>
		/// <param name="map">Matriz del mapa</param>
		/// <param name="startx">Posición Inicial X</param>
		/// <param name="starty">Posición Inicial Y</param>
		/// <param name="targetx">Posición Destino X</param>
		/// <param name="targety">Posición Destino Y</param>
		/// <returns></returns>
		public static bool findPath(char[,] map, int startx, int starty, int targetx, int targety)
		{
			Path path = new Path(starty, startx);
			int cost;

			cost = (Math.Max(Math.Abs(startx - targetx), Math.Abs(starty - targety)));

			PathNode FirstNode = new PathNode(starty, startx, true, -1, -1, cost);
			
			nodes.Add("" + starty + "," + startx + "", FirstNode);
			
			path.uncheckedNeighbors.Add(FirstNode);
			
			while (path.uncheckedNeighbors.Count > 0)
			{
				PathNode N = (PathNode)path.uncheckedNeighbors[0];
				path.uncheckedNeighbors.RemoveAt(0);
				
				if (N.x == targety && N.y == targetx)
				{
					makePath(N, map);
					return true;
				}
				else
				{
					N.visited = true;
					map[N.x, N.y] = 'V';
					printMap(map);
					//Thread.Sleep(100);
					if (N.x + 1 < 16)
					{
						if (map[N.x + 1, N.y] != 'X' && map[N.x + 1, N.y] != 'V')
						{
							map[N.x + 1, N.y] = 'C';
							//printMap(map);
							//Thread.Sleep(100);
						}
						addNode(map, N, path, N.x + 1, N.y, targety, targetx);
					}
					if (N.x - 1 >= 0)
					{
						if (map[N.x - 1, N.y] != 'X' && map[N.x - 1, N.y] != 'V')
						{
							map[N.x - 1, N.y] = 'C';
							//printMap(map);
							//Thread.Sleep(100);
						}
						addNode(map, N, path, N.x - 1, N.y, targety, targetx);
					}
					if (N.y + 1 < 16)
					{
						if (map[N.x, N.y + 1] != 'X' && map[N.x, N.y + 1] != 'V')
						{
							map[N.x, N.y + 1] = 'C';
							//printMap(map);
							//Thread.Sleep(100);
						}
						addNode(map, N, path, N.x, N.y + 1, targety, targetx);
					}
					if (N.y - 1 >= 0)
					{
						if (map[N.x, N.y - 1] != 'X' && map[N.x, N.y - 1] != 'V')
						{
							map[N.x, N.y - 1] = 'C';
							//printMap(map);
							//Thread.Sleep(100);
						}
						addNode(map, N, path, N.x, N.y - 1, targety, targetx);
					}
				}
			}
			return false;
		}
		
		/// <summary>
		/// Muestra el mapa por pantalla
		/// </summary>
		/// <param name="map">Matriz del mapa</param>
		public static void printMap(char[,] map)
		{
			int _height = 16; // ALTURA DEL MAPA
			int _width = 16; // ALTURA DEL MAPA
			Console.Clear();
			for (int i = 0; i < _height; i++)
			{
				for (int j = 0; j < _width; j++)
				{
					Console.ForegroundColor = ConsoleColor.White;
					if (map[i, j] == 'X')
					{
						Console.ForegroundColor = ConsoleColor.Red;
					}
					else if (map[i, j] == 'V')
					{
						Console.ForegroundColor = ConsoleColor.Yellow;
					}
					else if (map[i, j] == 'P')
					{
						Console.ForegroundColor = ConsoleColor.Green;
					}
					else if (map[i, j] == 'C')
					{
						Console.ForegroundColor = ConsoleColor.DarkYellow;
					}
					Console.Write(map[i, j]);
				}
				Console.WriteLine();
			}
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("O - Tile Accesible (Suelo)");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("X - Tile No Accesible (Pared)");
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("C - Tile Candidata");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("V - Tile Candidata Visitada");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("P - Camino Definitivo");
			Thread.Sleep(50);
		}
		
		/// <summary>
		/// Devuelve una lista de coordenadas a las que se debe seguir
		/// </summary>
		/// <returns></returns>
		public static string Coords()
		{
			string coord = "";
			while(finalpath.Count() > 0)
			{
				Coord c = (Coord)finalpath.Pop();
				coord = coord + " -> [" + c.x + "," + c.y + "]";
			}
			return coord;
		}
		
		/// <summary>
		/// Test del algoritmo
		/// </summary>
		public static void Main()
		{
			char[,] map = { 
				{ 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X' }, 
				{ 'X', 'O', 'O', 'X', 'O', 'X', 'O', 'O', 'O', 'X', 'O', 'X', 'X', 'O', 'O', 'X' }, 
				{ 'X', 'O', 'X', 'O', 'O', 'O', 'O', 'X', 'X', 'X', 'O', 'X', 'O', 'X', 'O', 'X' }, 
				{ 'X', 'O', 'X', 'X', 'X', 'X', 'X', 'X', 'O', 'O', 'O', 'X', 'X', 'X', 'X', 'X' }, 
				{ 'X', 'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'X', 'O', 'O', 'O', 'O', 'O', 'X' }, 
				{ 'X', 'X', 'X', 'X', 'X', 'O', 'X', 'X', 'X', 'X', 'X', 'O', 'O', 'X', 'X', 'X' }, 
				{ 'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'X' }, 
				{ 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'O', 'O', 'X' }, 
				{ 'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'X', 'O', 'X', 'X', 'O', 'O', 'X' }, 
				{ 'X', 'X', 'X', 'X', 'X', 'X', 'O', 'O', 'X', 'X', 'X', 'X', 'O', 'O', 'O', 'X' }, 
				{ 'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'X' }, 
				{ 'X', 'O', 'O', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X' }, 
				{ 'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'X' }, 
				{ 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'O', 'O', 'O', 'X', 'X', 'X', 'X' }, 
				{ 'X', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'X' }, 
				{ 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X' } };
			
			Console.WindowHeight = 45;
			Console.WriteLine("Bienvenid@ al sistema buscador de rutas");
			Console.WriteLine();
			Console.WriteLine("Ingrese las coordenadas x, y de inicio");
			Console.Write("x: ");
			int x1 = Convert.ToInt16 (Console.ReadLine());
			Console.Write("y: ");
			int y1 = Convert.ToInt16 (Console.ReadLine());
			Console.WriteLine("Ingrese las coordenadas x, y de llegada");
			Console.Write("x: ");
			int x2 = Convert.ToInt16 (Console.ReadLine());
			Console.Write("y: ");
			int y2 = Convert.ToInt16 (Console.ReadLine());
						
			if (x1 > 15)
			{
				x1 = 15;
			}
			if (x1 < 0)
			{
				x1 = 0;
			}
			if (y1 > 15)
			{
				y1 = 15;
			}
			if (y1 < 0)
			{
				y1 = 0;
			}
			if (x2 > 15)
			{
				x2 = 15;
			}
			if (x2 < 0)
			{
				x2 = 0;
			}
			if (y2 > 15)
			{
				y2 = 15;
			}
			if (y2 < 0)
			{
				y2 = 0;
			}

			printMap(map);
			if (map[x1, y1] != 'X' && map[x2, y2] != 'X')
			{
				if(findPath(map, x1, y1, x2, y2)) {
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.WriteLine();
					Console.WriteLine("Camino Encontrado: " + Coords() + "");
				}
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine();
				Console.WriteLine("Camino NO Encontrado (No se puede acceder a esa posición)");
			}
			
			Console.ReadLine();
		}
	}
}
