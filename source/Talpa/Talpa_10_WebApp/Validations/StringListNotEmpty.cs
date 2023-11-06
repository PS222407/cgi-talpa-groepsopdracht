namespace Talpa_10_WebApp.Validations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StringListNotEmpty : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is List<string> stringList)
            {
                if (stringList.Count != 0)
                {
                    foreach (var item in stringList)
                    {
                        if (string.IsNullOrWhiteSpace(item))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true; 
        }
    }

}
