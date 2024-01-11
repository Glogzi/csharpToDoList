using System.Text.RegularExpressions;

namespace toDoList {
    internal class Program {
        static string response(string[] msg) {
            int id = 0;
            try {
                string[] tempList = File.ReadAllLines("toDo.txt");
                for (int i = 0; i < tempList.Length; i++) {
                    id++;
                }
            }
            catch {
                File.Create("./toDo.txt");
                id = 1;
            }
            string[] allList = File.ReadAllLines("toDo.txt");
            string strID = Convert.ToString(id);
            switch (msg[0].ToLower()) {
                case "help":
                    return extraCommands.help();
                case "add":
                    return extraCommands.add(msg, strID);
                case "show":
                    return extraCommands.show(allList);
                default:
                    return "command not found";
            }
        }
        static void Main() {
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
        public static string add(string[] msg, string id) {
            foreach (string word in msg) {
                if (word != msg[0]) {
                    File.AppendAllText("toDo.txt", word + " ");
                    continue;
                }
                File.AppendAllText("toDo.txt", id + ". ");
            }

            File.AppendAllText("toDo.txt", "\n");
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
    }
}
