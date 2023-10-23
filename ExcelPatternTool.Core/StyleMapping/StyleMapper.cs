using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ExcelPatternTool.Contracts;
using ExcelPatternTool.Contracts.Attributes;

namespace ExcelPatternTool.Core.StyleMapping
{
    public class StyleMapper
    {
        public StyleMapperProvider styleMapperProvider;


        private Dictionary<string, StyleMapping> _styleMappingContainers;

        public Dictionary<string, StyleMapping> StyleMappingContainers
        {
            get
            {
                if (_styleMappingContainers == null)
                {
                    _styleMappingContainers = styleMapperProvider.GetStyleMappingContainers();

                }
                return _styleMappingContainers;
            }
            set { _styleMappingContainers = value; }
        }




        public StyleMapper(StyleMapperProvider styleMapperProvider) : this()
        {
            this.styleMapperProvider = styleMapperProvider;

        }

        public StyleMapper()
        {

        }


    }
}
