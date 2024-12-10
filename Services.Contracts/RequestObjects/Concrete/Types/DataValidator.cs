using Service.Contracts.RequestObjects.Interfaces.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects.Concrete.Types
{
    public class DataValidator : IDataValidator<Func<object, bool>>
    {
        private Func<object, bool> validator {  get; set; }

        public DataValidator(Func<object, bool> validator)
        {
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public bool Validate(object obj)
        {
            return validator(obj);
        }
    }
}
