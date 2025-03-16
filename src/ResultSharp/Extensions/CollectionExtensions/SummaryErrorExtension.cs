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
            if (errors == null || errors.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();

            for (int i = 0; i < errors.Count - 1; i++)
                sb.AppendLine(errors[i].Message);
            sb.Append(errors[^1].Message);

            return sb.ToString();
        }
    }
}
