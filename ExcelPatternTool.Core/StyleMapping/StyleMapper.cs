using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.Attributes;

namespace ExcelPatternTool.StyleMapping
{
    public class StyleMapper
    {
        private Type _entityType;
        private StyleMapperProvider styleMapperProvider;
        private Dictionary<string, string> aliasDictionary;


        private IEnumerable<StyleMappingContainer> _styleMappingContainers;

        public IEnumerable<StyleMappingContainer> StyleMappingContainers
        {
            get
            {
                if (_styleMappingContainers == null)
                {
                    _styleMappingContainers = styleMapperProvider.GetStyleMappingContainers(_entityType);

                }
                return _styleMappingContainers;
            }
            set { _styleMappingContainers = value; }
        }

        public IEnumerable<ProcessResult> Validate(object obj)
        {
            if (_entityType == null)
            {
                throw new Exception("还没有为实体指定类型，是否忘记调用SetStyleMapperProvider?");
            }
            var result = new List<ProcessResult>();

            foreach (var styleMapper in StyleMappingContainers)
            {
                if (styleMapper.StyleMapping == null)
                {
                    continue;
                }
                var currentConvention = styleMapperProvider.GetConvention(styleMapper.StyleMapping.Convention.ToString()).Convention;
                if (currentConvention == null)
                {
                    continue;
                }
                styleMapper.StyleMapping.Expression = ValidateItem(styleMapper.StyleMapping.Expression);
                var currentResult = currentConvention?.Invoke(styleMapper, obj);
                if (currentResult == null)
                {
                    continue;
                }
                currentResult.KeyName = $"RowNumber 为 {(obj as IExcelEntity).RowNumber} 的 {styleMapper.PropName}";
                result.Add(currentResult);
            }

            return result;
        }

        private string ValidateItem(string originalString)
        {
            var result = originalString;


            foreach (var pare in aliasDictionary)
            {
                var propName = pare.Value;
                var propNameAlias = pare.Key;

                if (!string.IsNullOrEmpty(propNameAlias))
                {
                    result = originalString.Replace(propNameAlias, propName);
                }
            }

            return result;

        }

        public virtual void InitAliasDictionary(Type entityType)
        {
            var type = entityType;
            foreach (var prop in type.GetProperties())
            {
                var attrs = Attribute.GetCustomAttributes(prop);
                var propName = prop.Name;
                var propNameAlias = string.Empty;

                ImportableAttribute attribute = default;
                foreach (var attr in attrs)
                {
                    if (attr is ImportableAttribute)
                    {
                        attribute = attr as ImportableAttribute;
                    }
                }

                if (attribute != null && attribute.Ignore == false && !string.IsNullOrEmpty(attribute.Name))
                {
                    propNameAlias = attribute.Name;
                    aliasDictionary.Add(propNameAlias, propName);
                }
            }
        }

        public string GetOriginalPropertyName(string alias)
        {
            var result = string.Empty;
            aliasDictionary.TryGetValue(alias, out result);
            return result;
        }

        public StyleMapper(StyleMapperProvider styleMapperProvider) : this()
        {
            this.styleMapperProvider = styleMapperProvider;

        }

        public StyleMapper()
        {
            aliasDictionary = new Dictionary<string, string>();

        }

        public void SetStyleMapperProvider<T>(StyleMapperProvider styleMapperProvider)
        {
            SetStyleMapperProvider(typeof(T), styleMapperProvider);
        }

        public void SetStyleMapperProvider(Type entityType, StyleMapperProvider styleMapperProvider)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException("EntityType为空");
            }
            _entityType = entityType;
            this.styleMapperProvider = styleMapperProvider;
            InitAliasDictionary(entityType);
            this.styleMapperProvider.PropertyTypeMaper = GetOriginalPropertyName;

        }
    }
}
