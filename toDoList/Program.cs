using System.Diagnostics;

namespace toDoList {
    internal class Program {
        static string response(string[] msg) {
            int id = 1;
            string[] allList = File.ReadAllLines("toDo.toDo");
            for (int i = 1; i <= allList.Length; i++) {
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
                    return extraCommands.mark(msg, allList);
                case "unmark":
                    return extraCommands.mark(msg, allList, "unmark");
                case "delall":
                    return extraCommands.delAll(allList);
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
            //i need to reload program, after creating file
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
        public static string mark(string[] msg, string[] list, string mode = "mark") {
            string red = "\x1b[31m ";
            string reset = " \x1b[0m";
            int line;
            try {
                line = Convert.ToInt32(msg[1]);
            }
            catch {
                return "wrong input";
            }
            
            if (mode == "unmark") {
                try {
                    string[] splitedLine = list[line-1].Split(" ");
                    if (splitedLine[0] != "\x1b[31m") { return "there's nothing to unmark"; }
                    splitedLine[0] = "";
                    splitedLine[splitedLine.Length - 1] = "";
                    list[line - 1] = String.Join(" ", splitedLine);
                    list[line - 1] = list[line - 1].Trim();
                    File.WriteAllLines("./toDo.toDo", list);
                    return $"unmarked line with id {line}";
                }
                catch {
                    return "there's nothing to unmark";
                }
            }
            list[line - 1] = red + list[line-1] + reset;
            File.WriteAllLines("./toDo.toDo", list);
            return $"marked line with id {line}";
        }
        public static string delAll(string[] list) {
            File.WriteAllLines("./toDo.toDo", []);
            return "deleted all lines";
        }
    }
}
