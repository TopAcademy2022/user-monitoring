using user_monitoring_gui.Services.Interfaces;
using user_monitoring_gui.Models;

namespace user_monitoring.Services
{
	public class ConsoleUserInterface : IUserInterface
	{
		private WordBanList _wordBanList;
		private Setting _setting;
		private ProgramBanList _programBanList;

		public ConsoleUserInterface(Setting setting, WordBanList wordBanList, ProgramBanList programBanList)
		{
			this._setting = setting;
			this._wordBanList = wordBanList;
			this._programBanList = programBanList;
		}

		public void PrintMenu()
		{

			const uint NUMBER_EXIT_MENU_ELEMENT = 6;
			int numberMenuElement = 0;

			while (numberMenuElement != NUMBER_EXIT_MENU_ELEMENT)
			{
				Console.Clear();
				Console.WriteLine("***********************************************");
				Console.WriteLine("***********************************************");
				Console.WriteLine("\t====== Меню ======\n");
				Console.WriteLine("1. Настроить");
				Console.WriteLine("2. Редактировать список запрещённых слов");
				Console.WriteLine("3. Редактировать список запрещённых программ");
				Console.WriteLine("4. Запустить");
				Console.WriteLine("5. Показать статистику");
				Console.WriteLine("6. Выход\n");
				Console.WriteLine("***********************************************");

				Console.Write("Выберите пункт меню: ");
				ConsoleKeyInfo userInputSymbol;

				do
				{
					userInputSymbol = Console.ReadKey(true);
				} while (userInputSymbol.Key < ConsoleKey.D0 || userInputSymbol.Key > ConsoleKey.D6);

				numberMenuElement = Convert.ToInt32(userInputSymbol.KeyChar.ToString());

				switch (numberMenuElement)
				{
					case 1:
						PrintMenuSettings();
						break;

					case 2:
						PrintMenuWordBanList();
						break;

					case 3:
						PrintMenuProgramBanList();
						break;

					case 4:
						RunProgram();
						break;

					case 5:
						PrintMenuStatistic();
						break;
				}
			}
		}

		public void PrintMenuSettings()
		{
			int numberMenuElement = 0;
			const uint NUMBER_EXIT_MENU_ELEMENT = 5;
			ConsoleKeyInfo userInputSymbol;
			while (numberMenuElement != NUMBER_EXIT_MENU_ELEMENT)
			{
				Console.Clear();
				Console.OutputEncoding = System.Text.Encoding.Unicode;

				const char SYMBOL_СHECMARK = '\u2705';
				const char SYMBOL_СROSS = '\u274E';
				char OnСonditions;
				Console.WriteLine("\t\t\tНастройки:\n");

				Console.Write("1. Считывать нажатия клавиш. ");
				OnСonditions = (this._setting.ReadKeyboardInput == true) ? SYMBOL_СHECMARK : SYMBOL_СROSS;
				Console.WriteLine(OnСonditions);

				Console.Write("2. Считывать список запущенных приложений. ");
				OnСonditions = (this._setting.ReadRunPrograms == true) ? SYMBOL_СHECMARK : SYMBOL_СROSS;
				Console.WriteLine(OnСonditions);

				Console.Write("3. Анализ запрещённых слов. ");
				OnСonditions = (this._setting.AnalysisProhibitedWords == true) ? SYMBOL_СHECMARK : SYMBOL_СROSS;
				Console.WriteLine(OnСonditions);

				Console.Write("4. Анализ запрещённых программ. ");
				OnСonditions = (this._setting.AnalysisProhibitedPrograms == true) ? SYMBOL_СHECMARK : SYMBOL_СROSS;
				Console.WriteLine(OnСonditions);
				Console.WriteLine("5.Назад. ");

				do
				{
					userInputSymbol = Console.ReadKey(true);
				}
				while (userInputSymbol.Key < ConsoleKey.D0 || userInputSymbol.Key > ConsoleKey.D5);

				numberMenuElement = Convert.ToInt32(userInputSymbol.KeyChar.ToString());

				switch (numberMenuElement)
				{
					case 1:
						this._setting.ReadKeyboardInput ^= true;
						break;
					case 2:
						this._setting.ReadRunPrograms ^= true;
						break;
					case 3:
						this._setting.AnalysisProhibitedWords ^= true;
						break;
					case 4:
						this._setting.AnalysisProhibitedPrograms ^= true;
						break;
				}

			}
		}

