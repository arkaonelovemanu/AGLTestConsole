using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AGLTestConsole;
using System.Collections.Generic;

namespace UnitTestAGLTestConsole
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetResponseFromWebService()
        {
            Program.GetResponseFromWebService();
        }

        [TestMethod]
        public void TestDoMainOperation()
        {
            Program.DoMainOperation();
        }

        [TestMethod]
        public void TestGetAnimalsByGenderOfOwner()
        {
            List<Persons> listOfPersons = new List<Persons>();

            Persons personA = new Persons();
            personA.Age = 17;
            //personA.Name = "abcd";
            //personA.Gender = "female";
            Pets abcdPetCat = new Pets();
            abcdPetCat.Name = "garfield";
            abcdPetCat.Type = "cat";
           // personA.Pets = new Lis
            personA.Pets.Add(abcdPetCat);
            listOfPersons.Add(personA);

            Persons personB = new Persons();
            personB.Age = 25;
            personB.Name = "vbnm";
            personB.Gender = "male";
            Pets vbnmPet = new Pets();
            vbnmPet.Name = "fluffy";
            vbnmPet.Type = "cat";
            personB.Pets.Add(vbnmPet);
            listOfPersons.Add(personB);


            Program.GetAnimalsByGenderOfOwner(listOfPersons,"cat");
        }

      
    }
}
