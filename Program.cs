using System.Text.RegularExpressions;

namespace toDoList {
    internal class Program {
        static string response(string[] msg) {
            string[] help = File.ReadAllLines("help.txt");
            int id = 0;
            try {
                string[] allList = File.ReadAllLines("toDo.txt");
                for (int i = 0; i < allList.Length; i++) {
                    id++;
                }
            }
            catch {
                File.Create("./toDo.txt");
                id = 1;
            }
            string strID = Convert.ToString(id);
            switch (msg[0].ToLower()) {
                case "help":
                    foreach (string line in help) {
                        Console.WriteLine(line);
                    }
                    return "";
                case "add":
                    foreach (string word in msg) {
                        if (word != msg[0]) {
                            File.AppendAllText("toDo.txt", word+" ");
                            continue;
                        }
                        File.AppendAllText("toDo.txt", strID+" ");
                    }

                    File.AppendAllText("toDo.txt", "\n");
                    return "added to an toDo list";
                
                default:
                    return "command not found";
            }
        }
        static void Main(string[] args) {
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
}
