using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
		
			public ContactValidator(IOptions<Utilities.ContactOptions> optionsAccessor, IHttpContextAccessor httpContextAccessor)
			{
				options = optionsAccessor;
			
				// built in Email Validator
				RuleFor(x => x.Email).NotNull().EmailAddress().Custom((email, context) => {
					// check for unique email
					// also check if this is the same one for updates
					var contact = context.ParentContext.InstanceToValidate as Models.Contact;
					var existingContacts = Models.Contact.GetByEmail(email, options);
					// need to find.Where(c=> !c.Id.Equals(contact.Id))
					if ((!string.IsNullOrEmpty(email)) && existingContacts.Any())
					{
						var method = httpContextAccessor.HttpContext.Request.Method;
						if (method.Equals("POST", StringComparison.OrdinalIgnoreCase))
						{
							// this is new
							context.AddFailure("Email must be unique");
						}
						else {
							// this is an update let's grab the Id from the call
							// this is super hinky
							string idparameter = httpContextAccessor.HttpContext.Request.RouteValues["Id"].ToString();
							int id = 0;
							bool throwError = true;
							if (int.TryParse(idparameter,out id)) {
								if (existingContacts.Where(c => c.Id.Equals(id)).Any()) {
									throwError = false;
								}
							}
							if (throwError) {
								// this is not the original
								context.AddFailure("Email must be unique");
							}
						}
					}
				});
				
				// set validators for sub objects
				RuleFor(x => x.Address).SetValidator(new AddressValidator());
				RuleFor(x => x.Name).SetValidator(new ContactNameValidator());
				RuleForEach(x => x.Phones).SetValidator(new PhoneValidator());

		}

		private readonly IOptions<ContactOptions> options; //set in the startup from the appsettings
	}
}
