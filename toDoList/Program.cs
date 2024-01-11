using System.Diagnostics;
using System.Text.RegularExpressions;

namespace toDoList {
    internal class Program {
        static string response(string[] msg) {
            int id = 1;
            string[] allList = new string[1];
            allList = File.ReadAllLines("toDo.toDo");
            for (int i = 0; i < allList.Length; i++) {
                id++;
            }
            string strID = Convert.ToString(id);
            switch (msg[0].ToLower()) {
                case "help":
                    return extraCommands.help();
                case "add":
                    return extraCommands.add(msg, strID);
                case "show":
                    return extraCommands.show(allList);
                case "mark":
                    return extraCommands.mark(msg, allList, "mark");
                case "unmark":
                    return extraCommands.mark(msg, allList, "unmark");
                default:
                    return "command not found";
            }
        }
        static void Main() {
            extraCommands.create();
            Console.WriteLine("welcome to console toDo list");
            Console.WriteLine("write help to get help with commands");
            while (true){
                Console.Write(">");
                string usrInput = Console.ReadLine();
                string[] usrInputArray = usrInput.Split(' ');
                Console.WriteLine(response(usrInputArray));
            }
        }
    }
    public class extraCommands {
        public static void create() {
            if (!File.Exists("./toDo.toDo")){
                File.Create("./toDo.toDo");
                Process.Start("./toDoList.exe");
                Environment.Exit(0);
            }
            return;
        }
        public static string add(string[] msg, string id) {
            foreach (string word in msg) {
                if (word != msg[0]) {
                    File.AppendAllText("toDo.toDo", word + " ");
                    continue;
                }
                File.AppendAllText("toDo.toDo", id + ". ");
            }

            File.AppendAllText("toDo.toDo", "\n");
            return "added to an toDo list";
        }
        public static string help() {
            string[] help = File.ReadAllLines("help.txt");
            foreach (string line in help) {
                Console.WriteLine(line);
            }
            return "";
        }
        public static string show(string[] list) {
            foreach (string line in list) {
                Console.WriteLine(line);
            }
            return "";
        }
        public static string mark(string[] msg, string[] list, string mode) {
            string red = "\x1b[31m";
            string reset = "\x1b[0m";
            int line = Convert.ToInt32(msg[1]);
            if (mode == "mark") {
                list[line] = red + list[line] + reset;
            }
            File.WriteAllLines("./toDo.toDo", list);
            return $"marked line with id {line}";
        }
    }
}
