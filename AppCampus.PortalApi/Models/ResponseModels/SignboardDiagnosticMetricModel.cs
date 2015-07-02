namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The signboard diagnostic metric model.
    /// </summary>
    public class SignboardDiagnosticMetricModel
    {
        /// <summary>
        /// The name of the metric.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The value of the metric.
        /// </summary>
        public string Value { get; private set; }

        internal static SignboardDiagnosticMetricModel From(string name, string value)
        {
            return new SignboardDiagnosticMetricModel()
            {
                Name = name,
                Value = value
            };
        }
    }
}