﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using Onism.Cldr.Extensions;

namespace Onism.Cldr.Utils
{
    internal static class JsonUtils
    {
        /// <summary>
        /// Returns a <see cref="JToken"/> loaded from a JSON file.
        /// </summary>
        /// <param name="path">The path to the JSON file.</param>
        public static JToken LoadFromFile(string path)
        {
            var allText = File.ReadAllText(path);
            return JToken.Parse(allText);
        }

        /// <summary>
        /// Safely merges two JSON objects. If any original value is missing from the
        /// resulting object (due to overwriting), an exception is thrown.
        /// </summary>
        public static JContainer SafeMerge(JContainer o1, JContainer o2)
        {
            var originalLeaves = new HashSet<string>(o1
                .Leaves()
                .Concat(o2.Leaves())
                .Select(x => x.Path + (string) x)
                .Distinct());

            o1.Merge(o2);

            var newLeaves = new HashSet<string>(o1
                .Leaves()
                .Select(x => x.Path + (string) x)
                .Distinct());

            // now assert
            if (originalLeaves.Any(x => newLeaves.Contains(x) == false))
                throw new Exception();

            return o1;
        }
    }
}
