using ResultSharp.Errors;
using System.Collections.ObjectModel;
using System.Text;

namespace ResultSharp.Extensions.CollectionExtensions
{
    public static class SummaryErrorExtension
    {
        public static string SummaryErrorMessages(this ReadOnlyCollection<Error> errors)
        {
            var sb = new StringBuilder();

            foreach (var error in errors.Take(errors.Count - 1))
                sb.AppendLine(error.Message);
            sb.Append(errors.Last().Message);

            return sb.ToString();
        }
    }
}