		public void PrintMenuWordBanList()
		{

			const uint NUMBER_EXIT_MENU_ELEMENT = 4;
			int numberMenuElement = 0;

			while (numberMenuElement != NUMBER_EXIT_MENU_ELEMENT)
			{
				Console.Clear();
				Console.WriteLine("***********************************************");
				Console.WriteLine("***********************************************");
				Console.WriteLine("\tМеню списка запрещенных слов\n");
				Console.WriteLine("1. Добавить слово в список запрещенных");
				Console.WriteLine("2. Удалить слово из списка запрещенных");
				Console.WriteLine("3. Вернуться в главное меню");
				Console.WriteLine("***********************************************");

				Console.Write("Выберите пункт меню: ");
				ConsoleKeyInfo userInputSymbol;

				do
				{
					userInputSymbol = Console.ReadKey(true);
				} while (userInputSymbol.Key < ConsoleKey.D0 || userInputSymbol.Key > ConsoleKey.D3);

				numberMenuElement = Convert.ToInt32(userInputSymbol.KeyChar.ToString());

				switch (numberMenuElement)
				{
					case 1:
						PrintAddWordToBanList();
						break;

					case 2:
						PrintRemoveWordFromBanList();
						break;

					case 3:
						PrintMenu();
						break;
				}
			}
		}

		public void PrintAddWordToBanList()
		{
			Console.WriteLine("Введите слово, которое нужно добавить в список запрещенных:");

			string word = Console.ReadLine();
			_wordBanList.AddWord(word);
		}

		public void PrintRemoveWordFromBanList()
		{
			Console.WriteLine("Введите слово, которое нужно удалить из списка запрещенных:");

			string word = Console.ReadLine();
			_wordBanList.RemoveWord(word);
		}
		public void PrintBanList()
		{
			List<string> banList = _wordBanList.GetWordBanList();

			if (banList.Count == 0)
			{
				Console.WriteLine("\nСписок запрещенных слов пуст.\n");
			}
			else
			{
				Console.WriteLine("\nСписок запрещенных слов:");

				foreach (string word in banList)
				{
					Console.WriteLine(word);
				}
			}

			Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
			Console.ReadKey(true);
		}

		public void PrintMenuProgramBanList()
		{
			const uint NUMBER_EXIT_MENU_ELEMENT = 5;
			int numberMenuElement = 0;

			while (numberMenuElement != NUMBER_EXIT_MENU_ELEMENT)
			{
				Console.Clear();
				Console.WriteLine("***********************************************");
				Console.WriteLine("***********************************************");
				Console.WriteLine("\tМеню списка запрещенных программ\n");
				Console.WriteLine("1. Добавить программу в список запрещенных");
				Console.WriteLine("2. Удалить программу из списка запрещенных");
				Console.WriteLine("3. Показать список запрещенных программ");
				Console.WriteLine("4. Вернуться в главное меню");
				Console.WriteLine("***********************************************");

				Console.Write("Выберите пункт меню: ");
				ConsoleKeyInfo userInputSymbol;

				do
				{
					userInputSymbol = Console.ReadKey(true);
				} while (userInputSymbol.Key < ConsoleKey.D0 || userInputSymbol.Key > ConsoleKey.D4);

				Console.Clear();

				numberMenuElement = Convert.ToInt32(userInputSymbol.KeyChar.ToString());

				switch (numberMenuElement)
				{
					case 1:
						AddProgramToBanList();
						break;

					case 2:
						RemoveProgramFromBanList();
						break;

					case 3:
						PrintAllProgramsFromBanList();
						break;

					case 4:
						PrintMenu();
						break;
				}
			}
		}

		public void AddProgramToBanList()
		{
			Console.WriteLine("Введите назавание программы, которое нужно добавить в список запрещенных:");

			string program = Console.ReadLine();
			_programBanList.AddProgram(program);
		}

		public void RemoveProgramFromBanList()
		{
			Console.WriteLine("Введите название программы, которое нужно удалить из списка запрещенных:");

			string program = Console.ReadLine();
			_programBanList.RemoveProgram(program);
		}

		public void PrintAllProgramsFromBanList()
		{
			List<string> allPrograms = _programBanList.GetProgramBanList();
			uint countPrograms = 0;

			Console.WriteLine("All programs from the banlist");

			foreach (string program in allPrograms)
			{
				++countPrograms;
				Console.WriteLine($"{countPrograms}: {program}");
			}

			Console.WriteLine("\nPress any Key to continue...");
			Console.ReadKey();
		}

		public void RunProgram()
		{

		}

		public void PrintMenuStatistic()
		{

		}
	}
}