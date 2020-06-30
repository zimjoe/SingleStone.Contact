using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleStone.Contact.Service.Validators
{
    public class AddressValidator : AbstractValidator<Models.Address>
	{

		public AddressValidator()
		{
			// built in Email Validator
			RuleFor(x => x.Street).NotNull().MaximumLength(255);
			RuleFor(x => x.City).NotNull().MaximumLength(100);

			RuleFor(x => x.State).Length(2);
			RuleFor(x => x.Zip).Length(5,10); 

		}
	}
}
