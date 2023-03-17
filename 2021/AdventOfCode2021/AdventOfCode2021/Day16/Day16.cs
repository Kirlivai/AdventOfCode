using AdventOfCode2021.D16;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Day16 : IDay
    {
        private string[] _input;
        private List<bool> _bitArray = new();
        private List<Packet> _packets = new();

        public Day16()
        {
            _input = File.ReadAllLines(@"Inputs/Day16Input.txt");
            string hexInput = _input[0];

            foreach (char c in hexInput)
            {
                int value = Convert.ToInt32(c.ToString(), 16);
                _bitArray.AddRange(Convert.ToString(value, 2).PadLeft(4, '0').Select(x => x == '1'));
            }

            Parse();
        }

        private void Parse()
        {
            int index = 0;
            int minPacketSize = 11;

            for (; index < _bitArray.Count; index++)
            {
                _packets.Add(ParsePacket(ref index));

                if (_bitArray.Count - index < minPacketSize)
                {
                    break;
                }
            }
        }

        public void RunPart1()
        {
            Console.WriteLine($"Part 1: {GetPacketVersion(_packets[0])}");
        }

        public void RunPart2()
        {
            Console.WriteLine($"Part 2: {GetPacketValue(_packets[0])}");
        }



        private long GetPacketVersion(Packet packet)
        {
            long sum = packet.Version;

            foreach (var sub in packet.Subs)
            {
                sum += GetPacketVersion(sub);
            }

            return sum;
        }

        private long GetPacketValue(Packet packet)
        {
            switch (packet.Id)
            {
                case 0:
                    long sum = 0;
                    foreach (var sub in packet.Subs)
                    {
                        sum += GetPacketValue(sub);
                    }
                    return sum;
                case 1:
                    long product = 1;
                    foreach (var sub in packet.Subs)
                    {
                        product *= GetPacketValue(sub);
                    }
                    return product;
                case 2:
                    long min = long.MaxValue;
                    foreach (var sub in packet.Subs)
                    {
                        min = Math.Min(min, GetPacketValue(sub));
                    }
                    return min;
                case 3:
                    long max = long.MinValue;
                    foreach (var sub in packet.Subs)
                    {
                        max = Math.Max(max, GetPacketValue(sub));
                    }
                    return max;
                case 4:
                    return packet.LiteralValue;
                default:
                    long packet1 = GetPacketValue(packet.Subs[0]);
                    long packet2 = GetPacketValue(packet.Subs[1]);

                    if (packet.Id == 5) return packet1 > packet2 ? 1 : 0;
                    else if (packet.Id == 6) return packet1 < packet2 ? 1 : 0;
                    else return packet1 == packet2 ? 1 : 0;

            }
        }


        private Packet ParsePacket(ref int index)
        {
            Packet packet = new();

            packet.Version = BitListToDecimal(_bitArray.Skip(index).Take(3).ToList());
            index += 3;

            packet.Id = BitListToDecimal(_bitArray.Skip(index).Take(3).ToList());
            index += 3;

            if (packet.Id == 4)
            {
                ParseLiteralValuePacket(packet, ref index);
            }
            else
            {
                long lenghtTypeId = BitListToDecimal(_bitArray.Skip(index).Take(1).ToList());
                index += 1;

                if (lenghtTypeId == 0)
                {
                    long lenght = BitListToDecimal(_bitArray.Skip(index).Take(15).ToList());
                    index += 15;

                    long maxIndex = lenght + index;


                    while (index < maxIndex)
                    {
                        packet.Subs.Add(ParsePacket(ref index));
                    }
                }
                else
                {
                    long lenght = BitListToDecimal(_bitArray.Skip(index).Take(11).ToList());
                    index += 11;

                    for (int i = 0; i < lenght; i++)
                    {
                        packet.Subs.Add(ParsePacket(ref index));
                    }
                }
            }
            return packet;
        }

        private void ParseLiteralValuePacket(Packet packet, ref int index)
        {
            List<bool> numberBits = new();

            while (index < _bitArray.Count)
            {
                var value = _bitArray.Skip(index).Take(5).ToList();
                numberBits.AddRange(_bitArray.Skip(index + 1).Take(4).ToList());
                index += 5;

                if (!value[0]) break;

            }
            packet.LiteralValue = BitListToDecimal(numberBits);
        }

        private static long BitListToDecimal(List<bool> bitList)
        {
            long decimalValue = 0;
            for (int i = 0; i < bitList.Count; i++)
            {
                if (bitList[i])
                {
                    decimalValue += (long)Math.Pow(2, bitList.Count - 1 - i);
                }
            }
            return decimalValue;
        }
    }
}
