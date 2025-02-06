namespace ResultSharp.Logging
{
    public static class LoggingExtensions
    {
        // TODO: Пока чисто заглушка, реализовать нормальное логгирование в дальнейшем
        public static Result LogIfFailure(this Result result)
        {
            if (result.IsFailure)
                Console.WriteLine(result.Errors.ElementAt(0).Message);
            return result;
        }
    }
}
