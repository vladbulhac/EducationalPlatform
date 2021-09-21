using System;
using System.Collections.Generic;

namespace Aggregator.Authorization.Policies
{
    public class ResourceConstraints
    {
        public Dictionary<string, HashSet<string>> Claims { get; init; }
        public int MinimumClaimsNeeded { get; init; }

        public ResourceConstraints(Dictionary<string, HashSet<string>> claims, int minimumClaimsNeeded = 1)
        {
            if (claims is null || claims.Count == 0) throw new ArgumentException($"{nameof(claims)} collection does not contain any element!");
            if (minimumClaimsNeeded <= 0) throw new ArgumentOutOfRangeException(nameof(minimumClaimsNeeded), $"{nameof(minimumClaimsNeeded)} is lesser or equal than 0!");

            if (minimumClaimsNeeded > claims.Count) MinimumClaimsNeeded = claims.Count;

            Claims = claims;
            MinimumClaimsNeeded = minimumClaimsNeeded;
        }

        public bool ContainsKeyValuePair(string key, string value)
        {
            if (Claims.ContainsKey(key))
                return Claims[key].Contains(value);

            return false;
        }
    }
}