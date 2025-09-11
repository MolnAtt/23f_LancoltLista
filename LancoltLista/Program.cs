using System;
using System.Linq;
using System.Text;
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

			public LancoltLista()
			{
				this.fejelem = new Elem<T> ();
			}

			public void Add(T ujertek)
			{
				new Elem<T>(this.fejelem.bal, ujertek);
			}

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
			lista.Add(2);
			Console.WriteLine(lista);
			lista.Add(9);
			Console.WriteLine(lista);
			lista.Add(0);
			Console.WriteLine(lista);
			lista.Add(12);
			Console.WriteLine(lista);


		}
	}
}
