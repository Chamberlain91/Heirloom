using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Meadows.Hardware
{
    internal static class VendorUtility
    {
        internal static TVendor DetectVendor<TVendor>(
            Dictionary<TVendor, string[]> vendorTemplates,
            Dictionary<TVendor, string[]> vendorHints,
            string vendorString)
        {
            // Normalize input by upper case and stripping non-alphanumeric
            // ie, "NVIDIA Corporation" -> "NVIDIA CORPORATION"
            vendorString = vendorString.ToUpper();
            vendorString = Regex.Replace(vendorString, @"[^\w\d ]", "");
            vendorString = Regex.Replace(vendorString, @" +", " ");

            // For each vendor
            foreach (var vendor in (TVendor[]) Enum.GetValues(typeof(TVendor)))
            {
                if (vendorTemplates != null)
                {
                    // Try to get known template strings
                    if (vendorTemplates.TryGetValue(vendor, out var templates))
                    {
                        // For each template string
                        foreach (var template in templates)
                        {
                            if (string.Equals(template, vendorString))
                            {
                                // Exact match!
                                return vendor;
                            }
                        }
                    }
                }

                // Could not determine exact match.
                // We will try to find by hints (brand names, etc).

                // Try to get known template strings
                if (vendorHints.TryGetValue(vendor, out var hints))
                {
                    // For each template string
                    foreach (var hint in hints)
                    {
                        // If the vendor string contains this hint, we
                        // assume it is correct. Hope for the best!
                        if (vendorString.Contains(hint))
                        {
                            // Hint detected, hopefully correct.
                            return vendor;
                        }
                    }
                }
            }

            return default;
        }
    }
}
