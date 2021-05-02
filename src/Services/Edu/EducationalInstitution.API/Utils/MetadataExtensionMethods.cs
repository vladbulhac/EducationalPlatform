using Grpc.Core;

namespace EducationalInstitutionAPI.Utils
{
    public static class MetadataExtensionMethods
    {
        public static void AddMultiple(this Metadata metadata, (string key, string value)[] data)
        {
            if (metadata is not null)
            {
                for (int i = 0; i < data.Length; i++)
                    metadata.Add(data[i].key, data[i].value);
            }
        }
    }
}