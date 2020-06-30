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
			// totally could use a list for better validation
			RuleFor(x => x.State).Length(2);
			// allow 5 or 9 digit zips with or without the dash
			RuleFor(x => x.Zip).NotNull().Matches(@"(^\d{5}$)|(^\d{9}$)|(^\d{5}-\d{4}$)"); 

		}
	}
}
