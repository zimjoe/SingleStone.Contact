using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using SingleStone.Contact.Service.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SingleStone.Contact.Service.Validators
{
    public class ContactValidator : AbstractValidator<Models.Contact>
	{
		
			public ContactValidator(IOptions<Utilities.ContactOptions> optionsAccessor)
			{
				options = optionsAccessor;

				// built in Email Validator
				RuleFor(x => x.Email).NotNull().EmailAddress().Custom((email, context) => {
					// check for unique email
					if ((!string.IsNullOrEmpty(email)) && Models.Contact.GetByEmail(email, options).Any())
					{
						context.AddFailure("Email must be unique");
					}
				});

				// set validators for sub objects
				RuleFor(x => x.Address).SetValidator(new AddressValidator());
				RuleFor(x => x.Name).SetValidator(new ContactNameValidator());
				RuleFor(x => x.Phone).SetValidator(new PhoneValidator());

		}

		private readonly IOptions<ContactOptions> options; //set in the startup from the appsettings
	}
}
