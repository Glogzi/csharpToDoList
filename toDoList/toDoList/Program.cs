using System.Text.RegularExpressions;

namespace toDoList {
    internal class Program {
        static void response(string[] msg) {
            string[] help = File.ReadAllLines("help.txt");
            string id = "0";
            switch (msg[0].ToLower()) {
                case "help":
                    foreach (string line in help) {
                        Console.WriteLine(line);
                    }
                    return;
                case "add":
                    try {
                        foreach (string word in msg) {
                            if (word != msg[0]) {
                                File.AppendAllText("Things.txt", word+" ");
                                continue;
                            }
                            File.AppendAllText("Things.txt", id+" ");
                        }
                        File.AppendAllText("Things.txt", "\n");
                    }
                    catch {
                        File.Create("./Things.txt");
                    }
                    return;
            }
        }
        static void Main(string[] args) {
            Console.WriteLine("welcome to console toDo list");
            Console.Write("write help to get help with commands\n>");
            while (true){
                string usrInput = Console.ReadLine();
                string[] usrInputArray = usrInput.Split(' ');
                response(usrInputArray);
            }
        }
    }
}
