using Grpc.Core;

namespace EducationalInstitutionAPI.Utils;

public static class MetadataExtensionMethods
{
    public static void AddMultiple(this Metadata metadata, (string key, string value)[] data)
    {
        if (metadata is not null)
        {
            foreach (var (key, value) in data)
                metadata.Add(key, value);
        }
    }
}