using System.Collections.Generic;

namespace StudentAPI.Common
{
    public static class CommonList
    {

        private static string[] CountryName = {"India", "Canada", "United States", "Switzerland" };
        public static List<Country> GetCountry()
        {
            List<Country> countries = new List<Country>();
            for (int i = 1; i < CountryName.Length; i++)
            {
                countries.Add(new Country {
                    Id = i,
                    Name = CountryName[i]
                });
            }
            return countries;
        }
    }
}
