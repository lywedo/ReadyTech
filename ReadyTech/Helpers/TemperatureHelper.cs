namespace ReadyTech.Helpers
{
    public static class TemperatureHelper
    {
        public static double CelsiusToFahrenheit(double celsius)
        {
            return (celsius * 9 / 5) + 32;
        }

        public static double FahrenheitToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }

        public static double KelvinToCelsius(double kelvin)
        {
            return kelvin - 273.15;
        }
    }
}
