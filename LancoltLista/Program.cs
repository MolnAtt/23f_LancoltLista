using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LancoltLista
{
	internal class Program
	{

		class LancoltLista<T>
		{
			class Elem<T>
			{
				public Elem<T> bal;
				public T value;
				public Elem<T> jobb;

				// Ha valaki úgy hoz létre Elem-et, hogy nem ad meg semmi mást, akkor ő egy fejelemet hozzon létre!
				public Elem()
				{
					this.bal = this;
					this.value = default;
					this.jobb = this;
				}

				public Elem(Elem<T> bal, T value, Elem<T> jobb)
				{
					this.bal = bal;
					this.value = value;
					this.jobb = jobb;
				}

				// Beszúrás egy elem mögé
				public Elem(Elem<T> emoge, T ertek)
				{
					this.value = ertek;
					// a sorrend most nagyon fontos!
					this.bal = emoge;
					this.jobb = emoge.jobb;
					emoge.jobb = this;
					this.jobb.bal = this;
				}

				public void Töröl()
				{
					this.bal.jobb = this.jobb;
					this.jobb.bal = this.bal;
					this.bal = null;
					this.jobb = null;
				} // mivel mindenki elfelejti, ezért megszűnik létezni. --> Garbage collector.
			}

			private Elem<T> fejelem;
			private int count;
			public int Count { get => count; }
			public LancoltLista()
			{
				this.fejelem = new Elem<T> ();
				this.count = 0;
			}
			public void Add(T ujertek)
			{
				new Elem<T>(this.fejelem.bal, ujertek);
				count++;
			}
			private Elem<T> Keres(T ertek)
			{
				Elem<T> aktelem = fejelem.jobb;
				while (aktelem != fejelem && !aktelem.value.Equals(ertek)) 
				{
					aktelem = aktelem.jobb;
				}
				return aktelem;
			}
			public void Remove(T ertek)
			{
				Elem<T> elem = Keres(ertek);
				if (elem == fejelem)
				{
					Console.Error.WriteLine($"Hát igazából nem volt \"{ertek}\" elem, úgyhogy nem töröltem semmit.");
				}
				else
				{
					elem.Töröl();
					count--;
				}
			}
			public void RemoveAll(T ertek)
			{
				Elem<T> aktelem = fejelem.jobb;
				while (aktelem != fejelem)
				{
					aktelem = aktelem.jobb;
					if (aktelem.bal.value.Equals(ertek))
					{
						aktelem.bal.Töröl();
						count--;
					}
				}
			}
			public void RemoveAt(int i)
			{
				if (count <= i)
				{
					throw new ArgumentOutOfRangeException($"Túl nagy a(z) {i}, mert ez a lista csak {count} elemből áll!");
				}

				if (i < 0)
				{
					throw new ArgumentOutOfRangeException($"Negatív a(z) {i}!");
				}

				Elem<T> aktelem = fejelem.jobb;
				for (int l = 0; l < i; l++)
				{
					aktelem=aktelem.jobb;
				}
				aktelem.Töröl();
				count--;
			}

			// FindIndex


			/// <summary>
			/// Megkeresi az első elemet a listában, amire teljesül az adott tulajdonság. Ha nincs ilyen, akkor -1-et ad vissza.
			/// </summary>
			/// <param name="predikatum">A tulajdonság</param>
			/// <returns>A megtalált első elem indexe.</returns>
			public int FindIndex() => Count == 0 ? -1 : 0;
			public int FindIndex(Func<T, bool> predikatum)
			{
				int i = 0;
				Elem<T> aktelem = fejelem.jobb;
				while (aktelem != fejelem && !predikatum(aktelem.value))
				{
					i++;
					aktelem = aktelem.jobb;
				}
				return aktelem == fejelem ? -1 : i;
			}

			// FindLastIndex
			// Where
			// Count
			// Select
			// First
			// Last
			// Sum
			// Reverse


			public override string ToString()
			{
				string s = "[ ";
				Elem<T> aktelem = fejelem.jobb;
				while (aktelem != fejelem)
				{
					s += aktelem.value+" ";
					aktelem = aktelem.jobb;
				}
				return s + "]";
			}

		}

		static void Main(string[] args)
		{
			LancoltLista<int> lista = new LancoltLista<int>();
			lista.Add(10);
			Console.WriteLine(lista);
			lista.Add(4);
			Console.WriteLine(lista);
			lista.Add(0);
			Console.WriteLine(lista);
			lista.Add(2);
			Console.WriteLine(lista);
			lista.Add(0);
			Console.WriteLine(lista);
			lista.Add(9);
			Console.WriteLine(lista);
			lista.Add(0);
			Console.WriteLine(lista);
			lista.Add(0);
			Console.WriteLine(lista);
			lista.Add(12);
			Console.WriteLine(lista);

			lista.Remove(9);
			Console.WriteLine(lista);
			lista.Remove(42);
			Console.WriteLine(lista);
			lista.Add(0);
			Console.WriteLine(lista);
			lista.RemoveAll(43);
			Console.WriteLine(lista);
			lista.RemoveAll(0);
			Console.WriteLine(lista);

			Console.WriteLine("törlések:0. és 2. elemek:");
			lista.RemoveAt(0);
			Console.WriteLine(lista);
			lista.RemoveAt(2);
			Console.WriteLine(lista);
			lista.RemoveAt(2);
			Console.WriteLine(lista);



			List<string> hagyomanyos_lista = new List<string>();
			hagyomanyos_lista.Remove("alma");
			hagyomanyos_lista.RemoveAt(0);
			Console.WriteLine(hagyomanyos_lista.Count);
			hagyomanyos_lista.Contains("körte");


			hagyomanyos_lista.FindIndex(x => x.Length > 3);
			hagyomanyos_lista.FindLastIndex(x => x.Length > 3);
			hagyomanyos_lista.First(x => x.Length > 3);
			hagyomanyos_lista.Last(x => x.Length > 3);
			hagyomanyos_lista.Where(x => x.Length > 3);
			hagyomanyos_lista.Select(x => x.Length);




		}
	}
}
