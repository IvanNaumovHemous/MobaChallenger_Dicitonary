using System;
using System.Collections.Generic;
using System.Linq;

namespace MOBAChallenger
{
    class MobaChallenger
    {
        static void Main(string[] args)
        {
            var PlayerDictionary = GetPlayerDictionary();
            PrintDictionary(PlayerDictionary);           
            
        }

        private static void PrintDictionary(Dictionary<string, Dictionary<string, int>> playerDictionary)
        {
            foreach (var player in playerDictionary.OrderByDescending(x => x.Value.Values.Sum()).ThenBy(a => a.Key))
            {
                Console.WriteLine("{0}: {1} skill", player.Key, player.Value.Values.Sum());
                foreach (var role in player.Value.OrderByDescending(x => x.Value).ThenBy(p => p.Key))
                {
                    Console.WriteLine("- {0} <::> {1}", role.Key, role.Value);
                }
            }
        }

        private static Dictionary<string, Dictionary<string, int>> GetPlayerDictionary()
        {
            string data = Console.ReadLine();
            var PlayerSkillDictionary = new Dictionary<string, Dictionary<string, int>>();
            var points = 0;

            while (data != "Season end")
            {
                var input = data.Split(' ').ToList();
                if (input[1].Equals("vs"))
                {
                    var player = input[0];
                    var player2 = input[2];

                    if (!PlayerSkillDictionary.ContainsKey(player) || !PlayerSkillDictionary.ContainsKey(player2))
                    {
                        data = Console.ReadLine();
                        continue;
                    }
                    else if (PlayerSkillDictionary.ContainsKey(player) && PlayerSkillDictionary.ContainsKey(player2))
                    {
                        var tempList = PlayerSkillDictionary[player].Keys.Intersect(PlayerSkillDictionary[player2].Keys).ToList();
                        if (tempList.Count == 0)
                        {
                            data = Console.ReadLine();
                            continue;
                        }
                        else
                        {
                            if (PlayerSkillDictionary[player].Values.Sum() > PlayerSkillDictionary[player2].Values.Sum())
                            {
                                PlayerSkillDictionary.Remove(player2);
                            }
                            else if (PlayerSkillDictionary[player].Values.Sum() < PlayerSkillDictionary[player2].Values.Sum())
                            {
                                PlayerSkillDictionary.Remove(player);
                            }
                            else if (PlayerSkillDictionary[player].Values.Sum() == PlayerSkillDictionary[player2].Values.Sum())
                            {
                                data = Console.ReadLine();
                                continue;
                            }
                        }
                    }

                }
                else
                {
                    var player = input[0];
                    var role = input[2];
                    var skillPoints = int.Parse(input[4]);

                    if (!PlayerSkillDictionary.ContainsKey(player))
                    {
                        PlayerSkillDictionary[player] = new Dictionary<string, int>();
                        points += skillPoints;
                        PlayerSkillDictionary[player].Add(role, skillPoints);
                    }
                    else
                    {
                        PlayerSkillDictionary[player].Add(role, skillPoints);
                        points += skillPoints;
                    }

                }


                data = Console.ReadLine();
            }

            return PlayerSkillDictionary;
        }
    }
}
