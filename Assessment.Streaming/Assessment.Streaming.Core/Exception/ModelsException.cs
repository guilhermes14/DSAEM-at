using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Streaming.Core.Exception
{
    public class ModelsException : System.Exception
    {
        public List<ModelsValidation> Errors { get; set; } = new List<ModelsValidation>();

        public ModelsException() { }

        public ModelsException(ModelsValidation validation)
        {
            this.AddError(validation);
        }

        public void AddError(ModelsValidation validation)
        {
            this.Errors.Add(validation);
        }

        public void ValidateAndThrow()
        {
            if (this.Errors.Any())
                throw this;
        }
    }

    public class ModelsValidation
    {
        public string ErrorName { get; set; } = "Validation Error";
        public string ErrorMessage { get; set; }
    }
}

