using System;
using System.Collections.Generic;
using System.Text;

namespace IdealGasUsingClass
{
    class IdealGas
    {
        //instance variables

        private double gasMass;
        private double gasVolume= -1.0;  //init to -1.0 to stop divide by zero error in Calc() prior to all fields being set
        private double gasTemp;
        private double molecularWeight;
        private double pressure;
        
        //setters/getters
        public double GetMass()
        {
            return this.gasMass;
        }

        public void SetMass(double newMass)
        {
            this.gasMass = newMass;
            this.Calc();
        }

        public double GetVolume()
        {
            return this.gasVolume;
        }

        public void SetVolume(double newVolume)
        {
            this.gasVolume = newVolume;
            this.Calc();
        }

        public double GetTemp()
        {
            return this.gasTemp;
        }

        public void SetTemp(double newTemp)
        {
            this.gasTemp = newTemp;
            this.Calc();
        }

        public double GetMolecularWeight()
        {
            return this.molecularWeight;
        }

        public void SetMolecularWeight(double newMolecularWeight)
        {
            this.molecularWeight = newMolecularWeight;
            this.Calc();
        }

        public double GetPressure()
        {
            return this.pressure;
        }
        //Instance method for calculating pressure
        private void Calc()
        {
            double tempK = this.gasTemp + 273.15;
            double numMoles = this.gasMass / this.molecularWeight;
            this.pressure=(numMoles * tempK * 8.3145) / this.gasVolume;       
        }
    }
}
