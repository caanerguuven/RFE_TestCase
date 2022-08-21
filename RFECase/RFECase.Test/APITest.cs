using Microsoft.AspNetCore.Mvc;
using Moq;
using RFECase.API.Controllers;
using RFECase.API.ViewModel;
using RFECase.Domain.DTO;
using RFECase.Domain.DTO.Base;
using RFECase.Service.Abstract;
using System.Threading.Tasks;
using Xunit;

namespace RFECase.Test
{
    public class APITest
    {
        [Fact]
        public async Task LeftPost_Should_Return_202()
        {
            //Arrange
            BaseResponseDTO returnObject = new BaseResponseDTO
            {
                StatusCode = 202,
                Result = "Accepted"
            };
            var mockService = new Mock<IDiffService>();
            mockService.Setup(service => service.SendToLeft(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(returnObject));
            var controller = new DiffController(mockService.Object);

            //Act
            var result = await controller.SendToLeft(1, new DiffRequestViewModel { Input = "Hello World !" });

            //Assert
            var response = Assert.IsType<JsonResult>(result);
            Assert.Equal(202, ((BaseResponseDTO)response.Value).StatusCode);
        }

        [Fact]
        public async Task RightPost_Should_Return_202()
        {
            //Arrange
            BaseResponseDTO returnObject = new BaseResponseDTO
            {
                StatusCode = 202,
                Result = "Accepted"
            };
            var mockService = new Mock<IDiffService>();
            mockService.Setup(service => service.SendToRight(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(returnObject));
            var controller = new DiffController(mockService.Object);

            //Act
            var result = await controller.SendToRight(1, new DiffRequestViewModel { Input = "Hello World !" });

            //Assert
            var response = Assert.IsType<JsonResult>(result);
            Assert.Equal(202, ((BaseResponseDTO)response.Value).StatusCode);
        }

        [Fact]
        public async Task GetDiff_Should_Return_200()
        {
            //Arrange
            DiffResponseDTO returnObject = new DiffResponseDTO
            {
                StatusCode = 200,
                Result = "OK",
                Detail = null,
                Elaborations = null
            };

            var mockService = new Mock<IDiffService>();
            mockService.Setup(service => service.GetDiff(It.IsAny<int>())).Returns(Task.FromResult(returnObject));
            var controller = new DiffController(mockService.Object);

            //Act
            var result = (JsonResult)await controller.GetDiff(1);

            //Assert
            var response = Assert.IsType<JsonResult>(result);
            Assert.Equal(200, ((BaseResponseDTO)response.Value).StatusCode);
        }
    }
}
