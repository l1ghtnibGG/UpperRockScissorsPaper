namespace UpperRockPapperScissors
{
    internal class Logic
    {
        public void Start(string[] args)
        {
            var menu = new Menu();

            menu.CheckArgs(args);

            var key = new Key();
            var logic = new Logic();

            var getKey = key.GetKey();
            var getComputerMotion = logic.ComputerMotion(args);

            var hmac = new Hmac(getKey, getComputerMotion);

            Console.WriteLine("HMAC: " + hmac.ComputeHash());

            menu.ShowMenu(args);

            var userMotion = menu.UserInput(args);

            Console.WriteLine("Your move: " + userMotion);
            Console.WriteLine("Computer move: " + getComputerMotion);

            Console.WriteLine("You " + logic.CheckResult(userMotion, getComputerMotion, args));
            Console.WriteLine("HMAC Key: " + key.GetKey());
        }

        private string ComputerMotion(string[] args)
        {
            var computerElement = new Random().Next(args.Length);

            return args[computerElement];
        }

        private string CheckResult(string userMotion, string computerMotion, string[] args)
        {
            var ruleMatrix = FillRuleMatrix(args.Length);

            var indexUserMotion = Array.IndexOf(args, userMotion);
            var indexComputerMotion = Array.IndexOf(args, computerMotion);
            
            var indexResult = ruleMatrix[indexUserMotion, indexComputerMotion];

            return Result(indexResult);
        }

        public int[,] FillRuleMatrix(int length)
        {
            var ruleMatrix = new int[length, length];
            var rule = FillRule(length);

            for (var i = 0; i < length; i++)
            {
                if (i != 0)
                {
                    rule = RightShift(rule);
                }

                for (var j = 0; j < length; j++)
                {
                    ruleMatrix[i, j] = rule[j];
                }
            }

            return ruleMatrix;
        }

        private int[] FillRule(int length)
        {
            var rule = new int[length];
            rule[0] = 0;

            for (var i = 1; i < length; i++)
            {
                if(i <= length / 2)
                {
                    rule[i] = 1;
                }
                else
                {
                    rule[i] = -1;
                }
            }

            return rule;
        }

        private int[] RightShift(int[] rule)
        {
            var newRule = new int[rule.Length];

            Array.Copy(rule, 0, newRule, 1, rule.Length - 1);
            newRule[0] = rule[^1];

            return newRule;
        }

        public string Result(int index) => index == 1 ? "Win" : index == 0 ? "Draw" : "Loss";
    }
}
