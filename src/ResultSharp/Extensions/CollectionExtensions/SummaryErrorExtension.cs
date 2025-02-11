using ResultSharp.Errors;
using System.Collections.ObjectModel;
using System.Text;

namespace ResultSharp.Extensions.CollectionExtensions
{
    /// <summary>
    /// Provides extension methods for summarizing error messages.
    /// </summary>
    public static class SummaryErrorExtension
    {
        /// <summary>
        /// Summarizes the error messages from a collection of errors.
        /// </summary>
        /// <param name="errors">The collection of errors.</param>
        /// <returns>A string containing the summarized error messages.</returns>
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
