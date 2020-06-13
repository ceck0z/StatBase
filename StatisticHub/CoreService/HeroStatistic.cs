using System;

namespace CoreService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class HeroStatistic : IHeroStatistic
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public void SelectHero(string heroName)
        {
            Console.WriteLine($"Your hero is {heroName}");
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
