using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace EscapeMines.Helpers
{
    public static class Util
    {
        /// <summary>
        /// Parses a string containing coordinates into a tuple of two integers
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static (int, int) ParseSetupLineInPosition(string line)
        {
            var t = line.Trim().Split(' ');
            return (int.Parse(t[0]), int.Parse(t[1]));
        }

        /// <summary>
        /// Parses a string containing a list of mines coordinates into a list of tuples of two integers
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static List<(int, int)> GetMines(string line)
        {
            var positions = line.Trim().Split(' ');
            var mineList = new List<(int, int)>();
            foreach (string position in positions)
            {
                var pos = position.Split(',');
                mineList.Add((int.Parse(pos[0]), int.Parse(pos[1])));
            }
            return mineList;
        }

        /// <summary>
        /// Gets the string value of a description attribute of an enum
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDescription(System.Enum enumValue)
        {
            var enumMember = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
            var descriptionAttrbs = enumMember.GetCustomAttributes(typeof(DescriptionAttribute), true);
            return ((DescriptionAttribute)descriptionAttrbs[0]).Description;
        }
    }
}