namespace ReadyTech.Helpers
{
    public static class TimeHelper
    {
        private static DateTime _fixedDateTime = DateTime.MinValue;

        public static void SetUtcNow(DateTime dateTime)
        {
            _fixedDateTime = dateTime;
        }

        public static DateTime UtcNow()
        {
            return _fixedDateTime == DateTime.MinValue ? DateTime.UtcNow : _fixedDateTime;
        }
    }
}
