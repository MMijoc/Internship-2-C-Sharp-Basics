using System;

enum error
{
	SUCCESS = 0,
	FAILURE = (-1)
}


namespace PlaylistMaker
{
	class Program
	{
		static int Main(string[] args)
		{ 
			return SelectMenu();
		}


		static int PrintMenu()
		{
			Console.WriteLine("Izbornik:");
			Console.WriteLine("1 - Ispis cijele liste:");
			Console.WriteLine("2 - Ispis imena pjesme unosom pripadajućeg rednog broja:");
			Console.WriteLine("3 - Ispis rednog broja pjesme unosom pripadajućeg imena:");
			Console.WriteLine("4 - Unos nove pjesme:");
			Console.WriteLine("5 - Brisanje pjesme po rednom broju:");
			Console.WriteLine("6 - Brisanje pjesme po imenu:");
			Console.WriteLine("7 - Brisanje cijele liste:");
			Console.WriteLine("8 - Uređivanje imena pjesme:");
			Console.WriteLine("9 - Uređivanje rednog broja pjesme, odnosno premještanje pjesme na novi redni broj u listi:");
			Console.WriteLine("10 - Shuffle pjesama, odnosno nasumično premještanje elemenata liste:");
			Console.WriteLine("11 - izlaz");
			Console.WriteLine("Izbor: ");

			return (int)error.SUCCESS;
		}


		static int SelectMenu()
		{
			var input = "";
			var select = 0;
			var isNumber = true;

			while (true)
			{
				PrintMenu();
				isNumber = int.TryParse(input = Console.ReadLine(), out select);

				if (isNumber != true) {
					Console.WriteLine("Nepoznata naredba \"{0}\"\nPritnisni bilo koju tipku za nastavak . . .", input);
					Console.ReadLine();
					Console.Clear();
					continue;
				}

				if (select == 1) {
					

				} else if (select == 2) {
					

				} else if (select == 3) {
					

				} else if (select == 4) {
					

				} else if (select == 5) {
					

				} else if (select == 6) {
					

				} else if (select == 7) {
					

				} else if (select == 8) {
					

				} else if (select == 9) {
					

				} else if (select == 10) {
					

				} else if (select == 11) {
					break;

				} else {
					Console.WriteLine("Nepoznata naredba \"{0}\"\nPritnisni bilo koju tipku za nastavak . . .", input);
					Console.ReadLine();
					Console.Clear();
					continue;
				}

			}

			return (int)error.SUCCESS;
		}

	}
}














