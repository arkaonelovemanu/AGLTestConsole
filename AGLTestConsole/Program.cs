using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AGLTestConsole
{
     public class Program
    {
        private const string urlTestWebService = "http://agl-developer-test.azurewebsites.net/people.json";
        private const string animalToSelect = "cat";
        private const string strExitMsg = "Please press ENTER key to exit";
        static void Main(string[] args)
        {
            DoMainOperation();

        }

        public static void DoMainOperation()
        {
            ResponseData response = GetResponseFromWebService();
            if (response.Success)
            {
                //get list of persons 
                List<Persons> persons = JsonConvert.DeserializeObject<List<Persons>>(response.Result);

                GetAnimalsByGenderOfOwner(persons, animalToSelect);

            }
        }

        public static ResponseData GetResponseFromWebService()
        {
            ResponseData response = GetWebServiceData(urlTestWebService).Result;
            return response;
        }

        public static void GetAnimalsByGenderOfOwner(List<Persons> persons, string animal)
        {
            string genderFemale = "Female";
            string genderMale = "Male";

            //categorize into two lists- one for females, one for males
            List<string> maleCats = GetAnimalNamesByGender(genderMale, animal, persons);
            PrintAnimalsByGenderOfOwner(maleCats, genderMale);

            List<string> femaleCats = GetAnimalNamesByGender(genderFemale, animal, persons);
            PrintAnimalsByGenderOfOwner(femaleCats, genderFemale);

            PrintExit(strExitMsg);
        
        }

        public static void PrintAnimalsByGenderOfOwner(List<string> animalNames ,string header)
        {
            try
            {
                Console.WriteLine(header + "\n");

                foreach (string curName in animalNames)
                {
                    Console.WriteLine(curName);
                }

                Console.WriteLine("\n\n");
            }

            catch (Exception ex)
            {

            }


        }

        public static void PrintExit(string exitmessage)
        {

            try
            {
                Console.WriteLine("Press any key to exit...");
                Console.Read();
            }

            catch(Exception ex)
            {

            }
        }

        private static List<string> GetAnimalNamesByGender(string gender,string type, List<Persons> persons)
        {
            List<string> animalNames = new List<string>();
            try
            {
                animalNames= persons.Where(p => p.Gender != null && p.Name != null && p.Gender.ToLower().Equals(gender.ToLower()) && p.Pets != null)
                                            .SelectMany(x => x.Pets)
                                            .Where(y => y.Type != null && y.Name != null && y.Type.ToLower().Equals(type.ToLower()))
                                            .OrderBy(t => t.Name)
                                            .Select(z => z.Name)
                                            .ToList();
            }

            catch(Exception ex)
            {

            }

            return animalNames;
        }

        private static  async Task<ResponseData> GetWebServiceData(string url)
        {
            ResponseData response = new ResponseData();
            using (HttpClient client = new System.Net.Http.HttpClient())
            {
                using (HttpResponseMessage responseGet = await client.GetAsync(url))
                {
                    try
                    {
                        response.Result = await responseGet.Content.ReadAsStringAsync();
                        response.Success = true;
                    }

                    catch(HttpRequestException ex)
                    {
                        response.Result = "An exception has occured while geting the data.";
                        response.Success = false;
                    }
                }
            }
            return response;

        }
    }

    public class Persons
    {
        private string name;
        private string gender;
        private int age;
        private List<Pets> pets;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        public List<Pets> Pets
        {
            get
            {
                return pets;
            }
            set
            {
                pets = value;
            }
        }

        public Persons()
        {
            Pets = new List<Pets>();
        }
    }

    public class Pets
    {
        private string name;
        private string type;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public  Pets (string name, string type)
        {
            Name = name;
            Type = type;
        }

        public Pets()
        { }
    }

    public class ResponseData
    {
        private bool success;
        private string result;

        public bool Success
        {
            get
            {
                return success;
            }
            set
            {
                success = value;
            }
        }

        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
            }
        }
    }
  
}
