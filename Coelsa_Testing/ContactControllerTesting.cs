using System;
using Services;
using Servidor;
using Servidor.Models;
using Xunit;
using Moq;
using Servidor.Controllers;
using Servidor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coelsa_Testing
{
	public class ContactControllerTesting
	{
		[Fact]
		public void GetById_ExisteContacto()
		{
			//ARRANGE
			int id = 3;
			var contact = new ContactModel()
			{
				Id = 3,
				FirstName = "Carlos",
				LastName = "Córdoba",
				Company = "Facebook",
				Email = "c.cordoba@facebook.com",
				PhoneNumber = 1536556556
			};
			var expected = new OkObjectResult(contact);
			var access = new Mock<DataAccess>()
				.As<IDataAccess>();
			var contactMapper = new Mock<ContactMapper>(access.Object)
				.As<IContactMapper>();
			contactMapper.Setup(x => x.BuscarContactoPorId(id)).Returns(contact);

			var sut = new ContactController(contactMapper.Object);

			//ACT
			var actual = sut.Get(id);

			//ASSERT
			Assert.Equal(expected.ToString(), actual.ToString());
		}


	}
}
