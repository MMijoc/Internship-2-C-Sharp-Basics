using System;
using System.Collections.Generic;

enum ResultType
{
	Success,
	Failure = -1
}

namespace PlaylistMaker
{
	class Program
	{
		static int Main(string[] args)
		{

			return SelectMenu();
		}

		static void PrintMenu()
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
			Console.WriteLine("0 - izlaz");
			Console.Write("Izbor: ");

		}

		static int SelectMenu()
		{
			Dictionary<int, string> playListDictionary = new Dictionary<int, string>();

			//testing samples
			char c;
			string s;
			for (int i = 1; i < 10; i++) {
				c = (char)('A' + i - 1);
				s = c.ToString();
				playListDictionary.Add(i, s);
			}

			while (true)
			{
				var input = "";
				var songID = 0;
				var songName = "";

				PrintMenu();
				var isNumber = int.TryParse(input = Console.ReadLine(), out var select);

				if (isNumber != true) {
					Console.WriteLine("Nepoznata naredba \"{0}\"\nPritnisni bilo koju tipku za nastavak . . .", input);
					Console.ReadLine();
					Console.Clear();
					continue;
				}

				switch (select)
				{
					case 1:
						PrintList(playListDictionary);
						break;
					case 2:
						songID = InputNumber("Unesite redni broj pjesme: ");
						PrintSong(playListDictionary, songID);
						break;
					case 3:
						Console.Write("Unseite ime pjesme: ");
						songName = Console.ReadLine();
						songName = songName.Trim();
						PrintSong(playListDictionary, songName);
						break;
					case 4:
						while (string.IsNullOrEmpty(songName)) {
							Console.Write("Unseite ime nove pjesme: ");
							songName = Console.ReadLine();
							songName = songName.Trim();
						}

						if (ConfirmAction()) {
							if (AddSong(playListDictionary, songName) == (int)ResultType.Success)
								Console.WriteLine("Pjesma je dodana na kraj playliste!");

						} else {
							Console.WriteLine("Promjene odbačene, pjesma nije dodana!");

						}
						break;
					case 5:
						songID = InputNumber("Unesite redni broj pjesme koju želite izbrisati: ");
						DeleteSong(playListDictionary, songID);
						break;
					case 6:
						Console.Write("Unseite ime pjesme koju zelite izbrisati: ");
						songName = Console.ReadLine();
						DeleteSong(playListDictionary, songName);
						break;
					case 7:
						Console.WriteLine("Jeste li sigurni da stvarno zelite izbrisati cijelu listu?");
						if (ConfirmAction())
						{
							DeletePlaylist(playListDictionary);
							Console.WriteLine("Playlista je uspjesno izbrisana");

						}
						break;
					case 8:
						PrintList(playListDictionary);
						songID = InputNumber("\nUnesite redni broj pjesme koju želite preimenovati: ");
						RenameSong(playListDictionary, songID);
						break;
					case 9:
						ReorderSongs(playListDictionary);
						break;
					case 10:
						Console.WriteLine("Želite li nasumično izmješati playlisu?");
						if (ConfirmAction())
						{
							ShufflePlaylist(playListDictionary);
							Console.WriteLine("Playlista je uspješno izmješana");
						}
						break;
					case 0:
						return (int)ResultType.Success;
					default:
						Console.WriteLine("Nepoznata naredba \"{0}\"", input);
						break;
				}

				Console.WriteLine("\nPritnisni bilo koju tipku za nastavak . . .");
				Console.ReadLine();
				Console.Clear();
			}

			
		}
		static int InputNumber(string message)
		{
			while (true) {
				if (message != "")
					Console.Write(message);
				bool isNumber = int.TryParse(Console.ReadLine(), out int number);

				if (isNumber)
					return number;
				else
					Console.WriteLine("Nepravilan unos!");
			} 
		}
		static int PrintList(Dictionary<int, string> playList)
		{
			int count = playList.Count;
			if (count == 0) {
				Console.WriteLine("Playlista je prazna!");
				return (int)ResultType.Failure;
			}

			Console.WriteLine("\n{0, -8} {1, -32}", "ID", "Song name");
			for (int i = 1; i <= count; i++)
				Console.WriteLine("{0, -8} {1, -32}", i, playList[i]);

			return (int)ResultType.Success;
		}

		static int PrintSong(Dictionary<int, string> playList, int songID)
		{
			var songExists = false;
			foreach (var song in playList) {
				if (song.Key == songID) {
					Console.WriteLine("{0, -8} {1, -32}", song.Key, song.Value);
					songExists = true;
					break;
				}
			}

			if (!songExists) {
				Console.WriteLine("Ne postoji pjesma s rednim brojem \"{0}\"", songID);
				return (int)ResultType.Failure;
			}

			return (int)ResultType.Success;
		}

		static int PrintSong(Dictionary<int, string> playList, string songName) {
			var songExists = false;

			foreach (var song in playList) {
				if (songName.Equals(song.Value, StringComparison.OrdinalIgnoreCase)) {
					Console.WriteLine("{0, -8} {1, -32}", song.Key, song.Value);
					songExists = true;
					break;
				}
			}

			if (!songExists) {
				Console.WriteLine("Ne postoji pjesma s imenom \"{0}\"", songName);
				return (int)ResultType.Failure;
			}

			return (int)ResultType.Success;
		}

		static int AddSong(Dictionary<int, string> playList, string songName)
		{
			var newSongId = playList.Count + 1;

			foreach (var song in playList)
				if (song.Value == songName) {
					Console.WriteLine("Pjesma s imenom \"{0}\" već postoji!", songName);
					return (int)ResultType.Failure;
				}

			playList.Add(newSongId, songName);

			return (int)ResultType.Success;
		}

