//CIS1280 P2
//IDEAL GAS CALCULATOR USING CLASSES

using System;

namespace IdealGasUsingClass
{
    class Program
    {
        

        static void Main(string[] args)
        {

            //VARIABLE DECLARATIONS/INITS
            int gasCount;
            string another = "no";
            string[] gasNames = new string[100];
            double[] moleWeights = new double[100];


            double molecularWeight;  //molecular weight of user selected gas
            double gasVolume;   //user input volume in m^3
            double gasMass;     //user input mass in g
            double gasTemp;     //user input temp in C
            string gasSelection;  //user gas selection entry
           

            //DRIVER SECTION
            DisplayHeader();  //not required but already written by time document changed so left as is
            GetMolecularWeights(ref gasNames, ref moleWeights, out gasCount);  //fill arrays & get element count
            DisplayGasNames(gasNames, gasCount);                               //display gases to user
            do
            {
                //get gas name from user
                Console.WriteLine("Please type in a gas name from the list above (must be exact match): ");
                gasSelection = Console.ReadLine();
                //get the molecular weight (and count of elements)
                molecularWeight = GetMolecularWeightFromName(gasSelection, gasNames, moleWeights, gasCount);
                if (molecularWeight > 0)
                {
                    //if gas is found:
                    //instantiate new IdealGas
                    IdealGas newGas = new IdealGas();
                    newGas.SetMolecularWeight(molecularWeight);

                    try
                    {
                        //get volume from user
                        Console.WriteLine("Please input the volume of the gas in cubic meters: ");
                        gasVolume = Convert.ToDouble(Console.ReadLine());
                        newGas.SetVolume(gasVolume);

                        //get mass from user
                        Console.WriteLine("Please input the mass of the gas in grams: ");
                        gasMass = Convert.ToDouble(Console.ReadLine());
                        newGas.SetMass(gasMass);

                        //get temp from user
                        Console.WriteLine("Please input the temperature of the gas in Celsius: ");
                        gasTemp = Convert.ToDouble(Console.ReadLine());
                        newGas.SetTemp(gasTemp);

                        //display the result
                        DisplayPressure(newGas.GetPressure());
 
                    }

                    catch (FormatException e)
                    {
                        Console.WriteLine("\nYou must enter a number!\n");

                    }

                    catch (OverflowException e)
                    {
                        Console.WriteLine("\nYou entered too large a number!\n");
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("\nError: " + e.Message);
                        Console.WriteLine();
                    }
                }
                else
                {
                    //gas not found
                    Console.WriteLine("Gas Name not found!  Please make sure you typed the name correctly.\n"); 
                }
               

                //ask for another?
                Console.WriteLine("Would you like to calculate another pressure? (enter yes to continue): ");
                another =Console.ReadLine();

            } while (string.Equals(another.ToLower(), "yes"));  //ignore case on input (small attempt at validation)

            //exit message
            Console.WriteLine("\n\nHave a great day!  Goodbye!\n\n");
        }

        static void DisplayHeader()
        {
            //DISPLAYS PROGRAM HEADER
            Console.WriteLine("B. C.");
            Console.WriteLine("Ideal Gas Calculator");
            Console.WriteLine("This program calculates pressure exerted by a gas in a container given the following inputs:");
            Console.WriteLine("\tName of the gas");
            Console.WriteLine("\tVolume of the container (in cubic meters");
            Console.WriteLine("\tWeight of the gas (in grams)");
            Console.WriteLine("\tTemperature of the gas (celsius)");
            Console.WriteLine();
            Console.WriteLine();
        }

        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {
            //READS CSV FILE AND POPULATES ARRAYS FOR NAMES/MOL.WTS
            count = 0;  //elements in array
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@".\MolecularWeightsGasesAndVapors.csv");    //init new Streamreader

            file.ReadLine();                                                            //read and throw away the header line
            while ((line = file.ReadLine()) != null)                                    //fill arrays gasNames[] and moleWeights[]
            {
                string[] result = line.Split(",");
                gasNames[count] = result[0];
                molecularWeights[count] = Convert.ToDouble(result[1]);
                count++;
            }
            file.Close();
        }

        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            //DISPLAYS GAS NAMES IN 3 COLUMNS
            for (int i = 0; i < countGases; i++)
            {
                System.Console.Write("{0,-20}", gasNames[i]);
                if ((i + 1) % 3 == 0)                               //line feed after every third item 
                {
                    System.Console.WriteLine();
                }
            }
        }
        private static double GetMolecularWeightFromName(string gasName, string[] gasNames, double[] molecularWeights, int countGases)
        {
            //GETS MOLECULAR WEIGHT FROM GAS NAME.  RETURNS -1 IF NAME NOT FOUND          
            double response = -1;
            for (int i = 0; i < countGases; i++)
            {
                if (gasName.Equals(gasNames[i]))
                {
                    response = molecularWeights[i];
                }
            }
            return response;
        }

        private static void DisplayPressure(double pressure)
        {
            Console.WriteLine("\nThe pressure is {0} Pascals, which is {1} PSI.\n", pressure, PaToPSI(pressure));
        }

        static double PaToPSI(double pascals)
        {
            return pascals * 0.0001450377;  
            //conversion factor taken from www.unitconverters.net/pressure/pascal-to-psi.htm
        }


        
    }
}
