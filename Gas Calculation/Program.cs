/*A. Seun Ajayi
 * aajayi@cnm.edu
 * Program.cs
 * 06/08/2022
 * 
 */

using System;

namespace AjayiIdealGasLawCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create arrays
            string[] gasName = new string[0];
            double[] molecularWeights = new double[1];
            int count;
                 
            DisplayHeader();

            GetMolecularWeights(ref gasName, ref molecularWeights, out count);

            DisplayGasNames(gasName, count);

            string input;
            double moleWeight;
            bool test = true;

            do
            {
                string choice = "yes";

                while (choice == "yes") //Runs if value is yes
                 {
                    //Welcom messgage to start calculation of pressure
                    Console.WriteLine("\n\nWelcome, Let\'s Calculate the Pressure!");
                    Console.Write("Enter the name of the gas:");
                    input = Console.ReadLine();

                    moleWeight = GetMolecularWeightFromName(input, gasName, molecularWeights, count);

                    //test if gas name is in list
                    if (moleWeight == 0)
                    {
                        Console.WriteLine("\nGas cannot be found, Goodbye.");
                        test = false;
                        break;
                    }

                    double pressure, vol, mass, temp;

                    Console.WriteLine("Enter volume of the gas container (in cubic meters):");
                    vol = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the weight of the gas (in gram):");
                    mass = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter the temperature of the gas (in degrees Celsius):");
                    temp = Convert.ToDouble(Console.ReadLine());

                    pressure = Pressure(mass,vol,temp,moleWeight);

                    DisplayPresure(pressure);

                    Console.WriteLine("\nDo you want to continue? (enter yes or no):");
                    choice = Console.ReadLine();

                }

                test = false; //sets value to false to end loop

                Console.WriteLine("\nThank you for playing!");

            } while (test);

        }

        static void DisplayHeader()
        {
            Console.WriteLine("A. Seun Ajayi");
            Console.WriteLine("Program.cs");
            Console.WriteLine("To use the Ideal Gas Equation to build a program that will calculate the pressure exerted by a gas in a container given the following inputs from the user.");
           
        }

     
        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {
            //set value
            count = 0;

            foreach (string line in System.IO.File.ReadLines("MolecularWeightsGasesAndVapors.csv"))
            {
                  count++;
            }

           //Subtract the header row from count
           count--;

            gasNames = new string[count];
            molecularWeights = new double[count];

            //Read through the file and assigns value to arrays
            int gasIndex = 0;
            bool isHeader = true;

            foreach(string line in System.IO.File.ReadLines("MolecularWeightsGasesAndVapors.csv"))
            {
                if(isHeader)
                {
                    isHeader = false;
                    continue;
                }

                string[] gasLine = line.Split(',');

                gasNames[gasIndex] = gasLine[0];
                molecularWeights[gasIndex] = Convert.ToDouble(gasLine[1]);

                gasIndex++;

            }
        }
        private static double GetMolecularWeightFromName(string gasName, string[] gasNames, double[] molecularWeights, int countGases)
        {
            //Looks up the gas in the array, and returns the molecular weight of that gas if found.
            
            double weightAmount = 0;
            bool test = false;

            for(int i = 0; i < countGases; i++)
            {
                test = String.Equals(gasName, gasNames[i]);

                //If gasName found break loop
                if (test) 
                {
                    weightAmount = molecularWeights[i];
                    break;
                }
            }

           //If Item was not found return 0
           if (test == false)
            {
                return 0;
            }
           
                return weightAmount;
        }

        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            //Given an array of gas names display gas names in 3 columns with left align
            Console.WriteLine("\nGas Names:");
            
            for (int i = 1; i <= countGases; i++)
            {
                //When index is divisible by 3, print gas names
                if (i % 3 == 0)
                {
                    Console.WriteLine($"{gasNames[i - 3],-20}   {gasNames[i - 2],-20}  {gasNames[i - 1],-20}");

                }
            }
        }

        static double Pressure(double mass, double vol, double temp, double molecularWeight)
        {
           //Returns pressure of a gas in pascals

            const double R = 8.3145; 
            double resultP, resultTemp, n;

            n = NumberOfMoles(mass, molecularWeight);

            resultTemp = CelciusToKelvin(temp);

            //Calculates Pressure
            resultP = (n*R*resultTemp) / vol;

            return resultP;
        }

        static double NumberOfMoles(double mass, double molecularWeight)
        {
            //returns the number of moles of air from mass and molecular weight 

            return mass/molecularWeight;
        }

        static double CelciusToKelvin(double celcius)
        {
            //returns celcius into kelvin

            return celcius + 273.15;
        }

        static double PaToPSI(double pascals)
        {
            //returns Pressure in PSI as a double

            return pascals/6895;
        }

        private static void DisplayPresure(double pressure)
        {
            //Calls method and returns PSI
            double valuePSI = PaToPSI(pressure);

            //Prints the Pressure in both pascals and PSI
            Console.WriteLine($"\nPressue is {pressure} pascals or {valuePSI} PSI.\n");

        }
    }
}
