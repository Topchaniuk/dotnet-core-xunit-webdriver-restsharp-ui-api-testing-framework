namespace DCXWRUATF.ServiceClient.Client
{
    public static class OdataFilter
    {
        public static string FilterBy(this string field, object value)
        {
            string result;
            switch (value.GetType().Name)
            {
                case "String":
                    result = $"{field} eq '{value}'";
                    break;
                case "Guid":
                    result = $"{field} eq guid'{value}'";
                    break;
                case "Boolean":
                    result = $"{field} eq '{value.ToString().ToLower()}'";
                    break;
                default:
                    result = $"{field} eq {value}";
                    break;
            }
            return result;
        }
    }
}
