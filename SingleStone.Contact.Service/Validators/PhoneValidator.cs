using FluentValidation;
using SingleStone.Contact.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleStone.Contact.Service.Validators
{
    public class PhoneValidator : AbstractValidator<Models.Phone>
	{

		public PhoneValidator()
		{
			// built in Email Validator
			RuleFor(x => x.PhoneNumber).NotNull().Matches(@"^[2-9]\d{2}-\d{3}-\d{4}$").WithMessage("phone.number is required and must use a xxx-xxx-xxxx format"); ;
			
			// must be one of the types
			RuleFor(x => x.PhoneType).NotNull().Must(x=> PhoneTypes.AllTypes.Contains(x)).WithMessage("phone.type is required and must be mobile, home or work.");
		}

	}
}
