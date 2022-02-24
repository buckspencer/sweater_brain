using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using SweaterBrain.Models;
using SweaterBrain.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SweaterBrainTests
{
    public class SweaterServiceTests
    {
        [Fact]
        public async Task ReturnsOpenWeatherResponseWhenRequestWeatherInfoIsCalled()
        {
            var configuration = new Mock<IConfiguration>();
            var configSection = new Mock<IConfigurationSection>();
            var _handlerMock = new Mock<HttpMessageHandler>();
            _ = _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""coord"":{""lon"":-118,""lat"":34},""weather"":[{""id"":802,""main"":""Clouds"",""description"":""scattered clouds"",""icon"":""03d""}],""base"":""stations"",""main"":{""temp"":49.98,""feels_like"":48.43,""temp_min"":46.42,""temp_max"":53.46,""pressure"":1015,""humidity"":78},""visibility"":10000,""wind"":{""speed"":4.61,""deg"":190},""clouds"":{""all"":40},""dt"":1645545947,""sys"":{""type"":2,""id"":2009710,""country"":""US"",""sunrise"":1645540148,""sunset"":1645580529},""timezone"":-28800,""id"":5354819,""name"":""Hacienda Heights"",""cod"":200}"),
            });

            var httpClient = new HttpClient(_handlerMock.Object);
            configuration.Setup(x => x.GetSection("MySection:Value")).Returns(configSection.Object);
            var serviceService = new SweaterService(httpClient, (IConfiguration)configSection.Object);
            var retrievedInfo = await serviceService.RequestWeatherInfo();

            Assert.IsType<OpenWeatherResponse>(retrievedInfo);
            Assert.IsType<Double>(retrievedInfo.Main.Temp);

            _handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());

        }

        [Fact]
        public async Task ReturnsSweaterImageWhenCalculateCurrentBestSweaterImageIsCalled()
        {
            var configuration = new Mock<IConfiguration>();
            var configSection = new Mock<IConfigurationSection>();
            var _handlerMock = new Mock<HttpMessageHandler>();
            _ = _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""coord"":{""lon"":-118,""lat"":34},""weather"":[{""id"":802,""main"":""Clouds"",""description"":""scattered clouds"",""icon"":""03d""}],""base"":""stations"",""main"":{""temp"":49.98,""feels_like"":48.43,""temp_min"":46.42,""temp_max"":53.46,""pressure"":1015,""humidity"":78},""visibility"":10000,""wind"":{""speed"":4.61,""deg"":190},""clouds"":{""all"":40},""dt"":1645545947,""sys"":{""type"":2,""id"":2009710,""country"":""US"",""sunrise"":1645540148,""sunset"":1645580529},""timezone"":-28800,""id"":5354819,""name"":""Hacienda Heights"",""cod"":200}"),
            });

            var httpClient = new HttpClient(_handlerMock.Object);
            configuration.Setup(x => x.GetSection("MySection:Value")).Returns(configSection.Object);
            var serviceService = new SweaterService(httpClient, (IConfiguration)configSection.Object);
            var retrievedInfo = await serviceService.RequestWeatherInfo();

            Assert.IsType<String>(serviceService.CalculateCurrentBestSweater(retrievedInfo))

        }
    }
}

