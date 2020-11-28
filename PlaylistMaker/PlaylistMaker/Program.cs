﻿using System;
using System.Collections.Generic;

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
			Console.Write("Izbor: ");

			return (int)error.SUCCESS;
		}

		static int SelectMenu()
		{
			var input = "";
			var select = 0;
			var isNumber = true;
			var songID = 0;
			var songName = "";
			var lastSongID = 0;

			Dictionary<int, string> playListDictionary = new Dictionary<int, string>();

			//lastSongID++;
			//playListDictionary.Add(lastSongID, "Accross the rainbow bridge");

			//lastSongID++;
			//playListDictionary.Add(lastSongID, "Elan");

			//lastSongID++;
			//playListDictionary.Add(lastSongID, "Noldor");


			while (true)
			{
				input = "";
				select = 0;

				isNumber = false;

				PrintMenu();
				isNumber = int.TryParse(input = Console.ReadLine(), out select);

				if (isNumber != true) {
					Console.WriteLine("Nepoznata naredba \"{0}\"\nPritnisni bilo koju tipku za nastavak . . .", input);
					Console.ReadLine();
					Console.Clear();
					continue;
				}

				if (select == 1) {
					PrintList(playListDictionary);

				} else if (select == 2) {
					do {
						Console.Write("Unesite redni broj pjesme: ");
						isNumber = int.TryParse(input = Console.ReadLine(), out songID);
						if (isNumber)
							PrintSong(playListDictionary, songID);
						else
							Console.WriteLine("Nepravilan unos!");

					} while (isNumber == false);

				} else if (select == 3) {
					Console.Write("Unseite ime pjesme: ");
					input = Console.ReadLine();
					PrintSong(playListDictionary, input);

				} else if (select == 4) {
					Console.Write("Unseite ime nove pjesme: ");
					songName = Console.ReadLine();

					Console.WriteLine("Jeste li sigurni da zelite unijeti novu pjesmu pod imenom: \"{0}\"", songName);
					Console.WriteLine("DA NE?");
					input = Console.ReadLine();
					if (input == "DA") { 
						AddSong(playListDictionary, songName);
						Console.WriteLine("Pjesma je dodana na karaj playliste!");
					} else {
						Console.WriteLine("Promjene odbačene, pjesma nije dodana!");
					}

				} else if (select == 5) {
					

				} else if (select == 6) {
					

				} else if (select == 7) {
					

				} else if (select == 8) {
					

				} else if (select == 9) {
					

				} else if (select == 10) {
					

				} else if (select == 11) {
					break;

				} else {
					Console.WriteLine("Nepoznata naredba \"{0}\"", input);
				}
				Console.WriteLine("\nPritnisni bilo koju tipku za nastavak . . .");
				Console.ReadLine();
				Console.Clear();

			}

			return (int)error.SUCCESS;
		}

		static int PrintList(Dictionary<int, string> playList)
		{
			if (playList.Count == 0) {
				Console.WriteLine("Playlista je prazna!");
				return (int)error.FAILURE;
			}

			Console.WriteLine("\n{0, -8} {1, -32}", "ID", "Song name");
			foreach (KeyValuePair<int, string> song in playList) {
				Console.WriteLine("{0, -8} {1, -32}", song.Key, song.Value);
			}

			return (int)error.SUCCESS;
		}

		static int PrintSong(Dictionary<int, string> playList, int songID)
		{
			var exits = false;
			foreach(KeyValuePair<int, string> song in playList) {
				if (song.Key == songID) {
					Console.WriteLine("\n{0, -8} {1, -32}", "ID", "Song name");
					Console.WriteLine("{0, -8} {1, -32}", song.Key, song.Value);
					exits = true;
					break;
				}
			}

			if (exits != true) {
				Console.WriteLine("Ne postoji pjesma s rednim brojem \"{0}\"", songID);
				return (int)error.FAILURE;
			}

			return (int)error.SUCCESS;
		}

		static int PrintSong(Dictionary<int, string> playList, string songName) {
			var exits = false;
			foreach (KeyValuePair<int, string> song in playList)
			{
				if (song.Value == songName)
				{
					Console.WriteLine("\n{0, -8} {1, -32}", "ID", "Song name");
					Console.WriteLine("{0, -8} {1, -32}", song.Key, song.Value);
					exits = true;
					break;
				}
			}

			if (exits != true) {
				Console.WriteLine("Ne postoji pjesma s imenom \"{0}\"", songName);
				return (int)error.FAILURE;
			}

			return (int)error.SUCCESS;

		}

		static int AddSong(Dictionary<int, string> playList, string songName)
		{
			var newSongID = playList.Count;
			newSongID++;

			foreach(KeyValuePair<int, string> song in playList)
				if (song.Value == songName) {
					Console.WriteLine("Pjesma s imenom \"{0}\" već postoji!", songName);
					return (int)error.FAILURE;
				}

			playList.Add(newSongID, songName);

			return (int)error.SUCCESS;
		}



	}
}














