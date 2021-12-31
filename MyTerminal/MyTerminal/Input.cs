namespace MyTerminal
{
    public class Input
    {
        public string Command { get; set; }
        public string FirstArgument { get; set; }
        public string SecondArgument { get; set; }
        public string[] Flags { get; set; }

        public Input()
        {
            Command = "ls";
            FirstArgument = null;
            SecondArgument = null;
            Flags = null;
        }

        public Input(string command)
        {
            Command = command;
            FirstArgument = null;
            SecondArgument = null;
            Flags = null;
        }
        
        public Input(string command, string firstArgument)
            :this(command)
        {
            FirstArgument = firstArgument;
            SecondArgument = null;
            Flags = null;
        }
        
        public Input(string command, string firstArgument, string secondArgument)
            :this(command, firstArgument)
        {
            SecondArgument = secondArgument;
            Flags = null;
        }

        public Input(string command, string[] flags)
            :this(command)
        {
            FirstArgument = null;
            SecondArgument = null;
            Flags = flags;
        }

        public Input(string command, string firstArgument, string secondArgument, string[] flags)
        {
            Command = command;
            FirstArgument = firstArgument;
            SecondArgument = secondArgument;
            Flags = flags;
        }
    }
}