using user_monitoring_gui.Services.Interfaces;
using user_monitoring_gui.Models;

namespace user_monitoring.Services
{
    /// @brief Class representing the console user interface for the application.
    ///
    /// This class implements the IUserInterface interface and provides methods
    /// to display menus and handle user interactions in a console application.
    public class ConsoleUserInterface : IUserInterface
    {
        private WordBanList _wordBanList;
        private Setting _setting;
        /// @brief Initializes a new instance of the ConsoleUser Interface class.
        /// @param setting The settings to be used by the interface.
        /// @param wordBanList The list of banned words to be managed.

        public ConsoleUserInterface(Setting setting, WordBanList wordBanList)
        {
            this._setting = setting;
            this._wordBanList = wordBanList;
        }

        /// @brief Displays the main menu and handles user selection.
        ///
        /// This method continuously displays the main menu until the user selects
        /// the exit option. It processes user input and calls the appropriate
        /// methods based on the selected menu item.
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
        /// @brief Displays the settings menu and allows the user to modify settings.
        ///
        /// This method displays the current settings and allows the user to toggle
        /// each setting on or off. The menu continues to display until the user
        /// selects the option to go back.
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
        /// @brief Displays the menu for managing the banned words list.
        ///
        /// This method displays options for adding or removing words from the banned
        /// words list. The menu continues to display until the user selects the
        /// option to return to the main menu.
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

        /// @brief Prompts the user to add a word to the banned list.
        ///
        /// This method reads a word from the user and adds it to the banned words list.
        public void PrintAddWordToBanList()
        {
            Console.WriteLine("Введите слово, которое нужно добавить в список запрещенных:");

            string word = Console.ReadLine();
            _wordBanList.AddWord(word);
        }
        /// @brief Prompts the user to remove a word from the banned list.
        ///
        /// This method reads a word from the user and removes it from the banned words list.
        public void PrintRemoveWordFromBanList()
        {
            Console.WriteLine("Введите слово, которое нужно удалить из списка запрещенных:");

            string word = Console.ReadLine();
            _wordBanList.RemoveWord(word);
        }
        /// @brief Displays the current banned words list.
        ///
        /// This method retrieves and displays the list of banned words. If the list
        /// is empty, it informs the user.
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

        /// @brief Displays the menu for managing the banned programs list.
        ///
        /// This method is currently not implemented.
        public void PrintMenuProgramBanList()
        {

        }
        /// @brief Executes the main program functionality.
        ///
        /// This method is currently not implemented.
        public void RunProgram()
        {

        }
        /// @brief Displays the statistics menu.
        ///
        /// This method is currently not implemented.
        public void PrintMenuStatistic()
        {

        }
    }
}