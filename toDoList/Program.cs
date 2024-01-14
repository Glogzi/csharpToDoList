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
                case "del":
                    return extraCommands.del(allList, msg);
                case "edit":
                    return extraCommands.edit(msg, allList);
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
        static string[] reloadId(string[] list) {
            int id = 1;
            for (int line = 0; line < list.Length; line++) {
                string[] spltedLine = list[line].Split(". ");
                spltedLine[0] = Convert.ToString(id);
                id++;
                list[line] = String.Join(". ", spltedLine);
            }
            return list;
        }
        public static string edit(string[] msg, string[] list) {
            int id;
            try {
                id = Convert.ToInt32(msg[1]) - 1;
            }
            catch {
                return "incorrect id";
            }
            string[] editToWhatArr = new string[msg.Length-1];
            editToWhatArr[0] = $"{msg[1]}.";
            for (int i = 1; i < editToWhatArr.Length; i++) {
                editToWhatArr[i] = msg[i + 1]; 
            }
            string editToWhat = String.Join(" ", editToWhatArr);
            try {
                for (int line = 1; line <= list.Length; line++) {
                    if (id == line) {
                        list[line] = editToWhat;
                        File.WriteAllLines("toDo.toDo", list);
                        return $"line with id {msg[1]} edited";
                    }
                }
                return $"line with id {msg[1]} not found";
            }
            catch {
                return $"line with id {msg[1]} not found";
            }
        }
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
            try {
                list[line - 1] = red + list[line-1] + reset;
                File.WriteAllLines("./toDo.toDo", list);
                return $"marked line with id {line}";
            }
            catch {
                return "wrong input";
            }
        }
        public static string delAll(string[] list) {
            File.WriteAllLines("./toDo.toDo", []);
            return "deleted all lines";
        }
        public static string del(string[] list, string[] msg) {
            int id;
            try {
                id = Convert.ToInt32(msg[1]);
            }
            catch {
                return "wrong input";
            }
            
            bool wasOneToDelete = false;
            string[] newList = new string[list.Length-1];
            try {
                for (int line = 0; line < list.Length; line++) {
                    if (line != id - 1) {
                        if (wasOneToDelete) {
                            newList[line - 1] = list[line];
                            continue;
                        }
                        newList[line] = list[line];
                    }
                    else {
                        wasOneToDelete = true;
                    }
                }
                //changing id at the start of a lin
                //e
                newList = reloadId(newList);
                File.WriteAllLines("toDo.toDo", newList);
                return $"line with id: {id} was deleted";
            }
            catch {
                return "wrong input";
            }
        }
    }
}