		static int DeleteSong(Dictionary<int, string> playList, int songID)
		{
			string tmp;
			int count = playList.Count;

			if (!playList.ContainsKey(songID)) {
				Console.WriteLine("Ne postoji pjesma s rednim brojem \"{0}\"", songID);
				return (int)ResultType.Failure;
			}

			Console.Write("Jeste li sigurni da želite obrisati:\n\t");
			PrintSong(playList, songID);

			if (!ConfirmAction()) return (int)ResultType.Success;

			var isDeleted = playList.Remove(songID);
			if (!isDeleted) {
				Console.WriteLine("Nije moguće obrisati pjesmu s rednim brojem \"{0}\"", songID);
				return (int)ResultType.Failure;
			}

			songID++;
			for (int i = songID; i <= count; i++) {
				tmp = playList[i];
				playList.Remove(i);
				playList.Add(i - 1, tmp);
			}

			return (int)ResultType.Success;
		}

		static int DeleteSong(Dictionary<int, string> playList, string songName)
		{
			int key = 0;

			foreach (var song in playList)
				if (songName.Equals(song.Value, StringComparison.OrdinalIgnoreCase))
					key = song.Key;

			if (key != 0) {
				DeleteSong(playList, key);
				return (int)ResultType.Success;
			} else {
				Console.WriteLine("Ne postoji pjesma s imenom {0}", songName);
				return (int)ResultType.Failure;
			}
		}

		static bool ConfirmAction()
		{
			Console.WriteLine("Jeste li sigurni da zelite nastaviti?\n\tDA - da želim nastaviti\n\tNE - ne zelim nastaviti (unesi bilo koju tipku za prekid)");
			var input = Console.ReadLine();

			return input.Equals("da", StringComparison.OrdinalIgnoreCase);
		}

		static int DeletePlaylist(Dictionary<int, string> playList)
		{
			int count = playList.Count;
			bool wasDeleted = true;
			int i = 1;

			
			while (wasDeleted == true && i <= count) {
				wasDeleted = playList.Remove(i);
				i++;
			}

			if (wasDeleted)
				return (int)ResultType.Success;
			else
				return (int)ResultType.Failure;
		}

		static int RenameSong(Dictionary<int, string> playList, int songID)
		{
			if (playList.ContainsKey(songID) != true) {
				Console.WriteLine("Ne psotoji pjesma s rednim brojem \"{0}\"", songID);
				return (int)ResultType.Failure;
			}

			Console.Write("Kako zelite preimenovati pjesmu  \"{0}\": ", playList[songID]);
			string input = Console.ReadLine();
			Console.WriteLine("Premienuj \"{0}\" u \"{1}\"", playList[songID], input);
			if (ConfirmAction()) {
				Console.WriteLine("Pjesma \"{0}\" je preimenovana u \"{1}\"", playList[songID], input);
				playList[songID] = input;
			}

			return (int)ResultType.Success;
		}

		static int ReorderSongs(Dictionary<int, string> playList)
		{
			bool contains;
			int conut = playList.Count;
			int i;
			int songID_1;
			int songID_2;

			Dictionary<int, string> tmpDict = new Dictionary<int, string>(playList);
			PrintList(playList);

			while (true) {
				songID_1 = InputNumber("\nUnesite redni broj pjesme koju želite premjestit: ");
				contains = playList.ContainsKey(songID_1);
				if (contains)
					break;
				Console.WriteLine("Ne postoji pjesma s rednim brojem \"{0}\"", songID_1);
			}

			while (true) {
				songID_2 = InputNumber("\nUnesite redni broj na koji zelite prmejstiti odabranu pjesmu: ");
				contains = playList.ContainsKey(songID_2);
				if (contains)
					break;
				Console.WriteLine("Ne postoji pjesma s rednim brojem \"{0}\"", songID_2);
			}

			Console.WriteLine("Premjsti pjesmu s rednim brojem {0}  \"{1}\" na mjesto {2}", songID_1, playList[songID_1], songID_2);
			if (!ConfirmAction())
				return (int)ResultType.Success;


			if (songID_1 > songID_2) {
				DeletePlaylist(playList);
				for (i = 1; i < songID_2; i++)
					playList.Add(i, tmpDict[i]);

				playList.Add(i, tmpDict[songID_1]);

				for (i+= 1; i <= songID_1; i++)
					playList.Add(i, tmpDict[i-1]);

				for (; i <= conut; i++)
					playList.Add(i, tmpDict[i]);

			} else if (songID_1 < songID_2) {
				DeletePlaylist(playList);
				for (i = 1; i < songID_1; i++)
					playList.Add(i, tmpDict[i]);

				for (; i < songID_2; i++)
					playList.Add(i, tmpDict[i + 1]);

				playList.Add(i, tmpDict[songID_1]);

				for (i += 1; i <= conut; i++)
					playList.Add(i, tmpDict[i]);
			}

			return (int)ResultType.Success;
		}

		static int ShufflePlaylist(Dictionary<int, string> playList)
		{
			int n = playList.Count;
			int[] arr = new int[n];
			int tmp, j;
			Dictionary<int, string> tmpDict = new Dictionary<int, string>(playList);
			Random rnd = new Random();

			for (int i = 0; i < n; i++)
				arr[i] = i + 1;

			for (int i = n - 1; i > 0; i--) {
				j = rnd.Next() % (i + 1);
				tmp = arr[j];
				arr[j] = arr[i];
				arr[i] = tmp;
			}

			DeletePlaylist(playList);
			for (int i = 0; i < n; i++)
				playList.Add(i + 1, tmpDict[arr[i]]);

			return (int)ResultType.Success;
		}
	}
}