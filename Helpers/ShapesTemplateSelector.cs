using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using AvaloniaTester.Models;
using System;
using System.Collections.Generic;

namespace AvaloniaTester.Helpers
{
    internal class ShapesTemplateSelector : IDataTemplate
    {
        [Content]
        public Dictionary<string, IDataTemplate> AvailableTemplates { get; } = new Dictionary<string, IDataTemplate>();

        public Control? Build(object? param)
        {
            var key = param?.ToString();
            if(key is null)
            {
                throw new ArgumentNullException(nameof(param));
            }
            return AvailableTemplates[key].Build(param);
        }

        public bool Match(object? data)
        {
            // Our Keys in the dictionary are strings, so we call .ToString() to get the key to look up
            var key = data?.ToString();

            return data is ShapeType                        // the provided data needs to be our enum type
                    && !string.IsNullOrEmpty(key)           // and the key must not be null or empty
                    && AvailableTemplates.ContainsKey(key); // and the key must be found in our Dictionary
        }
    }
}
