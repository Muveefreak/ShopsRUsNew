using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopsRUs.Api.Controllers;
using ShopsRUs.Core.Customers.Queries;
using ShopsRUs.Core.Customers.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ShopsRUs.UnitTests.Controllers
{
    public class CustomersControllerTest
    {
        private CustomersController sut;
        private Mock<IMediator> mediatorMock = new Mock<IMediator>();

        public CustomersControllerTest()
        {
            sut = new CustomersController(mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllCustomers_Should_Return_OkObjectResult_For_Valid_Input()
        {
            // Arrange
            mediatorMock.Setup(o => o.Send(It.IsAny<GetAllCustomersQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(() => (new List<CustomerResponse>() { }, "Success", true));

            //Act
            await mediatorMock.Object.Send(new GetAllCustomersQuery());


            // Assert
            mediatorMock.Verify(x => x.Send(It.IsAny<GetAllCustomersQuery>(), It.IsAny<CancellationToken>()));
        }
    }
}
