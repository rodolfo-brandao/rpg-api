namespace Rpg.Application.Validators
{
    /// <summary>
    /// Provides a set of common validation messages.
    /// </summary>
    internal static class ValidationMessages
    {
        public const string ForEmptyProperty = "This property cannot be empty.";

        /// <summary>
        /// Provides a validation message for when a certain record property conflicts with one another.
        /// </summary>
        /// <param name="recordName">The name of the record.</param>
        /// <param name="propertyValue">The current value of the property.</param>
        /// <returns>A message indicating that the specified record property conflicted with one another.</returns>
        public static string ForConflictWithRecordProperty(string recordName, object propertyValue)
        {
            return $"{recordName} '{propertyValue}' already exists.";
        }

        /// <summary>
        /// Provides a validation message for when a property length exceeds the specified maximum length.
        /// </summary>
        /// <param name="maxLength">The maximum length for the given property.</param>
        /// <returns>A message indicating that the property length cannot exceed the specified maximum length.</returns>
        public static string ForExceedPropertyLength(int maxLength)
        {
            return $"This property cannot exceed {maxLength} characters.";
        }

        /// <summary>
        /// Provides a validation message for when a property length exceeds the specified maximum length.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="maxLength">The maximum length for the given property.</param>
        /// <returns>A message indicating that the property length cannot exceed the specified maximum length.</returns>
        public static string ForExceedPropertyLength(string propertyName, int maxLength)
        {
            return $"The property '{propertyName}' cannot exceed {maxLength} characters.";
        }

        /// <summary>
        /// Provides a validation message for when a record is not found.
        /// </summary>
        /// <param name="recordName">The name of the record.</param>
        /// <returns>A message indicating that the record could not be found.</returns>
        public static string ForRecordNotFound(string recordName)
        {
            return $"{recordName} not found.";
        }

        /// <summary>
        /// Provides a validation message for when a record is not found by a given property value.
        /// </summary>
        /// <param name="recordName">The name of the record.</param>
        /// <param name="propertyValue">The current value of the property.</param>
        /// <returns>A message indicating that the record could not be found by a given property value.</returns>
        public static string ForRecordNotFound(string recordName, object propertyValue)
        {
            return $"{recordName} '{propertyValue}' not found.";
        }
    }
}
