using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{


    public class Class1
    {
        public void Resolve()
        {
            var inputFilePath = "input.txt";

            var input = File.ReadAllLines(inputFilePath);

            

            long sum = 0;
            
            foreach (var line in input)
            {
                sum += SnafuToDecimal(line);
            }

            string snafu = DecimalToSnafu(sum);

            Console.WriteLine(snafu);

        }

        private string DecimalToSnafu(long number)
        {
            string snafuNumber = string.Empty;

            while (number > 0)
            {
                long remainder = number % 5;
                switch (remainder)
                {
                    case 0:
                        snafuNumber = "0" + snafuNumber;
                        break;
                    case 1:
                        snafuNumber = "1" + snafuNumber;
                        break;
                    case 2:
                        snafuNumber = "2" + snafuNumber;
                        break;
                    case 3:
                        snafuNumber = "=" + snafuNumber;
                        number += 2;
                        break;
                    case 4:
                        snafuNumber = "-" + snafuNumber;
                        number += 1;
                        break;
                }
                number /= 5;
            }

            return snafuNumber;

        }

        private long SnafuToDecimal(string snafu)
        {
            long number = 0;
            for (int i = 0; i < snafu.Length; i++)
            {
                number += SnafuDigitToDecimal(snafu[i], snafu.Length - i - 1);
            }

            return number;
        }

        private long SnafuDigitToDecimal(char character, int index)
        {
            long digitValue = (long)Math.Pow(5, index);

            switch (character)
            {
                case '0':
                    return 0;
                case '1':
                    return digitValue;
                case '2':
                    return digitValue * 2;
                case '-':
                    return digitValue * -1;
                case '=':
                    return digitValue * -2;
            }
            return 0;
        }
    }
}