using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Authorization
{
    public class ResourceAccessConstraints
    {
        private readonly Dictionary<string, HashSet<string>> claims;
        public int MinimumClaimsNeeded { get; init; }

        public ResourceAccessConstraints(Dictionary<string, HashSet<string>> claims, int minimumClaimsNeeded = 1)
        {
            if (claims is null || claims.Count == 0) throw new ArgumentException($"{nameof(claims)} collection does not contain any element!");
            if (minimumClaimsNeeded <= 0) throw new ArgumentOutOfRangeException(nameof(minimumClaimsNeeded), $"{nameof(minimumClaimsNeeded)} is lesser or equal than 0!");

            if (minimumClaimsNeeded > claims.Count) MinimumClaimsNeeded = claims.Count;

            this.claims = claims;
            MinimumClaimsNeeded = minimumClaimsNeeded;
        }

        public bool ContainsKeyValuePair(string key, string value)
        {
            if (claims.ContainsKey(key))
                return claims[key].Contains(value);

            return false;
        }

        public bool ContainsKey(string key) => claims.ContainsKey(key);

        public bool ContainsValue(string value)
        {
            foreach (var (_, setOfValues) in claims)
            {
                if (setOfValues is not null && setOfValues.Count > 0 && setOfValues.Contains(value))
                    return true;
            }

            return false;
        }
    }
}